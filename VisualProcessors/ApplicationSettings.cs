using System;
using System.Collections.Generic;
using System.ComponentModel;
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

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Assembly directory")]
		[Category("Startup")]
		[Description("Sets the directory in the CWD to load assemblies from, requires restart to take effect")]
		[DefaultValue("/Assemblies")]
		public string AssemblySubDirectory { get; set; }

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Channel data")]
		[Category("File extensions")]
		[Description("Sets the file extension used to save/load channel data")]
		[DefaultValue(".cdata.xml")]
		public string ChannelDataFileExtension { get; set; }

		[Browsable(false)]
		[ReadOnly(true)]
		public string LastOpenedPipelineFile { get; set; }

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Open last file opened on startup")]
		[Category("Startup")]
		[Description("When true, the application will (on startup) load up the file that was opened last by the application previously")]
		[DefaultValue(false)]
		public bool OpenLastPipelineOnStartup { get; set; }

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Pipeline")]
		[Category("File extensions")]
		[Description("Sets the file extension used to save/load pipeline files")]
		[DefaultValue(".pipe.xml")]
		public string PipelineFileExtension { get; set; }

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Show data form on startup")]
		[Category("Startup")]
		[Description("When true, the application will (on startup) open the data form")]
		[DefaultValue(false)]
		public bool ShowDataFormOnStartup { get; set; }

		[XmlElement(Type = typeof(XmlColor))]
		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Simulation color")]
		[Category("Worksheet")]
		[Description("Sets the background color of the worksheet during simulation")]
		public Color SimulationBackgroundColor { get; set; }

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Start maximized")]
		[Category("Startup")]
		[Description("When true, the application will (on startup) maximize itself")]
		[DefaultValue(true)]
		public bool StartMaximized { get; set; }

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Use 3D border on worksheet")]
		[Category("Startup")]
		[Description("When true, the worksheet will have a 3D border around it (requires restart)")]
		[DefaultValue(false)]
		public bool Use3DBorderOnMdiClient { get; set; }

		[XmlElement(Type = typeof(XmlColor))]
		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Default color")]
		[Category("Worksheet")]
		[Description("Sets the background color of the worksheet during editing")]
		public Color WorksheetBackgroundColor { get; set; }

		#endregion Properties

		#region Constructor

		public ApplicationSettings()
		{
			StartMaximized = true;
			ShowDataFormOnStartup = false;
			OpenLastPipelineOnStartup = false;
			Use3DBorderOnMdiClient = false;
			WorksheetBackgroundColor = SystemColors.ActiveCaption;
			SimulationBackgroundColor = Color.Crimson;
			PipelineFileExtension = ".pipe.xml";
			ChannelDataFileExtension = ".cdata.xml";
			AssemblySubDirectory = "Assemblies";
			LastOpenedPipelineFile = "";
		}

		#endregion Constructor

		#region Methods

		#endregion Methods
	}

	#region XmlColor Helper Class

	public class XmlColor
	{
		private Color color_ = Color.Black;

		public XmlColor()
		{
		}

		public XmlColor(Color c)
		{
			color_ = c;
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

		public static implicit operator Color(XmlColor x)
		{
			return x.ToColor();
		}

		public static implicit operator XmlColor(Color c)
		{
			return new XmlColor(c);
		}

		public void FromColor(Color c)
		{
			color_ = c;
		}

		public bool ShouldSerializeAlpha()
		{
			return Alpha < 0xFF;
		}

		public Color ToColor()
		{
			return color_;
		}
	}

	#endregion XmlColor Helper Class
}