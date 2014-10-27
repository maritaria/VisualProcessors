using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using VisualProcessors.Forms;
using System.Drawing.Design;
namespace VisualProcessors.Design
{
	/// <summary>
	///
	/// </summary>
	/// <seealso cref="http://stackoverflow.com/questions/1016239/how-to-create-custom-propertygrid-editor-item-which-opens-a-form"/>
	public class SerialPortEditor : UITypeEditor
	{
		public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{
			IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
			SerialPortSettings settings = value as SerialPortSettings;
			if (svc != null && settings != null)
			{
				using (SerialPortForm form = new SerialPortForm(settings.PortName, settings.BaudRate, settings.DataBits, settings.Parity, settings.Handshake, settings.StopBits, settings.ReadTimeout, settings.WriteTimeout, settings.ReceivedBytesThreshold, settings.DtrEnable, settings.RtsEnable))
				{
					if (svc.ShowDialog(form) == DialogResult.OK)
					{
						//Update the settings object, value object is also updated because settings is a casted reference
						settings.BaudRate = form.BaudRate;
						settings.DataBits = form.DataBits;
						settings.DtrEnable = form.DtrEnable;
						settings.Handshake = form.Handshake;
						settings.Parity = form.Parity;
						settings.PortName = form.PortName;
						settings.ReadTimeout = form.ReadTimeout;
						settings.ReceivedBytesThreshold = form.ReceivedBytesThreshold;
						settings.RtsEnable = form.RtsEnable;
						settings.StopBits = form.StopBits;
						settings.WriteTimeout = form.WriteTimeout;
					}
				}
			}
			return value;
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}
	}
}
