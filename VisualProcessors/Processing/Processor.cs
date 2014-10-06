﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using VisualProcessors.Controls;

namespace VisualProcessors.Processing
{
	public class Processor : IXmlSerializable, IDisposable
	{
		#region Properties

		private string m_Name = "Undefined";
		private Thread m_WorkerThread;

		public virtual bool AllowOptionalChannels
		{
			get
			{
				return false;
			}
		}

		public bool IsRunning
		{
			get
			{
				return (m_WorkerThread != null) && m_WorkerThread.IsAlive;
			}
		}

		/// <summary>
		///  Gets or sets the location of the processor in the model
		/// </summary>
		public Point Location { get; set; }

		/// <summary>
		///  Gets or sets the name of the processor, must be unique within the pipeline
		/// </summary>
		public string Name
		{
			get
			{
				return m_Name;
			}
			set
			{
				string old = m_Name;
				m_Name = value;
				OnNameChanged(old, value);
			}
		}

		#endregion Properties

		#region Constructor

		/// <summary>
		///  Please do not use, only for serialization support.
		/// </summary>
		public Processor()
		{
		}

		/// <summary>
		///  Creates a new processor
		/// </summary>
		/// <param name="name">
		///  The name of the processor, must be unique within the pipeline the processor belongs to.
		/// </param>
		public Processor(string name)
		{
			Name = name;
		}

		~Processor()
		{
			Stop();
		}

		#endregion Constructor

		#region Methods

		public virtual void GetUserInterface(Panel panel)
		{
			StringInputPanel input = new StringInputPanel();
			input.InputCompleted += input_InputCompleted;
			input.InputTitle = "Name:";
			input.Dock = DockStyle.Top;
			panel.Controls.Add(input);
		}

		public virtual void Start()
		{
			if (m_WorkerThread != null)
			{
				Stop();
			}
			m_WorkerThread = new Thread(new ThreadStart(WorkerMethod));
			m_WorkerThread.IsBackground = true;
			m_WorkerThread.Start();
		}

		public virtual void Stop()
		{
			if (m_WorkerThread == null)
			{
				return;
			}
			if (m_WorkerThread.IsAlive)
			{
				m_WorkerThread.Abort();
				while (m_WorkerThread.IsAlive) ;
			}
			m_WorkerThread = null;
		}

		protected virtual void Process()
		{
		}

		protected virtual void WorkerMethod()
		{
			while (true)
			{
				bool ready = true;
				foreach (InputChannel input in m_InputChannels)
				{
					if (!input.IsOptional)
					{
						ready &= input.HasData();
					}
				}
				if (ready)
				{
					Process();
				}
				Thread.Sleep(1);
			}
		}

		private void input_InputCompleted(object sender, EventArgs e)
		{
			this.Name = (sender as StringInputPanel).InputText;
		}

		#endregion Methods

		#region InputChannels

		private List<InputChannel> m_InputChannels = new List<InputChannel>();

		public bool HasInputChannels
		{
			get
			{
				return m_InputChannels.Count > 0;
			}
		}

		public InputChannel GetInputChannel(string name)
		{
			foreach (InputChannel channel in m_InputChannels)
			{
				if (channel.Name == name)
				{
					return channel;
				}
			}
			return null;
		}

		public string[] GetInputChannelNames()
		{
			List<string> names = new List<string>();
			foreach (InputChannel channel in m_InputChannels)
			{
				names.Add(channel.Name);
			}
			return names.ToArray();
		}

		protected void AddInputChannel(string name, bool optional)
		{
			if (IsRunning)
			{
				throw new InvalidOperationException("Cannot modify input/output channels while running");
			}
			if (!AllowOptionalChannels)
			{
				optional = false;
			}
			if (GetInputChannel(name) == null)
			{
				AddInputChannel(new InputChannel(this, name, optional));
			}
		}

