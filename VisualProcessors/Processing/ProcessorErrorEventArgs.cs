using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualProcessors.Processing
{
	public class ProcessorErrorEventArgs : EventArgs
	{
		#region Properties

		private bool m_IsWarning = false;
		private HaltTypes m_HaltType = HaltTypes.Ask;
		private string m_Title;
		private string m_Message;
		private Processor m_Source;
		private Func<bool> m_Callback;
		/// <summary>
		/// Gets or sets whether this should be treated as a warning rather than a error
		/// </summary>
		public bool IsWarning
		{
			get
			{
				return m_IsWarning;
			}
			set
			{
				m_IsWarning = value;
			}
		}
		/// <summary>
		/// Gets or sets the halt effect the error has on a simulation, if it's running
		/// </summary>
		public HaltTypes HaltType
		{
			get
			{
				return m_HaltType;
			}
			set
			{
				m_HaltType = value;
			}
		}
		/// <summary>
		/// Gets the title/category of the error
		/// </summary>
		public string Title
		{
			get
			{
				return m_Title;
			}
			set
			{
				m_Title = value;
			}
		}
		/// <summary>
		/// Gets the message describing the error
		/// </summary>
		public string Message
		{
			get
			{
				return m_Message;
			}
			set
			{
				m_Message = value;
			}
		}
		/// <summary>
		/// Gets the processor that emitted the error
		/// </summary>
		public Processor Source
		{
			get
			{
				return m_Source;
			}
			private set
			{
				m_Source = value;
			}
		}
		/// <summary>
		/// Gets the callback that can be used to check if the error is still active, the function should return true if the error has been removed
		/// </summary>
		public Func<bool> Callback
		{
			get
			{
				return m_Callback;
			}
			private set
			{
				m_Callback = value;
			}
		}

		#endregion Properties

		#region Constructor

		public ProcessorErrorEventArgs(Processor source)
		{
			Source = source;
		}

		public ProcessorErrorEventArgs(Processor source, Func<bool> callback) : this(source)
		{
			Callback = callback;
		}

		public ProcessorErrorEventArgs(Processor source, string title, string message, bool isWarning, HaltTypes haltType)
			: this(source)
		{
			Title = title;
			Message = message;
			IsWarning = isWarning;
			HaltType = haltType;
		}

		public ProcessorErrorEventArgs(Processor source, string title, string message, bool isWarning, HaltTypes haltType, Func<bool> callback)
			: this(source,title,message,isWarning,haltType)
		{
			Callback = callback;
		}

		#endregion Constructor

		#region Methods

		#endregion Methods
	}
}