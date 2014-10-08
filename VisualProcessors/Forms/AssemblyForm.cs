using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualProcessors.Processing;

namespace VisualProcessors.Forms
{
	public partial class AssemblyForm : Form
	{
		public AssemblyForm()
		{
			InitializeComponent();
		}

		private void AssemblyForm_Load(object sender, EventArgs e)
		{
			Type processorType = typeof(Processor);
			foreach (Assembly asm in Program.ProcessorAssemblies)
			{
				AssemblyName asmname = asm.GetName();
				TreeNode tnode = ProcessorTree.Nodes.Add(asmname.FullName);
				tnode.Tag = asm;
				foreach (Type t in asm.GetTypes())
				{
					if (t.IsSubclassOf(processorType))
					{
						TreeNode pnode = tnode.Nodes.Add(t.FullName);
						pnode.Tag = t;
					}
				}
			}
			ProcessorTree.Sort();
		}

		private void ProcessorTree_Click(object sender, EventArgs e)
		{
		}

		private void ProcessorTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Node != null)
			{
				ProcessorTree.SelectedNode = e.Node;
				if (e.Node.Tag is Assembly)
				{
					Assembly asm = e.Node.Tag as Assembly;
					InformationList.Items.Clear();
					InformationList.Items.Add("Assembly: " + asm.FullName);

					return;
				}
				if (e.Node.Tag is Type)
				{
					Type t = e.Node.Tag as Type;
					InformationList.Items.Clear();
					InformationList.Items.Add("Type: " + t.FullName);

					foreach (Attribute attr in Attribute.GetCustomAttributes(t))
					{
						if (attr is ProcessorAttribute)
						{
							ProcessorAttribute pm = attr as ProcessorAttribute;
							InformationList.Items.Add("Author: " + pm.Author);
							InformationList.Items.Add("Description: " + pm.Description);
							InformationList.Items.Add("AllowUserSpawn: " + ((pm.AllowUserSpawn) ? "Allowed" : "Denied"));
							InformationList.Items.Add("AllowOptionalInputs: " + ((pm.AllowOptionalInputs) ? "Allowed" : "Denied"));
							InformationList.Items.Add("Default InputChannel: " + pm.DefaultInput);
							InformationList.Items.Add("Default OutputChannel: " + pm.DefaultInput);
							InformationList.Items.Add("InputTab: " + ((pm.HideInputTab) ? "Hidden" : "Shown"));
							InformationList.Items.Add("SettingsTab: " + ((pm.HideSettingsTab) ? "Hidden" : "Shown"));
							InformationList.Items.Add("OutputTab: " + ((pm.HideOutputTab) ? "Hidden" : "Shown"));
							break;
						}
					}

					return;
				}
			}
		}
	}
}