		protected void RemoveInputChannel(InputChannel channel)
		{
			if (IsRunning)
			{
				throw new InvalidOperationException("Cannot modify input/output channels while running");
			}
			if (m_InputChannels.Contains(channel))
			{
				channel.Unlink();
				channel.SourceChanged -= channel_SourceChanged;
				m_InputChannels.Remove(channel);
				OnInputChannelRemoved();
			}
		}

		private void AddInputChannel(InputChannel channel)
		{
			if (IsRunning)
			{
				throw new InvalidOperationException("Cannot modify input/output channels while running");
			}
			if (this == channel.Owner && !m_InputChannels.Contains(channel))
			{
				channel.SourceChanged += channel_SourceChanged;
				m_InputChannels.Add(channel);
				OnInputChannelAdded();
			}
		}

		#endregion InputChannels

		#region OutputChannels

		private List<OutputChannel> m_OutputChannels = new List<OutputChannel>();

		public bool HasOutputChannels
		{
			get
			{
				return m_OutputChannels.Count > 0;
			}
		}

		public OutputChannel GetOutputChannel(string name)
		{
			foreach (OutputChannel channel in m_OutputChannels)
			{
				if (channel.Name == name)
				{
					return channel;
				}
			}
			return null;
		}

		public string[] GetOutputChannelNames()
		{
			List<string> names = new List<string>();
			foreach (OutputChannel channel in m_OutputChannels)
			{
				names.Add(channel.Name);
			}
			return names.ToArray();
		}

		protected void AddOutputChannel(string name)
		{
			if (IsRunning)
			{
				throw new InvalidOperationException("Cannot modify input/output channels while running");
			}
			if (GetInputChannel(name) == null)
			{
				AddOutputChannel(new OutputChannel(this, name));
			}
		}

		protected void RemoveOutputChannel(OutputChannel channel)
		{
			if (IsRunning)
			{
				throw new InvalidOperationException("Cannot modify input/output channels while running");
			}
			if (m_OutputChannels.Contains(channel))
			{
				channel.Unlink();
				channel.OutputAdded -= channel_OutputAdded;
				channel.OutputRemoved -= channel_OutputRemoved;
				m_OutputChannels.Remove(channel);
			}
		}

		private void AddOutputChannel(OutputChannel channel)
		{
			if (IsRunning)
			{
				throw new InvalidOperationException("Cannot modify input/output channels while running");
			}
			if (this == channel.Owner && !m_OutputChannels.Contains(channel))
			{
				channel.OutputAdded += channel_OutputAdded;
				channel.OutputRemoved += channel_OutputRemoved;
				m_OutputChannels.Add(channel);
			}
		}

		#endregion OutputChannels

		#region Build

		private List<InputChannel> m_RawInput = new List<InputChannel>();
		private List<OutputChannel> m_RawOutput = new List<OutputChannel>();

		internal void Build(Pipeline pipeline)
		{
			m_InputChannels.Clear();
			m_OutputChannels.Clear();
			foreach (InputChannel input in m_RawInput)
			{
				AddInputChannel(input);
				input.Build(pipeline);
			}
			m_RawInput.Clear();
			foreach (OutputChannel output in m_RawOutput)
			{
				m_OutputChannels.Add(output);
				output.Build(pipeline);
			}
			m_RawOutput.Clear();
		}

		internal void BuildLinks(Pipeline pipeline)
		{
			foreach (OutputChannel channel in m_OutputChannels)
			{
				channel.BuildLinks(pipeline);
			}
		}

		#endregion Build

		#region Events

		/// <summary>
		///  Invoked after an InputChannel has been added
		/// </summary>
		public event EventHandler InputChannelAdded;

		/// <summary>
		///  Invoked after an InputChannel has been safely removed
		/// </summary>
		public event EventHandler InputChannelRemoved;

		/// <summary>
		///  Invoked after an Input or Output has been linked
		/// </summary>
		public event EventHandler LinkAdded;

		/// <summary>
		///  Invoked after an Input or Output has been unlinked
		/// </summary>
		public event EventHandler LinkRemoved;

