using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using VisualProcessors;
using VisualProcessors.Processing;

namespace VisualProcessors.Processing
{
	public delegate void InputReceived(InputChannel sender);

	/// <summary>
	///  The InputChannel class stores data for a Processor instance, it exposes events for the
	///  Processor to activate when input values are available. The data is stored using the FIFO
	///  system.
	/// </summary>
	public class InputChannel : IXmlSerializable
	{
		#region Properties

		private bool m_ChangingSource = false;
		private List<double> m_Data = new List<double>();
		private string m_Name = "";
		private string m_OwnerName = "";
		private OutputChannel m_Source;

		/// <summary>
		///  Gets or sets the value of the InputChannel if IsConstant is true. This property is
		///  automaticly set to false when another processor's output is linked to the InputChannel
		/// </summary>
		public double ConstantValue { get; set; }

		/// <summary>
		///  Gets or sets whether the InputChannel is a constant value, the processor will always be
		///  able to receive a value from this channel
		/// </summary>
		public bool IsConstant { get; set; }

		/// <summary>
		///  Gets or sets whether the InputChannel is optional, when it is the processor can run
		///  Process() even if there is no data available in the InputChannel
		/// </summary>
		public bool IsOptional { get; set; }

		/// <summary>
		///  Gets the name of the InputChannel, also represents the name of the argument it stores
		///  data for.
		/// </summary>
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

		/// <summary>
		///  Gets or sets the default value used when the inputchannel is optional, but no data is
		///  available
		/// </summary>
		public double OptionalDefault { get; set; }

		/// <summary>
		///  Gets the processor the InputChannel belongs to.
		/// </summary>
		public Processor Owner { get; private set; }

		/// <summary>
		///  Gets the OutputChannel who is linked to the InputChannel, null if no OutputChannel is
		///  linked
		/// </summary>
		public OutputChannel Source
		{
			get
			{
				return m_Source;
			}
		}

		#endregion Properties

		#region Constructor

		/// <summary>
		///  Creates a new instance of the InputChannel class
		/// </summary>
		/// <param name="owner">   The processor the InputChannel stores values for</param>
		/// <param name="name">    The name of the InputChannel</param>
		/// <param name="optional">Sets the IsOptional property</param>
		public InputChannel(Processor owner, string name, bool optional = false)
		{
			Owner = owner;
			Name = name;
			IsConstant = false;
			ConstantValue = 0;
			IsOptional = optional;
			OptionalDefault = double.NaN;
		}

		/// <summary>
		///  Please do not use, used for XML Deserialization
		/// </summary>
		public InputChannel()
		{
		}

		#endregion Constructor

		#region Methods

		/// <summary>
		///  Gets the oldest value from the inputchannel, and removes it from the channel.
		/// </summary>
		/// <returns>The oldest value written to the channel</returns>
		public double GetValue()
		{
			if (IsConstant)
			{
				return ConstantValue;
			}
			if (IsOptional)
			{
				if (!HasData())
				{
					return OptionalDefault;
				}
			}
			lock (m_Data)
			{
				double result = m_Data[0];
				m_Data.RemoveAt(0);
				return result;
			}
		}

		/// <summary>
		///  Gets whether there is data in the queue ready to be read
		/// </summary>
		public bool HasData()
		{
			//Don't need to lock if the inputchannel is a constant
			if (IsConstant)
			{
				return true;
			}
			lock (m_Data)
			{
				return m_Data.Count > 0;
			}
		}

		public void Unlink()
		{
			if (m_Source != null)
			{
				var ms = m_Source;
				m_Source = null;
				ms.Unlink(this);
				if (!m_ChangingSource)
				{
					OnSourceChanged();
				}
			}
		}

		/// <summary>
		///  Adds a new value to the channel, and tells the processor that a value has been added.
		/// </summary>
		/// <param name="value">The value to pass to the processor</param>
		public void WriteValue(double value)
		{
			lock (m_Data)
			{
				m_Data.Add(value);
			}
		}

		internal void SetSource(OutputChannel output)
		{
			m_ChangingSource = true;

			//Unlink output if its set
			if (m_Source != null)
			{
				Unlink();
			}
			m_Source = output;
			OnSourceChanged();
			m_ChangingSource = false;
		}

		#endregion Methods

		#region Build

		internal void Build(Pipeline pipeline)
		{
			if (Owner == null && m_OwnerName != null)
			{
				Owner = pipeline.GetByName(m_OwnerName);
			}
		}

		#endregion Build

		#region Events

		/// <summary>
		///  Invoked after the name of the InputChannel has been modified
		/// </summary>
		public event InputChannelNameChanged NameChanged;

		/// <summary>
		///  Invoked after the source of the InputChannel has been changed
		/// </summary>
		public event EventHandler SourceChanged;

		private void OnNameChanged(string oldname, string newname)
		{
			if (NameChanged != null)
			{
				NameChanged(this, oldname, newname);
			}
		}

		private void OnSourceChanged()
		{
			if (SourceChanged != null)
			{
				SourceChanged(this, EventArgs.Empty);
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
			IsOptional = bool.Parse(reader.GetAttribute("IsOptional"));
			IsConstant = bool.Parse(reader.GetAttribute("IsConstant"));
			ConstantValue = double.Parse(reader.GetAttribute("ConstantValue"));
			OptionalDefault = double.Parse(reader.GetAttribute("OptionalDefault"));
			m_OwnerName = reader.GetAttribute("Owner");
			reader.Read();
		}

		public void WriteXml(XmlWriter writer)
		{
			if (Owner == null)
			{
				throw new InvalidOperationException("Cannot serialize nonbuild InputChannels");
			}
			writer.WriteAttributeString("Name", Name);
			writer.WriteAttributeString("IsOptional", IsOptional.ToString());
			writer.WriteAttributeString("IsConstant", IsConstant.ToString());
			writer.WriteAttributeString("ConstantValue", ConstantValue.ToString());
			writer.WriteAttributeString("OptionalDefault", OptionalDefault.ToString());
			writer.WriteAttributeString("Owner", Owner.Name);
		}

		#endregion IXmlSerializable Members
	}
}