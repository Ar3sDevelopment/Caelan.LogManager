using System;
using Caelan.LogManager;

namespace Caelan.LogManager.TestConsole
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var logger = Log.CurrentLogger<MainClass>();

			logger.Log (LogType.Debug, "pippo");

			Console.WriteLine ("Hello World!");
		}
	}
}
