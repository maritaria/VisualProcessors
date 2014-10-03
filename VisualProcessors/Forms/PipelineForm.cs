using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualProcessors;
using VisualProcessors.Controls;
using VisualProcessors.Forms;
using VisualProcessors.Processing;

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

	public partial class PipelineForm : Form
	{
		#region Properties

		private bool m_HideScrollBars = false;
		private MdiClient m_MdiClient;
		private MdiClientHelper m_MdiHelper = new MdiClientHelper();
		private Pipeline m_Pipeline;
		private ToolboxForm m_Toolbox;

		/// <summary>
		///  Hides the scrollbars of the MdiClient, there may be a minor flicker when disabled
		/// </summary>
		public bool MdiHideScrollBars
		{
			get
			{
				return m_HideScrollBars;
			}
			set
			{
				m_HideScrollBars = value;
			}
		}

		#endregion Properties

		#region Constructor

		public PipelineForm(Pipeline pipeline)
		{
			m_Pipeline = pipeline;
			InitializeComponent();

			//Initialize MdiClient
			foreach (Control c in Controls)
			{
				if (c is MdiClient)
				{
					m_MdiClient = (MdiClient)c;
					break;
				}
			}
			m_MdiClient.Paint += MdiClientPaintLinks;
			m_MdiClient.MouseDown += MdiClientMouseDown;
			m_MdiClient.MouseUp += MdiClientMouseUp;
			m_MdiClient.MouseMove += MdiClientMouseMove;
			m_MdiHelper.AssignHandle(m_MdiClient.Handle);
			m_MdiClient.PerformLayout();

			//Add existing Processors of the Pipeline instance
			foreach (string name in pipeline.GetListOfNames())
			{
				AddProcessor(pipeline.GetByName(name));
			}

			//Show toolbox
			m_Toolbox = new ToolboxForm(this);
			m_Toolbox.MdiParent = this;
			m_Toolbox.Show();
		}

		#endregion Constructor

		#region Methods

		/// <summary>
		///  Forces the MDI to redraw itself, usefull for forcing a redraw when a link is added or
		///  removed
		/// </summary>
		public void InvalidateMdi()
		{
			m_MdiClient.Invalidate();
		}

		private Point CalculateChannelLink(Form a, Point end)
		{
			Point result = new Point();
			Point start = a.GetCenter();
			Point delta = new Point(end.X - start.X, end.Y - start.Y);
			Point absdelta = new Point(Math.Abs(end.X - start.X), Math.Abs(end.Y - start.Y));
			if (absdelta.X > absdelta.Y)
			{
				if (delta.X > 0)
				{
					//A is left of B
					result = new Point(a.Right, (a.Top + a.Bottom) / 2);
				}
				else
				{
					//A is right of B
					result = new Point(a.Left, (a.Top + a.Bottom) / 2);
				}
			}
			else
			{
				if (delta.Y > 0)
				{
					//A is below B
					result = new Point((a.Left + a.Right) / 2, a.Bottom);
				}
				else
				{
					//A is above B
					result = new Point((a.Left + a.Right) / 2, a.Top);
				}
			}

			return result;
		}

		#endregion Methods

		#region ProcessorForm Control

		private List<ProcessorForm> m_ProcessorForms = new List<ProcessorForm>();

		/// <summary>
		///  Adds a new processor to the pipeline, creates a ProcessorForm for it, and shows the
		///  form on the MDIClient
		/// </summary>
		/// <param name="p">The processor to add to the underlaying pipeline</param>
		public void AddProcessor(Processor p)
		{
			if (GetProcessorForm(p.Name) == null)
			{
				ProcessorForm f = new ProcessorForm(this, p);
				m_ProcessorForms.Add(f);
				AddOwnedForm(f);
				f.MdiParent = this;
				f.Show();
				ActivateMdiChild(f);
			}
			if (!m_Pipeline.IsProcessorNameTaken(p.Name))
			{
				m_Pipeline.AddProcessor(p);
			}
		}

		/// <summary>
		///  Gets the form that owns a named processor
		/// </summary>
		/// <param name="name">The name of the processor</param>
		/// <returns>The form that owns the processor, or null if none is found</returns>
		public ProcessorForm GetProcessorForm(string name)
		{
			foreach (ProcessorForm pf in m_ProcessorForms)
			{
				if (pf.Processor.Name == name)
				{
					return pf;
				}
			}
			return null;
		}

		/// <summary>
		///  Hides a ProcessorForm by making it invisible
		/// </summary>
		/// <param name="name">The name of the processor that has to be hidden</param>
		public void HideProcessor(string name)
		{
			ProcessorForm pf = GetProcessorForm(name);
			if (pf != null)
			{
				pf.Visible = false;
			}
		}

		/// <summary>
		///  Removes a processor from the pipeline
		/// </summary>
		/// <param name="pform">The name of the processor that has to be remoed</param>
		/// <returns>True when succesfull, false otherwise</returns>
		public bool RemoveProcessor(string name)
		{
			ProcessorForm pf = GetProcessorForm(name);
			if (pf == null)
			{
				return false;
			}

			pf.Processor.Dispose();
			bool result = m_ProcessorForms.Remove(pf);
			pf.Close();
			pf.Dispose();
			return result;
		}

		public void ShowInputChannel(string processorname, string channelname)
		{
			ProcessorForm pf = GetProcessorForm(processorname);
			if (pf != null)
			{
				pf.ShowInputChannel(channelname);
			}
		}

		/// <summary>
		///  Shows a hidden ProcessorForm, also gives the form focus
		/// </summary>
		/// <param name="name"></param>
		public void ShowProcessor(string name)
		{
			ProcessorForm pf = GetProcessorForm(name);
			pf.Visible = true;
			pf.Focus();
		}

		#endregion ProcessorForm Control

		#region Processor Linking

		private string m_LinkChannelName;
		private LinkMode m_LinkMode = LinkMode.Disabled;
		private ProcessorForm m_LinkStart;

		/// <summary>
		///  Completes the current linking session, if the linkmode is set to OutputFirst, this will
		///  also show a dialog box to the user to select the InputChannel to use. When the linkmode
		///  is set to InputFirst, no dialog box is shown because the InputChannel was selected at
		///  the start of the linking session.
		/// </summary>
		/// <param name="second">The ProcessorForm that contains the endpoint of the link</param>
		public void CompleteLinkMode(ProcessorForm second)
		{
			ChannelSelectionBox csbox = new ChannelSelectionBox((m_LinkMode == LinkMode.InputFirst) ? ChannelType.OutputChannel : ChannelType.InputChannel, second.Processor);
			if (csbox.ShowDialog(this).HasFlag(DialogResult.OK))
			{
				InputChannel input = null;
				OutputChannel output = null;
				if (m_LinkMode == LinkMode.InputFirst)
				{
					input = m_LinkStart.Processor.GetInputChannel(m_LinkChannelName);
					output = second.Processor.GetOutputChannel(csbox.Choice);
				}
				else
				{
					input = second.Processor.GetInputChannel(csbox.Choice);
					output = m_LinkStart.Processor.GetOutputChannel(m_LinkChannelName);
				}
				if (input == null)
				{
					MessageBox.Show("Error: The InputChannel was not found!");
					return;
				}
				if (output == null)
				{
					MessageBox.Show("Error: The InputChannel was not found!");
					return;
				}
				output.Link(input);
			}

			ResetLinkMode();
			m_MdiClient.Invalidate();
		}

		/// <summary>
		///  Stops the current linking session if one exists
		/// </summary>
		public void ResetLinkMode()
		{
			foreach (ProcessorForm pform in m_ProcessorForms)
			{
				pform.ShowLinkButton = false;
			}
			m_LinkMode = LinkMode.Disabled;
		}

		public void StartLinkMode(ProcessorForm first, string channelname, LinkMode mode)
		{
			if (m_LinkMode != LinkMode.Disabled)
			{
				ResetLinkMode();
			}
			if (mode == LinkMode.Disabled)
			{
				return;
			}
			foreach (ProcessorForm pform in m_ProcessorForms)
			{
				pform.ShowLinkButton = (pform != first);
			}
			m_LinkStart = first;
			m_LinkChannelName = channelname;
			m_LinkMode = mode;
		}

		#endregion Processor Linking

		#region EventHandlers

		private bool m_IsDragging = false;
		private Point m_PreviousDragPosition;

		private void MdiClientMouseDown(object sender, MouseEventArgs e)
		{
			m_IsDragging = true;
			m_PreviousDragPosition = e.Location;
			Cursor = Cursors.Hand;
		}

		private void MdiClientMouseMove(object sender, MouseEventArgs e)
		{
			if (!m_IsDragging)
			{
				return;
			}
			m_MdiClient.SuspendLayout();
			Point offset = new Point(e.Location.X - m_PreviousDragPosition.X, e.Location.Y - m_PreviousDragPosition.Y);
			foreach (ProcessorForm pf in m_ProcessorForms)
			{
				Point pos = pf.Location;
				pos.Offset(offset);
				pf.Location = pos;
			}
			m_MdiClient.ResumeLayout(true);
			m_PreviousDragPosition.Offset(offset);
		}

		private void MdiClientMouseUp(object sender, MouseEventArgs e)
		{
			m_IsDragging = false;
			Cursor = Cursors.Default;
		}

		private void MdiClientPaintLinks(object sender, PaintEventArgs e)
		{
			//Create buffer
			BufferedGraphicsContext currentContext;
			BufferedGraphics myBuffer;
			currentContext = BufferedGraphicsManager.Current;
			myBuffer = currentContext.Allocate(m_MdiClient.CreateGraphics(), m_MdiClient.DisplayRectangle);
			myBuffer.Graphics.Clear(m_MdiClient.BackColor);
			Pen outputhalf = new Pen(Color.Red, 4);
			Pen inputhalf = new Pen(Color.Green, 4);
			Pen unused = new Pen(Color.Gray, 4);
			System.Drawing.Font font = new System.Drawing.Font("Arial", 14, FontStyle.Bold);
			foreach (ProcessorForm startform in m_ProcessorForms)
			{
				if (!startform.Visible)
				{
					continue;
				}
				foreach (string outputname in startform.Processor.GetOutputChannelNames())
				{
					OutputChannel outputchannel = startform.Processor.GetOutputChannel(outputname);
					foreach (InputChannel inputchannel in outputchannel.Targets)
					{
						if (outputchannel.Owner != inputchannel.Owner)
						{
							Form endform = GetProcessorForm(inputchannel.Owner.Name);
							Point lineoutput = CalculateChannelLink(startform, endform.GetCenter());
							Point lineinput = CalculateChannelLink(endform, lineoutput);
							Point halfway = new Point((lineoutput.X + lineinput.X) / 2, (lineoutput.Y + lineinput.Y) / 2);
							myBuffer.Graphics.DrawLine(outputhalf, lineoutput, halfway);
							myBuffer.Graphics.DrawLine((inputchannel.IsConstant) ? unused : inputhalf, halfway, lineinput);
							string str1 = "(" + outputchannel.Name + ")";
							string str2 = "(" + inputchannel.Name + ")";
							SizeF size1 = myBuffer.Graphics.MeasureString(str1, font);
							SizeF size2 = myBuffer.Graphics.MeasureString(str2, font);
							float height = size1.Height + size2.Height;
							myBuffer.Graphics.DrawString(str1, font, new SolidBrush(outputhalf.Color), halfway.X - size1.Width / 2, halfway.Y - height / 2);
							myBuffer.Graphics.DrawString(str2, font, new SolidBrush((inputchannel.IsConstant) ? unused.Color : inputhalf.Color), halfway.X - size2.Width / 2, halfway.Y + size2.Height - height / 2);
						}
					}
				}
			}

			//Render
			myBuffer.Render();
			myBuffer.Dispose();
		}

		private void PipelineFormShown(object sender, EventArgs e)
		{
			System.Threading.Thread.Sleep(100);
			int row = 5;
			for (int i = 0; i < m_ProcessorForms.Count; i++)
			{
				var pf = m_ProcessorForms[i];

				//pf.ToggleMinimalView();
				pf.Location = new Point(10 + (i % row) * 550, 10 + 210 * (i / row));
			}
		}

		#endregion EventHandlers

		#region Interop

		#endregion Interop
	}

	internal class MdiClientHelper : NativeWindow
	{
		private const int SB_BOTH = 3;
		private const int SB_CTL = 2;
		private const int SB_HORZ = 0;
		private const int SB_VERT = 1;
		private const int WM_NCCALCSIZE = 0x0083;

		public MdiClientHelper()
		{
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