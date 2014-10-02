using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace VisualProcessors.Processing
{
	public class Pipeline : IXmlSerializable
	{
		#region Properties

		private List<XmlAnything<Processor>> Processors = new List<XmlAnything<Processor>>();

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
			if (GetByName(p.Name) == null)
			{
				Processors.Add(new XmlAnything<Processor>(p));
			}
		}

		public Processor GetByName(string name)
		{
			foreach (XmlAnything<Processor> p in Processors)
			{
				if (p.Value.Name == name)
				{
					return p.Value;
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
			foreach (XmlAnything<Processor> p in Processors)
			{
				names.Add(p.Value.Name);
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

		/// <summary>
		///  Saves the pipeline to a file, serialized output if XML formatted.
		/// </summary>
		/// <param name="filestream">The filestream to write the serialized instance to</param>
		public void SaveToFile(Stream filestream)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Pipeline));
			serializer.Serialize(Console.Out, this);
			serializer.Serialize(filestream, this);
		}

		public void Start()
		{
			foreach (XmlAnything<Processor> p in Processors)
			{
				p.Value.Start();
			}
		}

		public void Stop()
		{
			foreach (XmlAnything<Processor> p in Processors)
			{
				p.Value.Stop();
			}
		}

		internal void Build()
		{
			foreach (XmlAnything<Processor> p in Processors)
			{
				p.Value.Build(this);
			}
			foreach (XmlAnything<Processor> p in Processors)
			{
				p.Value.BuildLinks(this);
			}
		}

		#endregion Methods

		#region IXmlSerializable Members

		public XmlSchema GetSchema()
		{
			return (null);
		}

		public void ReadXml(XmlReader reader)
		{
			XmlSerializer serializer = new XmlSerializer(Processors.GetType(), GetOverrides());
			reader.Read();
			Processors = (List<XmlAnything<Processor>>)serializer.Deserialize(reader);
			reader.ReadEndElement();
			Build();
		}

		public void WriteXml(XmlWriter writer)
		{
			XmlSerializer serializer = new XmlSerializer(this.Processors.GetType(), GetOverrides());
			XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
			ns.Add("", "");
			serializer.Serialize(writer, this.Processors, ns);
		}

		private XmlAttributeOverrides GetOverrides()
		{
			XmlAttributeOverrides overrides = new XmlAttributeOverrides();
			XmlAttributes attributes = new XmlAttributes();
			attributes.XmlRoot = new XmlRootAttribute("Processors");
			attributes.Xmlns = false;
			overrides.Add(typeof(List<XmlAnything<Processor>>), attributes);
			return overrides;
		}

		#endregion IXmlSerializable Members
	}
}