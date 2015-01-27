using System;

namespace Caelan.LogManager.TestConsole
{
	public static class MainClass
	{
		public static void Main(string[] args)
		{
			var logger = Log.Logger("MainClass");

			logger.Log(LogType.Debug, "pippo");

			Console.WriteLine("Hello World!");
		}
	}
}
