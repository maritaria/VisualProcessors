using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualProcessors.Forms
{
	public enum LinkMode
	{
		/// <summary>
		///  User selects the output first, and then the input target
		/// </summary>
		OutputFirst,

		/// <summary>
		///  User selected the input target first, then the source
		/// </summary>
		InputFirst,

		/// <summary>
		///  Disable linkmode
		/// </summary>
		Disabled,
	}
}
