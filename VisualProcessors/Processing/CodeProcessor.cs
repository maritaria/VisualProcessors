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
	[ProcessorMeta(Author = "Bram Kamies",
		Description = "Allows user C# code to be executed during simulations",
		AllowOptionalInputs = true)]
	public class CodeProcessor : Processor
	{
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

			Code = "public static void Process(CodeProcessor processor)" + Environment.NewLine +
				"{" + Environment.NewLine +
				"\tthrow new Exception();" + Environment.NewLine +
				"}";
		}

		public string Code { get; set; }

		public Action<CodeProcessor> ProcessFunction { get; set; }

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

		#region IXmlSerializable Members

		public override void ReadXml(XmlReader reader)
		{
			Code = reader.GetAttribute("Code");
			base.ReadXml(reader);
		}

		public override void WriteXml(XmlWriter writer)
		{
			writer.WriteAttributeString("Code", Code);
			base.WriteXml(writer);
		}

		#endregion IXmlSerializable Members
	}
}