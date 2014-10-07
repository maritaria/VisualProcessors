using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualProcessors.Forms
{
	public static class FormExtensions
	{
		public static Point GetCenter(this Rectangle a)
		{
			return new Point((a.Left + a.Right) / 2, (a.Top + a.Bottom) / 2);
		}

		public static Point GetCenter(this Form a)
		{
			return a.Bounds.GetCenter();
		}
	}

	internal class MdiClientHelper : NativeWindow
	{
		private const int SB_BOTH = 3;
		private const int SB_CTL = 2;
		private const int SB_HORZ = 0;
		private const int SB_VERT = 1;
		private const int WM_NCCALCSIZE = 0x0083;

		public MdiClientHelper(MdiClient client)
		{
			AssignHandle(client.Handle);
		}

		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case WM_NCCALCSIZE:

					ShowScrollBar(m.HWnd, SB_BOTH, 0);

					break;
			}
			base.WndProc(ref m);
		}

		// Win32 Functions
		[DllImport("user32.dll")]
		private static extern int ShowScrollBar(IntPtr hWnd, int wBar, int bShow);
	}
}