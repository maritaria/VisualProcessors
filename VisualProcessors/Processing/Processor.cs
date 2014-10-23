using System;
using System.Collections.Generic;
using System.ComponentModel;
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

		private Point m_Location = new Point(0, 0);
		private string m_Name = "Undefined";
		private ProcessorOptions m_Options = new ProcessorOptions();
		private Pipeline m_Pipeline;
		private Size m_Size = new Size(0, 0);
		private Thread m_WorkerThread;

		/// <summary>
		///  Gets whether the processor is prepared for processing, if this value is false, the
		///  Prepare() function will run when the processor is started.
		/// </summary>
		[Browsable(true)]
		[ReadOnly(true)]
		[DisplayName("Prepared")]
		[Category("Processor")]
		[Description("Is processor ready for simulation")]
		[DefaultValue(true)]
		public bool IsPrepared { get; protected set; }

		/// <summary>
		///  Gets whether the processor has a thread running for processing data.
		/// </summary>
		[Browsable(true)]
		[ReadOnly(true)]
		[DisplayName("Running")]
		[Category("Processor")]
		[Description("Is processor running in a simulation")]
		[DefaultValue(false)]
		public bool IsRunning
		{
			get
			{
				return (m_WorkerThread != null) && m_WorkerThread.IsAlive;
			}
		}

		/// <summary>
		///  Gets the ProcessorAttribute of this processor
		/// </summary>
		[Browsable(false)]
		public ProcessorMeta Meta
		{
			get
			{
				ProcessorMeta attr = (ProcessorMeta)Attribute.GetCustomAttribute(this.GetType(), typeof(ProcessorMeta));
				return (attr != null) ? attr : ProcessorMeta.Default;
			}
		}

		/// <summary>
		///  Gets or sets the name of the processor, must be unique within the pipeline
		/// </summary>
		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Name")]
		[Category("Processor")]
		[Description("Name of the processor")]
		public string Name
		{
			get
			{
				return m_Name;
			}
			set
			{
				if (Pipeline != null)
				{
					if (value == m_Name)
					{
						return;
					}
					if (Pipeline.IsProcessorNameTaken(value))
					{
						throw new ArgumentException("The name is already taken by another processor");
					}
				}
				string old = m_Name;
				m_Name = value;
				OnNameChanged(old, value);
			}
		}

		/// <summary>
		///  Gets or sets whether the processor uses a seperate thread during simulation
		/// </summary>
		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("MultiThreaded")]
		[Category("Processor")]
		[Description("When true, the processor gets its own thread during simulation, use this only for intensive processors, or if there is a feedback loop in your model (and then only one is required to be multithreaded).")]
		[DefaultValue(false)]
		public bool MultiThreaded
		{
			get
			{
				if (Meta.ThreadMode == ProcessorThreadMode.UserDefined)
				{
					return bool.Parse(Options.GetOption("MultiThreaded", "False"));
				}
				else
				{
					return Meta.ThreadMode == ProcessorThreadMode.ForceMultiThreading;
				}
			}
			set
			{
				if (Meta.ThreadMode == ProcessorThreadMode.UserDefined)
				{
					Options.SetOption("MultiThreaded", value.ToString());
					OnModified(HaltTypes.ShouldHalt);
				}
			}
		}
		/// <summary>
		/// Gets the amount of milliseconds to sleep after a unsuccesfull inputchannel check loop
		/// </summary>
		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("ThreadSleep")]
		[Category("Processor")]
		[Description("Only used when multi-threading is enabled. Sets the amount of milliseconds to sleep after the workermethod has determined not all required inputchannels have data. Allows decimal placements.")]
		[DefaultValue(1)]
		public double ThreadSleep
		{
			get
			{
				return double.Parse(Options.GetOption("ThreadSleep", "1"));
			}
			set
			{
				Options.SetOption("ThreadSleep", value.ToString());
				OnModified(HaltTypes.Continue);
			}
		}

		/// <summary>
		///  Gets the pipeline the processor belongs to.
		/// </summary>
		[Browsable(false)]
		public Pipeline Pipeline
		{
			get
			{
				return m_Pipeline;
			}
		}

		/// <summary>
		///  Gets the options collection for storing data
		/// </summary>
		[Browsable(false)]
		protected ProcessorOptions Options
		{
			get
			{
				return m_Options;
			}
		}

		#region ProcessorForm

		/// <summary>
		///  Gets or sets the Height component of the processors Size property
		/// </summary>
		[Browsable(false)]
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
		[Browsable(true)]
		[ReadOnly(true)]
		[DisplayName("Location")]
		[Category("Processor")]
		[Description("The location of the processor window in the model")]
		[DefaultValue(false)]
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
		[Browsable(true)]
		[ReadOnly(true)]
		[DisplayName("Size")]
		[Category("Processor")]
		[Description("The size of the processor window in the model")]
		[DefaultValue(false)]
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
		[Browsable(false)]
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
		[Browsable(false)]
		public int X
		{
			get
			{
				return Location.X;
			}
			set
			{
				Location = new Point(value, Location.Y);
			}
		}

		/// <summary>
		///  Gets or sets the Y component of the processors Location property
		/// </summary>
		[Browsable(false)]
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

		#endregion ProcessorForm

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
		/// <param name="pipeline">The pipeline the processor belongs to</param>
		/// <param name="name">    
		///  The name of the processor, must be unique within the pipeline
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

		/// <summary>
		///  When implemented in derived type, this will add a GUI specific for the processor to the
		///  panel
		/// </summary>
		/// <param name="panel">
		///  A (clean/empty) panel that will hold the GUI for the processor
		/// </param>
		public virtual void GetUserInterface(Panel panel)
		{
		}

		/// <summary>
		///  Stops the processor if its running, and sets IsPrepared to false, this will cause the
		///  processor to clear its buffer on the next Start() call.
		/// </summary>
		public void Reset()
		{
			if (IsRunning)
			{
				Stop();
			}
			IsPrepared = false;
		}

		/// <summary>
		///  Starts the processors worker thread, also runs Prepare() if IsPrepared is false. If the
		///  processor is already running, it calls Stop() and then starts a new workerthread
		/// </summary>
		public virtual void Start()
		{
			if (MultiThreaded)
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
				m_WorkerThread.Name = "WT " + Name;
				m_WorkerThread.Start();
			}
		}

		/// <summary>
		///  Stops the execution of the processor (if its running). Blocks until the workerthread
		///  has terminated.
		/// </summary>
		public virtual void Stop()
		{
			KillWorkerThread();
		}

		protected void KillWorkerThread()
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

		/// <summary>
		///  Sets the processor to a new/cleared state. On base class: clears InputChannels of
		///  remaining values.
		/// </summary>
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
				SingleWorkerLoop();
			}
		}

		internal void SingleWorkerLoop()
		{
			bool ready = true;
			foreach (InputChannel input in m_InputChannels)
			{
				if (!input.IsOptional)
				{
					ready &= input.HasValue();
				}
			}
			if (ready)
			{
				Process();
			}
			else
			{
				Thread.Sleep(new TimeSpan((long)(TimeSpan.TicksPerMillisecond * ThreadSleep)));
			}
		}

		#endregion Methods

		#region InputChannels

		private List<InputChannel> m_InputChannels = new List<InputChannel>();

		/// <summary>
		///  Gets the number of InputChannels the processor has
		/// </summary>
		[Browsable(false)]
		public int InputChannelCount
		{
			get
			{
				return m_InputChannels.Count;
			}
		}

		/// <summary>
		///  Gets an InputChannel of the Processor by its name, returns null if no InputChannel with
		///  that name exists.
		/// </summary>
		/// <param name="name">The name of the InputChannel</param>
		/// <returns>
		///  The InputChannel whose Name property is equal to the given name argument. Returns null
		///  if no InputChannel was found for the given name
		/// </returns>
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

		/// <summary>
		///  Gets an array of strings containing the names of the InputChannels that the Processor
		///  has.
		/// </summary>
		/// <returns></returns>
		public string[] GetInputChannelNames()
		{
			//Consider: yield return
			List<string> names = new List<string>();
			foreach (InputChannel channel in m_InputChannels)
			{
				names.Add(channel.Name);
			}
			return names.ToArray();
		}

		/// <summary>
		///  Adds a new InputChannel to the processor.
		/// </summary>
		/// <param name="name">    
		///  The name of the InputChannel, used for retrieving it with GetInputChannel(name).
		/// </param>
		/// <param name="optional">
		///  Whether the InputChannel is optional, can be changed with the IsOptional property.
		/// </param>
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

		/// <summary>
		///  Removes an existing InputChannel from the Processor, unlinks it properly first.
		/// </summary>
		/// <param name="channel">The InputChannel instance to remove.</param>
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

		/// <summary>
		///  Gets the number of OutputChannels the processor has
		/// </summary>
		[Browsable(false)]
		public int OutputChannelCount
		{
			get
			{
				return m_OutputChannels.Count;
			}
		}

		/// <summary>
		///  Gets an OutputChannel of the Processor by its name, returns null if no OutputChannel
		///  with that name exists.
		/// </summary>
		/// <param name="name">The name of the OutputChannel</param>
		/// <returns>
		///  The OutputChannel whose Name property is equal to the given name argument. Returns null
		///  if no OutputChannel was found for the given name
		/// </returns>
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

		/// <summary>
		///  Gets an array of strings containing the names of the OutputChannel that the Processor
		///  has.
		/// </summary>
		/// <returns></returns>
		public string[] GetOutputChannelNames()
		{
			List<string> names = new List<string>();
			foreach (OutputChannel channel in m_OutputChannels)
			{
				names.Add(channel.Name);
			}
			return names.ToArray();
		}

		/// <summary>
		///  Adds a new OutputChannel to the processor.
		/// </summary>
		/// <param name="name">
		///  The name of the InputChannel, used for retrieving it with GetOutputChannel(name).
		/// </param>
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

		/// <summary>
		///  Removes an existing OutputChannel from the Processor, unlinks it properly first.
		/// </summary>
		/// <param name="channel">The OutputChannel instance to remove.</param>
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

		/// <summary>
		///  Initializes all Input/OutputChannels that were deserialized from XML.
		/// </summary>
		/// <param name="pipeline"></param>
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

		/// <summary>
		///  Should be called directly after Build(), links the OutputChannels to their targers.
		///  This is called after Build because all the Processors in the Pipeline first need to
		///  have initialized(Build) their InputChannels.
		/// </summary>
		/// <param name="pipeline"></param>
		internal void BuildLinks(Pipeline pipeline)
		{
			foreach (OutputChannel channel in m_OutputChannels)
			{
				channel.BuildLinks(pipeline);
			}
		}
		/// <summary>
		/// Called when the processor has been loaded from a file, after its in- and outputs have been linked.
		/// </summary>
		/// <param name="pipeline"></param>
		public virtual void PostLoad(Pipeline pipeline)
		{

		}

		#endregion Build

		#region Events

		/// <summary>
		///  Invoked when the processor wants to indicate an error.
		/// </summary>
		public event Action<Processor, string> Error;

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
		public event EventHandler<ProcessorModifiedEventArgs> Modified;

		/// <summary>
		///  Invoked just after the name of the processor has changed.
		/// </summary>
		public event ProcessorNameChanged NameChanged;

		/// <summary>
		///  Invoked after an OutputChannel has been added.
		/// </summary>
		public event EventHandler OutputChannelAdded;

		/// <summary>
		///  Invoked after an OutputChannel has safely removed.
		/// </summary>
		public event EventHandler OutputChannelRemoved;

		/// <summary>
		///  Invoked when the size of the processor has been changed.
		/// </summary>
		public event EventHandler SizeChanged;

		/// <summary>
		///  Invoked when the processor wants to indicate a warning.
		/// </summary>
		public event Action<Processor, string> Warning;

		/// <summary>
		///  Invokes the Error event
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		protected void OnError(string message)
		{
			if (Error != null)
			{
				Error(this, message);
			}
		}

		/// <summary>
		///  Invokes the Modified event, tells the pipeline that a simulation should/shouldn't be
		///  halted due to the modification
		/// </summary>
		/// <param name="haltType">Whether and or how to halt the simulation</param>
		protected void OnModified(HaltTypes haltType)
		{
			if (Modified != null)
			{
				Modified(this, new ProcessorModifiedEventArgs(this, haltType));
			}
		}

		/// <summary>
		///  Invokes the Warning event
		/// </summary>
		/// <param name="message">The message that describes the warning</param>
		protected void OnWarning(string message)
		{
			if (Warning != null)
			{
				Warning(this, message);
			}
		}

		private void OnInputChannelAdded()
		{
			if (InputChannelAdded != null)
			{
				InputChannelAdded(this, EventArgs.Empty);
			}
			OnModified(HaltTypes.Ask);
		}

		private void OnInputChannelRemoved()
		{
			if (InputChannelRemoved != null)
			{
				InputChannelRemoved(this, EventArgs.Empty);
			}
			OnModified(HaltTypes.Ask);
		}

		private void OnLinkAdded()
		{
			if (LinkAdded != null)
			{
				LinkAdded(this, EventArgs.Empty);
			}
			OnModified(HaltTypes.ShouldHalt);
		}

		private void OnLinkRemoved()
		{
			if (LinkRemoved != null)
			{
				LinkRemoved(this, EventArgs.Empty);
			}
			OnModified(HaltTypes.ShouldHalt);
		}

		private void OnLocationChanged()
		{
			if (LocationChanged != null)
			{
				LocationChanged(this, EventArgs.Empty);
			}
			OnModified(HaltTypes.Continue);
		}

		private void OnNameChanged(string oldname, string newname)
		{
			if (NameChanged != null)
			{
				NameChanged(this, oldname, newname);
			}
			OnModified(HaltTypes.Continue);
		}

		private void OnOutputChannelAdded()
		{
			if (OutputChannelAdded != null)
			{
				OutputChannelAdded(this, EventArgs.Empty);
			}
			OnModified(HaltTypes.Ask);
		}

		private void OnOutputChannelRemoved()
		{
			if (OutputChannelRemoved != null)
			{
				OutputChannelRemoved(this, EventArgs.Empty);
			}
			OnModified(HaltTypes.Ask);
		}

		private void OnSizeChanged()
		{
			if (SizeChanged != null)
			{
				SizeChanged(this, EventArgs.Empty);
			}
			OnModified(HaltTypes.Continue);
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