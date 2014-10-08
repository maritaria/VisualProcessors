using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace VisualProcessors.Processing
{
	public sealed class XmlAnything<T> : IXmlSerializable
	{
		private static string ProcessorAssemblyName = typeof(Processor).Assembly.GetName().Name;

		public XmlAnything()
		{
		}

		public XmlAnything(T t)
		{
			this.Value = t;
		}

		public T Value { get; set; }

		public XmlSchema GetSchema()
		{
			return (null);
		}

		public void ReadXml(XmlReader reader)
		{
			if (!reader.HasAttributes)
			{
				throw new FormatException("expected a type attribute!");
			}
			string type = reader.GetAttribute("Type");
			reader.Read(); // consume the value
			if (type == "null")
				return;// leave T at default value
			Type t = Type.GetType(type, false, true);
			if (t == null)
			{
				t = FindType(type);
			}
			XmlSerializer serializer = new XmlSerializer(t);
			this.Value = (T)serializer.Deserialize(reader);
			reader.ReadEndElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			if (Value == null)
			{
				writer.WriteAttributeString("Type", "null");
				return;
			}
			Type type = this.Value.GetType();
			XmlSerializer serializer = new XmlSerializer(type);
			writer.WriteAttributeString("Type", type.AssemblyQualifiedName);
			serializer.Serialize(writer, this.Value);
		}

		private Type FindType(string fullname)
		{
			foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
			{
				foreach (AssemblyName a in asm.GetReferencedAssemblies())
				{
					if (a.Name == ProcessorAssemblyName)
					{
						foreach (Type _t in asm.GetTypes())
						{
							if (_t.AssemblyQualifiedName == fullname)
							{
								return _t;
							}
						}
					}
				}
			}
			return null;
		}
	}
}