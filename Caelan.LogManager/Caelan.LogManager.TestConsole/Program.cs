using System;

namespace Caelan.LogManager.TestConsole
{
	public class TestWriter : ILogWriter
	{
		public void Log(LogType logType, string message)
		{
			Console.WriteLine("{0} {1}", logType, message);
		}

		public void LogException(LogType logType, string message, Exception ex)
		{
			Console.WriteLine("{0} {1} {2}", logType, message, ex.Message);
		}

		public void Assign(WriterElement element)
		{
		}

		public string Source { get; set; }
	}

	public static class MainClass
	{
		public static void Main(string[] args)
		{
			Log.AddWriter("Test", typeof(TestWriter));

			var logger = Log.CurrentLogger("MainClass");

			logger.Log(LogType.Debug, "pippo");

			Console.WriteLine("Hello World!");
		}
	}
}