		/// <summary>
		///  Invoked just after the name of the processor has changed
		/// </summary>
		public event ProcessorNameChanged NameChanged;

		/// <summary>
		///  Invoked after an OutputChannel has been added
		/// </summary>
		public event EventHandler OutputChannelAdded;

		/// <summary>
		///  Invoked after an OutputChannel has safely removed
		/// </summary>
		public event EventHandler OutputChannelRemoved;

		private void OnInputChannelAdded()
		{
			if (InputChannelAdded != null)
			{
				InputChannelAdded(this, EventArgs.Empty);
			}
		}

		private void OnInputChannelRemoved()
		{
			if (InputChannelRemoved != null)
			{
				InputChannelRemoved(this, EventArgs.Empty);
			}
		}

		private void OnLinkAdded()
		{
			if (LinkAdded != null)
			{
				LinkAdded(this, EventArgs.Empty);
			}
		}

		private void OnLinkRemoved()
		{
			if (LinkRemoved != null)
			{
				LinkRemoved(this, EventArgs.Empty);
			}
		}

		private void OnNameChanged(string oldname, string newname)
		{
			if (NameChanged != null)
			{
				NameChanged(this, oldname, newname);
			}
		}

		private void OnOutputChannelAdded()
		{
			if (OutputChannelAdded != null)
			{
				OutputChannelAdded(this, EventArgs.Empty);
			}
		}

		private void OnOutputChannelRemoved()
		{
			if (OutputChannelRemoved != null)
			{
				OutputChannelRemoved(this, EventArgs.Empty);
			}
		}

		#endregion Events

		#region Event Handlers

		private void channel_OutputAdded(object sender, EventArgs e)
		{
			OnLinkAdded();
		}

		private void channel_OutputRemoved(object sender, EventArgs e)
		{
			OnLinkRemoved();
		}

		private void channel_SourceChanged(object sender, EventArgs e)
		{
			InputChannel channel = sender as InputChannel;
			OnLinkRemoved();
			if (channel.Source != null)
			{
				OnLinkAdded();
			}
		}

		#endregion Event Handlers

		#region IXmlSerializable Members

		public XmlSchema GetSchema()
		{
			return null;
		}

		public virtual void ReadXml(XmlReader reader)
		{
			Name = reader.GetAttribute("Name");
			reader.Read();
			m_RawInput = (List<InputChannel>)new XmlSerializer(m_InputChannels.GetType(), GetOverrides(m_InputChannels.GetType(), "InputChannels")).Deserialize(reader);
			m_RawOutput = (List<OutputChannel>)new XmlSerializer(m_OutputChannels.GetType(), GetOverrides(m_OutputChannels.GetType(), "OutputChannels")).Deserialize(reader);
			reader.ReadEndElement();
		}

		public virtual void WriteXml(XmlWriter writer)
		{
			writer.WriteAttributeString("Name", Name);
			XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
			ns.Add("", "");
			new XmlSerializer(m_InputChannels.GetType(), GetOverrides(m_InputChannels.GetType(), "InputChannels")).Serialize(writer, m_InputChannels, ns);
			new XmlSerializer(m_OutputChannels.GetType(), GetOverrides(m_OutputChannels.GetType(), "OutputChannels")).Serialize(writer, m_OutputChannels, ns);
		}

		private XmlAttributeOverrides GetOverrides(Type t, string name)
		{
			XmlAttributeOverrides overrides = new XmlAttributeOverrides();
			XmlAttributes attribute = new XmlAttributes();
			attribute.XmlRoot = new XmlRootAttribute(name);
			attribute.Xmlns = false;
			overrides.Add(t, attribute);
			return overrides;
		}

		#endregion IXmlSerializable Members

		#region IDisposable Members

		public void Dispose()
		{
			foreach (InputChannel input in m_InputChannels)
			{
				input.Unlink();
			}
			foreach (OutputChannel output in m_OutputChannels)
			{
				output.Unlink();
			}
		}

		#endregion IDisposable Members
	}
}