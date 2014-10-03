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

namespace VisualProcessors.Controls
{
	public partial class CodePanel : UserControl
	{
		#region Properties

		public CodeProcessor CodeProcessor { get; set; }

		#endregion Properties

		#region Constructor

		public CodePanel()
		{
			InitializeComponent();
		}

		public CodePanel(CodeProcessor cp)
			: this()
		{
			CodeProcessor = cp;
			CodeBox.Text = cp.Code;
		}

		#endregion Constructor

		#region Methods

		#endregion Methods
	}
}