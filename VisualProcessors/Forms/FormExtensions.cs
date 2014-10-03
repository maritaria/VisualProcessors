using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
}