using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualProcessors.Design;
using VisualProcessors.Processing;

namespace VisualProcessors.Processors
{
	[ProcessorMeta("Bram Kamies", "A base processor for processors whose functionallity requires interaction with a file", "", "",
		AllowUserSpawn = false)]
	public class FileBasedProcessor : Processor
	{
		#region Properties

		protected List<string> ExtensionRestriction = new List<string>();
		protected FileAccess FileAccess = FileAccess.ReadWrite;
		protected FileStream FileStream;
		private bool m_Loaded = false;
		private bool m_OutputSet = false;

		#endregion Properties

		#region Options

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Output file")]
		[Category("Settings")]
		[Description("The file to write the input data to")]
		[DefaultValue("")]
		[Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string FilePath
		{
			get
			{
				return Options.GetOption("FilePath", "");
			}
			set
			{
				m_OutputSet = false;
				ThrowExceptionOnInvalid(value);
				m_OutputSet = true;
				Options.SetOption("FilePath", value);
				OnModified(HaltTypes.ShouldHalt);
			}
		}

		#endregion Options

		#region Constructor

		public FileBasedProcessor()
			: base()
		{
			m_Loaded = true;
		}

		public FileBasedProcessor(Pipeline p, string name)
			: base(p, name)
		{
		}

		#endregion Constructor

		#region Methods

		public override void Dispose()
		{
			DisposeFileStream();
			base.Dispose();
		}

		public override void Start()
		{
			try
			{
				ThrowExceptionOnInvalid(FilePath);
			}
			catch(Exception e)
			{
				OnError(new ProcessorErrorEventArgs(this, "FilePath", e.Message, false, HaltTypes.ShouldHalt, IsFilePathValid));
			}
			FileStream = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess);
			base.Start();
		}

		public override void Stop()
		{
			base.Stop();
			DisposeFileStream();
		}

		public bool IsFilePathValid()
		{
			try
			{
				ThrowExceptionOnInvalid(FilePath);
			}
			catch(Exception e)
			{
				return false;
			}
			return true;
		}

		public void ThrowExceptionOnInvalid(string path)
		{
			if (path == "")
			{
				throw new Exception("No output file selected");
			}
			FileInfo info = new FileInfo(path);
			if (!info.Exists)
			{
				File.Create(path).Close();
				info = new FileInfo(path);
			}
			if (ExtensionRestriction.Count > 0)
			{
				if (!ExtensionRestriction.Contains(info.Extension))
				{
					throw new Exception("File extension " + info.Extension + " is not allowed");
				}
			}
			if (info.Attributes.HasFlag(FileAttributes.Directory))
			{
				throw new Exception("The selected file is a directory");
			}
			if (info.Attributes.HasFlag(FileAttributes.System))
			{
				throw new Exception("The selected file is a system-only file");
			}
			if (info.Attributes.HasFlag(FileAttributes.Offline))
			{
				throw new Exception("The selected file is offline or unavailable");
			}
			if (info.Attributes.HasFlag(FileAttributes.Temporary))
			{
				OnError(new ProcessorErrorEventArgs(this, "FilePath", "The selected file is a temporary file, it may be removed when the application closes", true, HaltTypes.Continue, IsFilePathValid));
			}
			if (info.Attributes.HasFlag(FileAttributes.Compressed))
			{
				OnError(new ProcessorErrorEventArgs(this, "FilePath", "The selected file is compressed, unknown behaviour on read/write-operations", true, HaltTypes.Continue, IsFilePathValid));
			}
			if (info.Attributes.HasFlag(FileAttributes.ReadOnly))
			{
				OnError(new ProcessorErrorEventArgs(this, "FilePath", "The selected file is a read-only file", true, HaltTypes.Continue, IsFilePathValid));
			}

			//This will try to open the file, and immidiatly close it again.
			//This will throw an exception if the file is in use by another program.
			info.Open(FileMode.Open, FileAccess, FileShare.None).Close();
		}

		protected void DisposeFileStream()
		{
			if (FileStream != null)
			{
				FileStream.Dispose();
				FileStream = null;
			}
		}

		protected override void Prepare()
		{
			if (m_Loaded)
			{
				if (FilePath != "")
				{
					m_OutputSet = true;
				}
				m_Loaded = false;
			}
			if (!m_OutputSet)
			{
				throw new Exception("No file selected");
			}
		}

		protected override void WorkerMethod()
		{
			if (!m_OutputSet)
			{
				return;
			}
			base.WorkerMethod();
		}

		#endregion Methods

		#region IXmlSerializable Members

		public override void WriteXml(System.Xml.XmlWriter writer)
		{
			base.WriteXml(writer);
		}

		#endregion IXmlSerializable Members
	}
}