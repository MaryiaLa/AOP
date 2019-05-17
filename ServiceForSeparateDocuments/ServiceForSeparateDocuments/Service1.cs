using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForSeparateDocuments
{
	/// <summary>
	/// The Service class, which is responsible for collecting files and sending them to a queue. 
	/// </summary>
	public partial class Service1 : ServiceBase
	{
		/// <summary>
		/// The service name.
		/// </summary>
		internal const string SericeName = "SendService2";

		/// <summary>
		/// Path to get files from.
		/// </summary>
		private string sourcePath;

		/// <summary>
		/// The message queue name.
		/// </summary>
		private string queueName;

		/// <summary>
		/// File watcher helper class.
		/// </summary>
		private BaseFileWatcherHelper fileWatcherHelper;

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="pathMoveTo">Path to get files from.</param>
		/// <param name="queueName">The message queue name.</param>
		public Service1(string pathMoveFrom, string queueName)
		{
			this.CanStop = true;
			this.ServiceName = SericeName;
			this.AutoLog = false;
			this.CanPauseAndContinue = true;

			this.sourcePath = pathMoveFrom;
			this.queueName = queueName;
		}

		/// <summary>
		/// Action on service start.
		/// </summary>
		/// <param name="args">Arguments.</param>
		protected override void OnStart(string[] args)
		{
			EventLog.WriteEntry("SendService2 started");
			this.fileWatcherHelper = new FileWatcherHelper(this.sourcePath, EventLog, this.queueName);
		}

		/// <summary>
		/// Action on service stop.
		/// </summary>
		protected override void OnStop()
		{
			this.fileWatcherHelper.FileSystemWatcher.EnableRaisingEvents = false;
			this.fileWatcherHelper.FileSystemWatcher.Dispose();
			this.fileWatcherHelper.Queue.Close();
			EventLog.WriteEntry("SendService2 stopped");
		}

		/// <summary>
		/// Action on service pause.
		/// </summary>
		protected override void OnPause()
		{
			this.fileWatcherHelper.FileSystemWatcher.EnableRaisingEvents = false;
			EventLog.WriteEntry("SendService2 paused");
		}

		/// <summary>
		/// Action on service continue.
		/// </summary>
		protected override void OnContinue()
		{
			this.fileWatcherHelper.FileSystemWatcher.EnableRaisingEvents = true;
			EventLog.WriteEntry("SendService2 continued");
		}
	}
}
