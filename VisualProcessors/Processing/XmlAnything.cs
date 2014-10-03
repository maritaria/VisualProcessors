﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace VisualProcessors.Processing
{
	public sealed class XmlAnything<T> : IXmlSerializable
	{
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
			string type = reader.GetAttribute("type");
			reader.Read(); // consume the value
			if (type == "null")
				return;// leave T at default value
			XmlSerializer serializer = new XmlSerializer(Type.GetType(type));
			this.Value = (T)serializer.Deserialize(reader);
			reader.ReadEndElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			if (Value == null)
			{
				writer.WriteAttributeString("type", "null");
				return;
			}
			Type type = this.Value.GetType();
			XmlSerializer serializer = new XmlSerializer(type);
			writer.WriteAttributeString("type", type.AssemblyQualifiedName);
			serializer.Serialize(writer, this.Value);
		}
	}
}