using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceForSeparateDocuments
{
	/// <summary>
	/// System File Watcher helper class.
	/// </summary>
	internal class FileWatcherHelper : BaseFileWatcherHelper
	{
		private const int TrialsCount = 3;
		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="pathFrom">Path to copy files from.</param>
		/// <param name="eventLog">Event log.</param>
		/// <param name="queueName">The message queue name.</param>
		public FileWatcherHelper(string pathFrom, EventLog eventLog, string queueName)
			: base(pathFrom, eventLog, queueName)
		{
			this.queue.MessageReadPropertyFilter.AppSpecific = true;
			this.queue.Formatter = new BinaryMessageFormatter();

			this.fileWatcher.Created += new FileSystemEventHandler(FSWatcher_Created);
			this.fileWatcher.Deleted += new FileSystemEventHandler(FSWatcher_Removed);
			this.fileWatcher.EnableRaisingEvents = true;
			this.eventLog.WriteEntry("File watcher is started.");

			this.MoveFilesOnStart(pathFrom);
		}

		/// <summary>
		/// Event occurs when new files appeared.
		/// </summary>
		private void FSWatcher_Created(object sender, FileSystemEventArgs e)
		{
			this.eventLog.WriteEntry("File: " + e.FullPath);
			this.AddMessageToQueue(e.FullPath);
		}

		/// <summary>
		/// Event occurs when files removed.
		/// </summary>
		private void FSWatcher_Removed(object sender, FileSystemEventArgs e)
		{
			this.eventLog.WriteEntry("File " + e.FullPath + " removed");
		}

		/// <summary>
		/// Searchs files in source and adds them to a nessage queue.
		/// </summary>
		/// <param name="path">Path to copy from.</param>
		private void MoveFilesOnStart(string path)
		{
			Task.Factory.StartNew(() =>
			{
				foreach (string file in Directory.EnumerateFiles(path))
				{
					this.AddMessageToQueue(file);
				}
			});
		}

		/// <summary>
		/// Adds message to a queue message.
		/// </summary>
		/// <param name="file">File path.</param>
		[LoggerPostSharpAspect]
		public void AddMessageToQueue(string file)
		{
			int trial = 1;
			while (trial <= TrialsCount)
			{
				try
				{
					Message message = new Message(File.ReadAllBytes(file), new BinaryMessageFormatter());
					message.Label = Path.GetFileName(file);
					message.AppSpecific = (int)MessageType.MESSAGE_TYPE_MESSAGE_INFO;
					this.queue.Send(message);
					File.Delete(file);
					this.eventLog.WriteEntry("File: " + file + "added");
					return;
				}
				catch (Exception ex)
				{
					this.eventLog.WriteEntry("Send service. Error occurred: " + ex.Message);
					trial++;
					Thread.Sleep(1000);
				}
			}
		}
	}
}
