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

		/// <summary>
		///  Gets or sets an option stored by the instance, wraps GetOption() and SetOption()
		/// </summary>
		/// <param name="key">The key to access the option by</param>
		/// <returns></returns>
		public string this[string key]
		{
			get
			{
				return GetOption(key,null);
			}
			set
			{
				SetOption(key, value);
			}
		}

		/// <summary>
		///  Gets the value of a stored option
		/// </summary>
		/// <param name="key">The key to access the option by</param>
		/// <param name="alt">The default value to return if the option is not found</param>
		/// <returns>The value of the option, or alt if the option is not set</returns>
		public string GetOption(string key, string alt)
		{
			foreach (Option o in m_Options)
			{
				if (o.Key == key)
				{
					return o.Value;
				}
			}
			return alt;
		}

		/// <summary>
		///  Sets an option to a given value, creates a new option if it doesn't exist yet,
		///  overwrites it if it does.
		/// </summary>
		/// <param name="key">  The key to access the option by</param>
		/// <param name="value">The value to set the option to</param>
		public void SetOption(string key, string value)
		{
			foreach (Option o in m_Options)
			{
				if (o.Key == key)
				{
					o.Value = value;
					return;
				}
			}
			m_Options.Add(new Option(key, value));
		}
		/// <summary>
		/// Removes an option, by its key
		/// </summary>
		/// <param name="key">The key to access the option by</param>
		public void ClearOption(string key)
		{
			foreach (Option o in m_Options)
			{
				if (o.Key == key)
				{
					m_Options.Remove(o);
					return;
				}
			}
		}

		public IEnumerable<string> GetKeys()
		{
			//yield:		http://msdn.microsoft.com/en-us/library/9k7k7cf0.aspx
			//yield return:	http://www.dotnetperls.com/yield
			foreach(Option o in m_Options)
			{
				yield return o.Key;
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
				SetOption(StringDeserialize(reader.GetAttribute("Key")), StringDeserialize(reader.ReadElementContentAsString()));
			}
			reader.ReadEndElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			foreach (Option o in m_Options)
			{
				if (o.Value==null)
				{
					continue;
				}
				writer.WriteStartElement("Option");
				writer.WriteAttributeString("Key", StringSerialize(o.Key));
				writer.WriteValue(StringSerialize(o.Value));
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

		private string StringDeserialize(string source)
		{
			return source.Replace("\\\\", "\\").Replace("\\r", "\r").Replace("\\n", "\n").Replace("\\t", "\t");
		}

		private string StringSerialize(string source)
		{
			return source.Replace("\\", "\\\\").Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t");
		}

		#endregion IXmlSerializable Members

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

		#endregion Option Helper Class
	}
}