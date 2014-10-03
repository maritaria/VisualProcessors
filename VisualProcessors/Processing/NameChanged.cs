using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualProcessors.Processing
{
	public delegate void InputChannelNameChanged(InputChannel channel, string oldname, string newname);

	public delegate void OutputChannelNameChanged(OutputChannel channel, string oldname, string newname);

	public delegate void ProcessorNameChanged(Processor processor, string oldname, string newname);
}