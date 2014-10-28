using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualProcessors.Design
{
	public class EnumFlagsListBox : CheckedListBox
	{
		#region Properties

		private Type m_Type;
		private Enum m_Value;

		public Enum Value
		{
			get
			{
				int i = 0;
				foreach (int o in CheckedItems)
				{
					i += o;
				}
				return (Enum)Enum.ToObject(m_Type, i);
			}
			set
			{
				if (m_Type != value.GetType())
				{
					m_Type = value.GetType();
					UpdateList();
				}
				m_Value = value;
				UpdateSelection();
			}
		}

		#endregion Properties

		#region Constructor

		public EnumFlagsListBox()
		{
			CheckOnClick = true;
		}

		#endregion Constructor

		#region Methods

		private void UpdateList()
		{
			Console.WriteLine("UpdateList()");
			foreach (string s in Enum.GetNames(m_Type))
			{
				Items.Add(Enum.Parse(m_Type, s));
			}
		}

		private void UpdateSelection()
		{
			Console.WriteLine("UpdateSelection() " + m_Value);
			SelectedIndices.Clear();
			if (m_Value == null)
			{
				return;
			}
			for (int i = 0; i < Items.Count; i++)
			{
				Enum o = (Enum)Items[i];
				if (m_Value.HasFlag(o))
				{
					SetItemCheckState(i, CheckState.Checked);
				}
				else
				{
					SetItemCheckState(i, CheckState.Unchecked);
				}
			}
		}

		#endregion Methods
	}
}