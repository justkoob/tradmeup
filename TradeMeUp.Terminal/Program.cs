using System;
using TradeMeUp.Logging;

namespace TradeMeUp.Terminal
{
	class Program
	{
		static void Main(string[] args)
		{
			var log = new LogFactory()
				.MinimumVerbose()
				.AddConsole()
				.AddFile();

			log.LogInformation("Info Message");
			log.LogDebug("Debug Message");
			log.LogWarning("Warning Message");
			log.LogError("Error Message");
			log.LogVerbose("Verbose Message");

			Console.WriteLine("Press ENTER to continue . . .");
			Console.ReadLine();
		}
	}
}
