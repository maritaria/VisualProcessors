using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualProcessors.Processing;
using VisualProcessors.Processors;

namespace VisualProcessors.Forms
{
	public partial class ProcessorInterfaceForm : Form
	{
		#region Properties

		private Processor m_Processor;

		public Processor Processor
		{
			get
			{
				return m_Processor;
			}
			private set
			{
				m_Processor = value;
			}
		}

		#endregion Properties

		#region Constructor

		public ProcessorInterfaceForm()
		{
			InitializeComponent();
		}

		public ProcessorInterfaceForm(Processor proc)
			: this()
		{
			Processor = proc;
			Text = proc.Name;
			proc.NameChanged += proc_NameChanged;
			proc.GetUserInterface(UIPanel);
		}

		#endregion Constructor

		#region Methods

		#endregion Methods

		#region Event Handlers

		private void proc_NameChanged(Processor processor, string oldname, string newname)
		{
			Text = newname;
		}

		#endregion Event Handlers
	}
}