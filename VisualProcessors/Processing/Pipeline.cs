using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace VisualProcessors.Processing
{
	public class Pipeline : IXmlSerializable
	{
		#region Properties

		private bool m_BreakOnModification = true;
		private List<Processor> m_Processors = new List<Processor>();
		private bool m_Running = false;

		public bool BreakOnModification
		{
			get
			{
				return m_BreakOnModification;
			}
			set
			{
				if (!IsRunning)
				{
					m_BreakOnModification = value;
				}
			}
		}

		public bool IsRunning
		{
			get
			{
				return m_Running;
			}
		}

		#endregion Properties

		#region Constructor

		public Pipeline()
		{
		}

		#endregion Constructor

		#region Methods

		public static Pipeline LoadFromFile(Stream filestream)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Pipeline));
			return serializer.Deserialize(filestream) as Pipeline;
		}

		/// <summary>
		///  Adds a new processor to the pipeline, if you don't add all the processors to the
		///  pipeline, errors will occur when trying to save/load the pipeline to/from a file.
		/// </summary>
		/// <param name="p">Processor instance to add</param>
		public void AddProcessor(Processor p)
		{
			if (p.Name == null)
			{
				throw new ArgumentException("Invalid processor");
			}
			lock (m_Processors)
			{
				if (m_Processors.Contains(p))
				{
					return;
				}
				if (m_Running)
				{
					Stop();
				}
				p.InputChannelAdded += ProcessorModified;
				p.InputChannelRemoved += ProcessorModified;
				p.OutputChannelAdded += ProcessorModified;
				p.OutputChannelRemoved += ProcessorModified;
				p.LinkAdded += ProcessorModified;
				p.LinkRemoved += ProcessorModified;
				p.NameChanged += delegate(Processor pr, string o, string n) { OnModified(); };
				p.Error += OnError;
				p.Warning += OnWarning;
				if (GetByName(p.Name) == null)
				{
					m_Processors.Add(p);
				}
			}
			OnModified();
		}

		public Processor GetByName(string name)
		{
			foreach (Processor p in m_Processors)
			{
				if (p.Name == name)
				{
					return p;
				}
			}
			return null;
		}

		/// <summary>
		///  Gets an array containing the names of all the processors in the pipeline
		/// </summary>
		/// <returns></returns>
		public string[] GetListOfNames()
		{
			List<string> names = new List<string>();
			foreach (Processor p in m_Processors)
			{
				names.Add(p.Name);
			}
			return names.ToArray();
		}

		/// <summary>
		///  Gets whether a given name already has a processor assigned
		/// </summary>
		/// <param name="name">The name to check</param>
		/// <returns>True if a processor in the pipeline has that name, false otherwise</returns>
		public bool IsProcessorNameTaken(string name)
		{
			return GetByName(name) != null;
		}

		public void RemoveProcessor(Processor p)
		{
			lock (m_Processors)
			{
				if (m_Running)
				{
					Stop();
				}
				p.InputChannelAdded -= ProcessorModified;
				p.InputChannelRemoved -= ProcessorModified;
				p.OutputChannelAdded -= ProcessorModified;
				p.OutputChannelRemoved -= ProcessorModified;
				p.LinkAdded -= ProcessorModified;
				p.LinkRemoved -= ProcessorModified;
				if (GetByName(p.Name) == p)
				{
					p.Dispose();
				}
				m_Processors.Remove(p);
				OnModified();
			}
		}

		public void Reset()
		{
			if (IsRunning)
			{
				Stop();
			}
			lock (m_Processors)
			{
				foreach (Processor p in m_Processors)
				{
					p.Reset();
				}
			}
		}

		/// <summary>
		///  Saves the pipeline to a file, serialized output if XML formatted.
		/// </summary>
		/// <param name="filestream">The filestream to write the serialized instance to</param>
		public void SaveToFile(Stream filestream)
		{
			lock (m_Processors)
			{
				if (m_Running)
				{
					Stop();
				}
				XmlSerializer serializer = new XmlSerializer(typeof(Pipeline));
				serializer.Serialize(Console.Out, this);
				serializer.Serialize(filestream, this);
			}
		}

		public void Start()
		{
			bool fail = false;
			lock (m_Processors)
			{
				foreach (Processor p in m_Processors)
				{
					try
					{
						p.Start();
					}
					catch(Exception e)
					{
						fail = true;
						OnError(p, e.Message);
						break;
					}
				}
				m_Running = true;
			}
			if (fail)
			{
				Stop();
				return;
			}
			OnStarted();
		}

		public void Stop()
		{
			lock (m_Processors)
			{
				foreach (Processor p in m_Processors)
				{
					p.Stop();
				}
				m_Running = false;
			}
			OnStopped();
		}

		internal void Build()
		{
			foreach (Processor p in m_Processors)
			{
				p.Build(this);
			}
			foreach (Processor p in m_Processors)
			{
				p.BuildLinks(this);
			}
		}

		#endregion Methods

		#region Events

		public event EventHandler Modified;

		public event EventHandler Started;

		public event EventHandler Stopped;
		/// <summary>
		/// Invoked when a Processor indicates a warning
		/// </summary>
		public event Action<Processor, string> Warning;
		/// <summary>
		/// Invoked when a Processor indicates an error
		/// </summary>
		public event Action<Processor, string> Error;

		private void OnWarning(Processor p, string s)
		{
			if (Warning != null)
			{
				Warning(p, s);
			}
		}

		private void OnError(Processor p, string s)
		{
			if (Error != null)
			{
				Error(p, s);
			}
		}

		private void OnModified()
		{
			if (Modified != null)
			{
				Modified(this, EventArgs.Empty);
			}
		}

		private void OnStarted()
		{
			if (Started != null)
			{
				Started(this, EventArgs.Empty);
			}
		}

		private void OnStopped()
		{
			if (Stopped != null)
			{
				Stopped(this, EventArgs.Empty);
			}
		}

		#endregion Events

		#region Event Handlers

		private void p_RequestExecutionHalt(object sender, EventArgs e)
		{
			Stop();
		}

		private void ProcessorModified(object sender, EventArgs e)
		{
			if (IsRunning && BreakOnModification)
			{
				Stop();
			}
			OnModified();
		}

		#endregion Event Handlers

		#region IXmlSerializable Members

		public XmlSchema GetSchema()
		{
			return (null);
		}

		public void ReadXml(XmlReader reader)
		{
			reader.Read();//Consume <pipeline...>

			List<XmlAnything<Processor>> plist = new List<XmlAnything<Processor>>();
			XmlSerializer s2 = new XmlSerializer(plist.GetType(), GetOverrides(plist.GetType(), "Processors"));
			plist = (List<XmlAnything<Processor>>)s2.Deserialize(reader);
			foreach (XmlAnything<Processor> element in plist)
			{
				m_Processors.Add(element.Value);
			}
			reader.ReadEndElement();//Consume </pipeline>
			Build();
		}

		public void WriteXml(XmlWriter writer)
		{
			XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
			ns.Add("", "");

			List<XmlAnything<Processor>> plist = new List<XmlAnything<Processor>>();
			XmlSerializer s2 = new XmlSerializer(plist.GetType(), GetOverrides(plist.GetType(), "Processors"));
			foreach (Processor p in m_Processors)
			{
				plist.Add(new XmlAnything<Processor>(p));
			}
			s2.Serialize(writer, plist, ns);
		}

		private XmlAttributeOverrides GetOverrides(Type t, string name)
		{
			XmlAttributeOverrides overrides = new XmlAttributeOverrides();
			XmlAttributes attributes = new XmlAttributes();
			attributes.XmlRoot = new XmlRootAttribute(name);
			attributes.Xmlns = false;
			overrides.Add(t, attributes);
			return overrides;
		}

		#endregion IXmlSerializable Members
	}
}