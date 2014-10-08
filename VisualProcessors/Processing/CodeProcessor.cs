using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using VisualProcessors.Controls;

namespace VisualProcessors.Processing
{
	[ProcessorAttribute("Bram Kamies", "Allows user C# code to be executed during simulations", "A", "Output1",
		AllowOptionalInputs = true)]
	public class CodeProcessor : Processor
	{
		public static string DefaultCode =
			"public static void Process(CodeProcessor processor)" + Environment.NewLine +
			"{" + Environment.NewLine + "\t" + Environment.NewLine + "}" + Environment.NewLine + Environment.NewLine +
			"public static void Prepare(CodeProcessor processor)" + Environment.NewLine +
			"{" + Environment.NewLine + "\t" + Environment.NewLine + "}" + Environment.NewLine;
		public CodeProcessor()
		{
		}

		public CodeProcessor(string name)
			: base(name)
		{
			AddInputChannel("A", false);
			AddInputChannel("B", true);
			AddInputChannel("C", true);
			AddInputChannel("D", true);
			AddInputChannel("E", true);
			AddOutputChannel("Output1");
			AddOutputChannel("Output2");
			AddOutputChannel("Output3");

			Code = DefaultCode;

		}

		public string Code { get; set; }

		public Action<CodeProcessor> ProcessFunction { get; set; }
		public Action<CodeProcessor> PrepareFunction { get; set; }

		public override void GetUserInterface(Panel panel)
		{
			CodePanel cpanel = new CodePanel(this, this.Code);
			cpanel.Dock = DockStyle.Fill;
			panel.Controls.Add(cpanel);
			base.GetUserInterface(panel);
		}

		protected override void Process()
		{
			if (ProcessFunction != null)
			{
				try
				{
					ProcessFunction(this);
				}
				catch (Exception e)
				{
					MessageBox.Show(e.ToString());
					OnRequestExecutionHalt();
				}
			}
		}
		protected override bool Prepare()
		{
			base.Prepare();
			if (PrepareFunction!=null)
			{
				try
				{
					PrepareFunction(this);
				}
				catch (Exception e)
				{
					MessageBox.Show(e.ToString());
					return false;
				}
			}
			return true;
		}

		#region IXmlSerializable Members

		public override void ReadXml(XmlReader reader)
		{
			Code = reader.GetAttribute("Code").Replace("\\t","\t");
			base.ReadXml(reader);
		}

		public override void WriteXml(XmlWriter writer)
		{
			writer.WriteAttributeString("Code", Code.Replace("\t", "\\t"));
			base.WriteXml(writer);
		}

		#endregion IXmlSerializable Members
	}
}