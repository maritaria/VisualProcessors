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

		[DllImport("user32.dll")]
		private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		[DllImport("user32.dll")]
		private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		[DllImport("user32.dll", ExactSpelling = true)]
		private static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

		private const int GWL_EXSTYLE = -20;
		private const int WS_EX_CLIENTEDGE = 0x200;
		private const uint SWP_NOSIZE = 0x0001;
		private const uint SWP_NOMOVE = 0x0002;
		private const uint SWP_NOZORDER = 0x0004;
		private const uint SWP_NOREDRAW = 0x0008;
		private const uint SWP_NOACTIVATE = 0x0010;
		private const uint SWP_FRAMECHANGED = 0x0020;
		private const uint SWP_SHOWWINDOW = 0x0040;
		private const uint SWP_HIDEWINDOW = 0x0080;
		private const uint SWP_NOCOPYBITS = 0x0100;
		private const uint SWP_NOOWNERZORDER = 0x0200;
		private const uint SWP_NOSENDCHANGING = 0x0400;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="form"></param>
		/// <param name="show"></param>
		/// <returns></returns>
		/// <see cref="http://stackoverflow.com/questions/7752696/how-to-remove-3d-border-sunken-from-mdiclient-component-in-mdi-parent-form"/>
		public static bool SetBevel(this Form form, bool show)
		{
			foreach (Control c in form.Controls)
			{
				MdiClient client = c as MdiClient;
				if (client != null)
				{
					int windowLong = GetWindowLong(c.Handle, GWL_EXSTYLE);

					if (show)
					{
						windowLong |= WS_EX_CLIENTEDGE;
					}
					else
					{
						windowLong &= ~WS_EX_CLIENTEDGE;
					}

					SetWindowLong(c.Handle, GWL_EXSTYLE, windowLong);

					// Update the non-client area.
					SetWindowPos(client.Handle, IntPtr.Zero, 0, 0, 0, 0,
						SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER |
						SWP_NOOWNERZORDER | SWP_FRAMECHANGED);

					return true;
				}
			}
			return false;
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