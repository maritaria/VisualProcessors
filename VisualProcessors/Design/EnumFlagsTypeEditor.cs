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

namespace VisualProcessors.Design
{
	public class EnumFlagsTypeEditor : UITypeEditor
	{
		private IWindowsFormsEditorService m_EditorService;

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			m_EditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

			EnumFlagsListBox listbox = new EnumFlagsListBox();
			listbox.Value = (Enum)value;
			m_EditorService.DropDownControl(listbox);

			return listbox.Value;
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}
	}
}