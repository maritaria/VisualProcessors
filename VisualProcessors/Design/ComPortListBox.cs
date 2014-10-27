using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualProcessors.Design
{
	public class ComPortListBox : ListBox
	{
		#region Properties

		public string Value
		{
			get
			{
				if (SelectedItem != null)
				{
					return (string)SelectedItem;
				}
				return null;
			}
			set
			{
				SelectedItem = Value;
			}
		}

		#endregion Properties

		#region Constructor

		public ComPortListBox()
		{
			UpdateList();
		}

		#endregion Constructor

		#region Methods

		public void UpdateList()
		{
			string selected = (string)SelectedItem;
			Items.Clear();
			foreach (string s in SerialPort.GetPortNames())
			{
				Items.Add(s);
			}
			SelectedItem = selected;
		}

		#endregion Methods
	}
}