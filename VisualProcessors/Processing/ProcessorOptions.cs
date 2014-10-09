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
	public class ProcessorOptions : IXmlSerializable
	{
		#region Properties
		private List<Option> m_Options = new List<Option>();
		#endregion Properties

		#region Constructor

		public ProcessorOptions()
		{
		}

		#endregion Constructor

		#region Options
		public void SetOption(string key, string value)
		{
			foreach(Option o in m_Options)
			{
				if (o.Key==key)
				{
					o.Value = value;
					return;
				}
			}
			m_Options.Add(new Option(key, value));
		}
		public string GetOption(string key)
		{
			foreach(Option o in m_Options)
			{
				if (o.Key==key)
				{
					return o.Value;
				}
			}
			return null;
		}
		public string this[string key]
		{
			get
			{
				return GetOption(key);
			}
			set
			{
				SetOption(key, value);
			}
		}
		#endregion Options



		#region IXmlSerializable Members

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			if (reader.IsEmptyElement)
			{
				//In case of <Options />, consume the Options tag otherwise the ReadEndElement will read to far.
				reader.Read();
				return;
			}
			reader.ReadStartElement();
			while (reader.Name == "Option")
			{
				SetOption(reader.GetAttribute("Key"), reader.ReadElementContentAsString());
			}
			reader.ReadEndElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			foreach(Option o in m_Options)
			{
				writer.WriteStartElement("Option");
				writer.WriteAttributeString("Key", o.Key);
				writer.WriteValue(o.Value);
				writer.WriteEndElement();
			}
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

		#endregion

		#region Option Helper Class
		private class Option
		{
			public string Key;
			public string Value;
			public Option(string k, string v)
			{
				Key = k;
				Value = v;
			}
		}
		#endregion
	}
}