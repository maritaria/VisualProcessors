using System;
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

		protected int m_ThreadSleep = 1;
		private Point m_Location = new Point(0, 0);
		private string m_Name = "Undefined";
		private Size m_Size = new Size(0, 0);
		private Thread m_WorkerThread;
		private Pipeline m_Pipeline;
		private ProcessorOptions m_Options = new ProcessorOptions();
		public ProcessorOptions Options
		{
			get
			{
				return m_Options;
			}
		}
		public Pipeline Pipeline
		{
			get
			{
				return m_Pipeline;
			}
		}


		public bool IsPrepared { get; protected set; }

		public bool IsRunning
		{
			get
			{
				return (m_WorkerThread != null) && m_WorkerThread.IsAlive;
			}
		}

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

		#region ProcessorForm

		/// <summary>
		///  Gets or sets the Height component of the processors Size property
		/// </summary>
		public int Height
		{
			get
			{
				return Size.Height;
			}
			set
			{
				Size = new Size(Size.Width, value);
			}
		}

		/// <summary>
		///  Gets or sets the location of the processor in the model
		/// </summary>
		public Point Location
		{
			get
			{
				return m_Location;
			}
			set
			{
				m_Location = value;
				OnLocationChanged();
			}
		}
		/// <summary>
		///  Gets or sets the size of the processor's form in the model
		/// </summary>
		public Size Size
		{
			get
			{
				return m_Size;
			}
			set
			{
				m_Size = value;
				OnSizeChanged();
			}
		}

		/// <summary>
		///  Gets or sets the Width component of the processors Size property
		/// </summary>
		public int Width
		{
			get
			{
				return Size.Width;
			}
			set
			{
				Size = new Size(value, Size.Height);
			}
		}

		/// <summary>
		///  Gets or sets the X component of the processors Location property
		/// </summary>
		public int X
		{
			get
			{
				return Location.X;
			}
			set
			{
				Location = new Point(value, Location.Y);
				OnModified();
			}
		}

		/// <summary>
		///  Gets or sets the Y component of the processors Location property
		/// </summary>
		public int Y
		{
			get
			{
				return Location.Y;
			}
			set
			{
				Location = new Point(Location.X, value);
			}
		}
		#endregion

		#region Meta

		public bool AllowOptionalInputs
		{
			get
			{
				ProcessorAttribute attr = Meta;
				if (attr == null)
				{
					return false;
				}
				return attr.AllowOptionalInputs;
			}
		}

		public bool AllowUserSpawn
		{
			get
			{
				ProcessorAttribute attr = Meta;
				if (attr == null)
				{
					return true;
				}
				return attr.AllowUserSpawn;
			}
		}

		public string Author
		{
			get
			{
				ProcessorAttribute attr = Meta;
				if (attr == null)
				{
					return null;
				}
				return attr.Author;
			}
		}

		public string DefaultInput
		{
			get
			{
				ProcessorAttribute attr = Meta;
				if (attr == null)
				{
					return null;
				}
				return attr.DefaultInput;
			}
		}

		public string DefaultOutput
		{
			get
			{
				ProcessorAttribute attr = Meta;
				if (attr == null)
				{
					return null;
				}
				return attr.DefaultOutput;
			}
		}

		public string Description
		{
			get
			{
				ProcessorAttribute attr = Meta;
				if (attr == null)
				{
					return null;
				}
				return attr.Description;
			}
		}

		public bool HasMeta
		{
			get
			{
				return ((ProcessorAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof(ProcessorAttribute)) != null);
			}
		}

		public bool HideInputTab
		{
			get
			{
				ProcessorAttribute attr = Meta;
				if (attr == null)
				{
					return false;
				}
				return attr.HideInputTab;
			}
		}

		public bool HideOutputTab
		{
			get
			{
				ProcessorAttribute attr = Meta;
				if (attr == null)
				{
					return false;
				}
				return attr.HideOutputTab;
			}
		}

		public bool HideSettingsTab
		{
			get
			{
				ProcessorAttribute attr = Meta;
				if (attr == null)
				{
					return false;
				}
				return attr.HideSettingsTab;
			}
		}

		public ProcessorAttribute Meta
		{
			get
			{
				ProcessorAttribute attr = (ProcessorAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof(ProcessorAttribute));
				return (attr != null) ? attr : ProcessorAttribute.Default;
			}
		}

		#endregion Meta

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
		public Processor(Pipeline pipeline, string name)
		{
			m_Pipeline = pipeline;
			m_Name = name;
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
		public void Reset()
		{
			if (IsRunning)
			{
				Stop();
			}
			IsPrepared = false;
		}
		public virtual void Start()
		{
			if (m_WorkerThread != null)
			{
				Stop();
			}
			if (!IsPrepared)
			{
				Prepare();
				IsPrepared = true;
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

		protected virtual void Prepare()
		{
			if (IsRunning)
			{
				Stop();
			}
			foreach (InputChannel channel in m_InputChannels)
			{
				channel.Clear();
			}
		}

		protected virtual void Process()
		{
			//Read
			//Parse
			//Write
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
				Thread.Sleep(m_ThreadSleep);
			}
		}

		#endregion Methods

		#region InputChannels

		private List<InputChannel> m_InputChannels = new List<InputChannel>();

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
			m_Pipeline = pipeline;
			m_InputChannels.Clear();
			m_OutputChannels.Clear();
			foreach (InputChannel input in m_RawInput)
			{
				input.Build(pipeline);
				AddInputChannel(input);
			}
			m_RawInput.Clear();
			foreach (OutputChannel output in m_RawOutput)
			{
				output.Build(pipeline);
				AddOutputChannel(output);
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
		///  Invoked when the location of the processor has been changed
		/// </summary>
		public event EventHandler LocationChanged;

		/// <summary>
		///  Invoked by the processor if any part of the processor has changed
		/// </summary>
		public event EventHandler Modified;

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
		
		/// <summary>
		///  Invoked when the size of the processor has been changed
		/// </summary>
		public event EventHandler SizeChanged;

		public event Action<Processor,string> Warning;
		public event Action<Processor, string> Error;

		protected void OnModified()
		{
			if (Modified != null)
			{
				Modified(this, EventArgs.Empty);
			}
		}

		protected void OnWarning(string message)
		{
			if (Warning!=null)
			{
				Warning(this, message);
			}
		}
		protected void OnError(string message)
		{
			if (Error!=null)
			{
				Error(this, message);
			}
		}

		private void OnInputChannelAdded()
		{
			if (InputChannelAdded != null)
			{
				InputChannelAdded(this, EventArgs.Empty);
			}
			OnModified();
		}

		private void OnInputChannelRemoved()
		{
			if (InputChannelRemoved != null)
			{
				InputChannelRemoved(this, EventArgs.Empty);
			}
			OnModified();
		}

		private void OnLinkAdded()
		{
			if (LinkAdded != null)
			{
				LinkAdded(this, EventArgs.Empty);
			}
			OnModified();
		}

		private void OnLinkRemoved()
		{
			if (LinkRemoved != null)
			{
				LinkRemoved(this, EventArgs.Empty);
			}
			OnModified();
		}

		private void OnLocationChanged()
		{
			if (LocationChanged != null)
			{
				LocationChanged(this, EventArgs.Empty);
			}
			OnModified();
		}

		private void OnNameChanged(string oldname, string newname)
		{
			if (NameChanged != null)
			{
				NameChanged(this, oldname, newname);
			}
			OnModified();
		}

		private void OnOutputChannelAdded()
		{
			if (OutputChannelAdded != null)
			{
				OutputChannelAdded(this, EventArgs.Empty);
			}
			OnModified();
		}

		private void OnOutputChannelRemoved()
		{
			if (OutputChannelRemoved != null)
			{
				OutputChannelRemoved(this, EventArgs.Empty);
			}
			OnModified();
		}

		private void OnSizeChanged()
		{
			if (SizeChanged != null)
			{
				SizeChanged(this, EventArgs.Empty);
			}
			OnModified();
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

		private void input_InputCompleted(object sender, EventArgs e)
		{
			Name = (sender as StringInputPanel).InputText;
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
			m_Location = (Point)new XmlSerializer(m_Location.GetType(), GetOverrides(m_Location.GetType(), "Location")).Deserialize(reader);
			m_Size = (Size)new XmlSerializer(m_Size.GetType(), GetOverrides(m_Size.GetType(), "Size")).Deserialize(reader);
			m_Options = (ProcessorOptions)new XmlSerializer(m_Options.GetType(), GetOverrides(m_Options.GetType(), "Options")).Deserialize(reader);
			reader.ReadEndElement();
		}

		public virtual void WriteXml(XmlWriter writer)
		{
			writer.WriteAttributeString("Name", Name);
			XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
			ns.Add("", "");
			new XmlSerializer(m_InputChannels.GetType(), GetOverrides(m_InputChannels.GetType(), "InputChannels")).Serialize(writer, m_InputChannels, ns);
			new XmlSerializer(m_OutputChannels.GetType(), GetOverrides(m_OutputChannels.GetType(), "OutputChannels")).Serialize(writer, m_OutputChannels, ns);
			new XmlSerializer(m_Location.GetType(), GetOverrides(m_Location.GetType(), "Location")).Serialize(writer, m_Location, ns);
			new XmlSerializer(m_Size.GetType(), GetOverrides(m_Size.GetType(), "Size")).Serialize(writer, m_Size, ns);
			new XmlSerializer(m_Options.GetType(), GetOverrides(m_Options.GetType(), "Options")).Serialize(writer, m_Options, ns);
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

		public virtual void Dispose()
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