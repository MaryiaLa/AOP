using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForSeparateDocuments
{
	/// <summary>
	/// Base File Watcher Helper class.
	/// </summary>
	internal abstract class BaseFileWatcherHelper
	{
		/// <summary>
		/// File System Watcher.
		/// </summary>
		protected FileSystemWatcher fileWatcher;

		/// <summary>
		/// File System Watcher property.
		/// </summary>
		internal FileSystemWatcher FileSystemWatcher
		{
			get { return fileWatcher; }
		}

		/// <summary>
		/// Event log.
		/// </summary>
		protected EventLog eventLog;

		/// <summary>
		/// The message queue name.
		/// </summary>
		protected string queueName;

		/// <summary>
		/// The message queue.
		/// </summary>
		protected MessageQueue queue;

		/// <summary>
		/// Message Queue property.
		/// </summary>
		internal MessageQueue Queue
		{
			get { return queue; }
		}

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="pathFrom">Path to copy files from.</param>
		/// <param name="eventLog">Event log.</param>
		/// <param name="queueName">The message queue name.</param>
		public BaseFileWatcherHelper(string pathFrom, EventLog eventLog, string queueName)
		{
			this.eventLog = eventLog;
			this.eventLog.WriteEntry("File watcher is starting...");
			this.fileWatcher = new FileSystemWatcher();
			this.fileWatcher.Path = pathFrom;
			this.fileWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
			this.fileWatcher.IncludeSubdirectories = false;
			this.fileWatcher.EnableRaisingEvents = false;
			this.queueName = queueName;

			if (MessageQueue.Exists(queueName))
			{
				this.queue = new MessageQueue(queueName);
			}
			else
			{
				this.queue = MessageQueue.Create(queueName);
			}
		}
	}
}
