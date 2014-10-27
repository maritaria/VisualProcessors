using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using VisualProcessors.Design;

namespace VisualProcessors.Design
{
	public class ComPortTypeEditor : UITypeEditor
	{
		private IWindowsFormsEditorService m_EditorService;

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			m_EditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

			ComPortListBox listbox = new ComPortListBox();
			listbox.Value = value.ToString();
			m_EditorService.DropDownControl(listbox);

			return listbox.Value ?? value;
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}
	}
}
