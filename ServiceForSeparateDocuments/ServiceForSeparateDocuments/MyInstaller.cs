using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace ServiceForSeparateDocuments
{
	/// <summary>
	/// The service installer.
	/// </summary>
	[RunInstaller(true)]
	public partial class MyInstaller : Installer
	{
		public MyInstaller()
		{
			var serviceInstaller = new ServiceInstaller();
			serviceInstaller.StartType = ServiceStartMode.Manual;
			serviceInstaller.ServiceName = Service1.SericeName;
			serviceInstaller.DisplayName = Service1.SericeName;

			Installers.Add(serviceInstaller);

			var processInstaller = new ServiceProcessInstaller();
			processInstaller.Account = ServiceAccount.LocalSystem;

			Installers.Add(processInstaller);
		}
	}
}
