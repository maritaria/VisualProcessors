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
	public class AssemblyDetails : IXmlSerializable
	{
		#region Properties

		private Assembly m_Assembly;

		public bool IsGlobalAssembly { get; private set; }

		public bool IsLoaded { get; private set; }

		public string Source { get; private set; }

		#endregion Properties

		#region Constructor

		public AssemblyDetails()
		{
			IsLoaded = false;
		}

		public AssemblyDetails(Assembly subject)
			: this()
		{
			m_Assembly = subject;
			IsGlobalAssembly = m_Assembly.GlobalAssemblyCache;
			if (IsGlobalAssembly)
			{
				Source = m_Assembly.FullName;
			}
			else
			{
				Source = m_Assembly.Location;
			}
			IsLoaded = true;
		}

		#endregion Constructor

		#region Methods

		public Assembly GetAssembly()
		{
			if (m_Assembly == null)
			{
				if (IsGlobalAssembly)
				{
					m_Assembly = Assembly.Load(Source);
				}
				else
				{
					m_Assembly = Assembly.LoadFrom(Source);
				}
			}
			return m_Assembly;
		}


		#endregion Methods

		#region IXmlSerializable Members

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			IsGlobalAssembly = bool.Parse(reader.GetAttribute("IsGlobalAssembly"));
			Source = reader.GetAttribute("Source");
			reader.Read();
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteAttributeString("IsGlobalAssembly", IsGlobalAssembly.ToString());
			writer.WriteAttributeString("Source", Source);
		}

		#endregion IXmlSerializable Members
	}
}