using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace VisualProcessors.Processing
{
	public class OutputChannel : IXmlSerializable
	{
		#region Properties

		private string m_Name;
		private string m_OwnerName;
		private List<InputChannelDescription> m_RawTargets = new List<InputChannelDescription>();
		private List<InputChannel> m_Targets = new List<InputChannel>();

		public string Name
		{
			get
			{
				return m_Name;
			}
			set
			{
				string old = m_Name;
				m_Name = value;
				OnNameChanged(old, value);
			}
		}

		public Processor Owner { get; private set; }

		public InputChannel[] Targets
		{
			get
			{
				return m_Targets.ToArray();
			}
		}

		#endregion Properties

		#region Constructor

		/// <summary>
		///  Please do not use, only for serialization compatibility
		/// </summary>
		public OutputChannel()
		{
		}

		public OutputChannel(Processor owner, string name)
		{
			Owner = owner;
			Name = name;
		}

		#endregion Constructor

		#region Methods

		public bool IsLinked(InputChannel target)
		{
			return m_Targets.Contains(target);
		}

		public void Link(InputChannel target)
		{
			if (!m_Targets.Contains(target))
			{
				m_Targets.Add(target);
				target.SetSource(this);
				OnOutputAdded();
			}
		}

		public void Unlink(InputChannel target)
		{
			m_Targets.Remove(target);
			target.Unlink();
			OnOutputRemoved();
		}

		public void Unlink()
		{
			while (m_Targets.Count > 0)
			{
				Unlink(m_Targets.First());
			}
		}

		public void Unlink(string processorname, string channelname)
		{
			foreach (InputChannel target in m_Targets)
			{
				if (target.Owner.Name == processorname && target.Name == channelname)
				{
					Unlink(target);
					return;
				}
			}
		}

		public void WriteValue(double value)
		{
			foreach (InputChannel target in m_Targets)
			{
				target.WriteValue(value);
			}
		}

		#endregion Methods

		#region Build

		internal void Build(Pipeline pipeline)
		{
			Owner = pipeline.GetByName(m_OwnerName);
		}

		internal void BuildLinks(Pipeline pipeline)
		{
			foreach (InputChannelDescription input in m_RawTargets)
			{
				Processor p = pipeline.GetByName(input.Owner);
				if (p == null)
				{
					continue;
				}
				InputChannel channel = p.GetInputChannel(input.Name);
				if (channel != null)
				{
					Link(channel);
				}
			}
		}

		#endregion Build

		#region Events

		public event OutputChannelNameChanged NameChanged;

		public event EventHandler OutputAdded;

		public event EventHandler OutputRemoved;

		private void OnNameChanged(string oldname, string newname)
		{
			if (NameChanged != null)
			{
				NameChanged(this, oldname, newname);
			}
		}

		private void OnOutputAdded()
		{
			if (OutputAdded != null)
			{
				OutputAdded(this, EventArgs.Empty);
			}
		}

		private void OnOutputRemoved()
		{
			if (OutputRemoved != null)
			{
				OutputRemoved(this, EventArgs.Empty);
			}
		}

		#endregion Events

		#region IXmlSerializable Members

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			if (!reader.HasAttributes)
			{
				throw new FormatException("Expected a type attribute!");
			}
			Name = reader.GetAttribute("Name");
			m_OwnerName = reader.GetAttribute("Owner");
			reader.Read();

			XmlSerializer serializer = new XmlSerializer(m_RawTargets.GetType(), GetOverrides(m_RawTargets.GetType(), "Targets"));
			m_RawTargets = (List<InputChannelDescription>)serializer.Deserialize(reader);
			reader.ReadEndElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			if (Owner == null)
			{
				throw new InvalidOperationException("Cannot serialize nonbuild InputChannels");
			}
			writer.WriteAttributeString("Name", Name);
			writer.WriteAttributeString("Owner", Owner.Name);
			m_RawTargets.Clear();
			foreach (InputChannel t in m_Targets)
			{
				m_RawTargets.Add(new InputChannelDescription(t.Owner.Name, t.Name));
			}
			XmlSerializer serializer = new XmlSerializer(m_RawTargets.GetType(), GetOverrides(m_RawTargets.GetType(), "Targets"));
			XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
			ns.Add("", "");
			serializer.Serialize(writer, m_RawTargets, ns);
		}

		private XmlAttributeOverrides GetOverrides(Type t, string name)
		{
			XmlAttributeOverrides overrides = new XmlAttributeOverrides();
			XmlAttributes attribute = new XmlAttributes();
			attribute.XmlRoot = new XmlRootAttribute(name);
			attribute.Xmlns = false;
			overrides.Add(t, attribute);
			return overrides;
		}

		/// <summary>
		///  Used internally for serialization
		/// </summary>
		public class InputChannelDescription : IXmlSerializable
		{
			public string Name;

			public string Owner;

			public InputChannelDescription()
			{
			}

			public InputChannelDescription(string owner, string name)
			{
				Owner = owner;
				Name = name;
			}

			#region IXmlSerializable Members

			public XmlSchema GetSchema()
			{
				throw new NotImplementedException();
			}

			public void ReadXml(XmlReader reader)
			{
				Owner = reader.GetAttribute("Owner");
				Owner = reader.GetAttribute("Name");
				reader.Read();
			}

			public void WriteXml(XmlWriter writer)
			{
				writer.WriteAttributeString("Owner", Owner);
				writer.WriteAttributeString("Name", Name);
			}

			#endregion IXmlSerializable Members
		}

		#endregion IXmlSerializable Members
	}
}