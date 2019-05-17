using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForSeparateDocuments
{
	static class Program
	{
		/// <summary>
		/// The main function.
		/// </summary>
		static void Main()
		{
			var appSettings = ConfigurationManager.AppSettings;
			string sourcePath = Path.GetFullPath(appSettings["PathMoveFrom"]);
			string queueName = appSettings["QueueName"];

			if (Directory.Exists(sourcePath))
			{
				ServiceBase[] ServicesToRun;
				ServicesToRun = new ServiceBase[] 
				{ 
					new Service1(sourcePath, queueName) 
				};
				ServiceBase.Run(ServicesToRun);
			}
			else
			{
				throw new DirectoryNotFoundException("Directories doesn't exist or some of them are equal");
			}
		}
	}
}
