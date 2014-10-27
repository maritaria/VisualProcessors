using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VisualProcessors.Forms
{
	public sealed class ApplicationSettings
	{
		#region Properties

		public string LastOpenedPipelineFile { get; set; }

		public bool OpenLastPipelineOnStartup { get; set; }

		public bool ShowDataFormOnStartup { get; set; }

		[XmlElement(Type = typeof(XmlColor))]
		public Color SimulationBackgroundColor { get; set; }

		public bool StartMaximized { get; set; }

		[XmlElement(Type = typeof(XmlColor))]
		public Color WorksheetBackgroundColor { get; set; }

		public string PipelineFileExtension { get; set; }

		public string ChannelDataFileExtension { get; set; }

		public string AssemblySubDirectory { get; set; }

		#endregion Properties

		#region Constructor

		public ApplicationSettings()
		{
			LastOpenedPipelineFile = "";
			OpenLastPipelineOnStartup = false;
			ShowDataFormOnStartup = false;
			SimulationBackgroundColor = Color.Pink;
			StartMaximized = true;
			WorksheetBackgroundColor = SystemColors.ActiveCaption;
			PipelineFileExtension = ".pipe.xml";
			ChannelDataFileExtension = ".cdata.xml";
			AssemblySubDirectory = "/Assemblies";
		}

		#endregion Constructor

		#region Methods

		#endregion Methods
	}
	#region XmlColor Helper Class
	public class XmlColor
	{
		private Color color_ = Color.Black;

		public XmlColor() { }
		public XmlColor(Color c) { color_ = c; }


		public Color ToColor()
		{
			return color_;
		}

		public void FromColor(Color c)
		{
			color_ = c;
		}

		public static implicit operator Color(XmlColor x)
		{
			return x.ToColor();
		}

		public static implicit operator XmlColor(Color c)
		{
			return new XmlColor(c);
		}

		[XmlAttribute]
		public string Web
		{
			get { return ColorTranslator.ToHtml(color_); }
			set
			{
				try
				{
					if (Alpha == 0xFF) // preserve named color value if possible
						color_ = ColorTranslator.FromHtml(value);
					else
						color_ = Color.FromArgb(Alpha, ColorTranslator.FromHtml(value));
				}
				catch (Exception)
				{
					color_ = Color.Black;
				}
			}
		}

		[XmlAttribute]
		public byte Alpha
		{
			get { return color_.A; }
			set
			{
				if (value != color_.A) // avoid hammering named color if no alpha change
					color_ = Color.FromArgb(value, color_);
			}
		}

		public bool ShouldSerializeAlpha() { return Alpha < 0xFF; }
	}
	#endregion
}