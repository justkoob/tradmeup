using System;

namespace TradeMeUp.Logging
{
	public class ConsoleLogger : ILogger
	{
		private static readonly ConsoleLogger logger;

		public static LoggingLevel LoggingLevel { get; set; }

		private readonly object messageLock = new object();
		
		public ConsoleLogger(LoggingLevel loggingLevel = LoggingLevel.Information)
		{
			LoggingLevel = loggingLevel;
		}

		public async void Information(string message)
		{
			lock (messageLock)
			{
				Console.WriteLine($"{DateTime.Now:s} INFORMATION:: {message}");
			}
		}

		public async void Debug(string message)
		{
			lock (messageLock)
			{
				if (LoggingLevel >= LoggingLevel.Debug)
				{
					Console.ForegroundColor = ConsoleColor.Magenta;
					Console.WriteLine($"{DateTime.Now:s} DEBUG:: {message}");
					Console.ResetColor();
				}
			}
		}
		public async void Warning(string message)
		{
			lock (messageLock)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine($"{DateTime.Now:s} WARNING:: {message}");
				Console.ResetColor();
			}
		}

		public async void Error(string message)
		{
			lock (messageLock)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"{DateTime.Now:s} ERROR:: {message}");
				Console.ResetColor();
			}
		}
		
		public async void Verbose(string message)
		{
			lock (messageLock)
			{
				if (LoggingLevel == LoggingLevel.Verbose)
				{
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.WriteLine($"{DateTime.Now:s} VERBOSE:: {message}");
					Console.ResetColor();
				}
			}
		}
	}

	public partial class LogFactory
	{
		public LogFactory AddConsole()
		{
			var logger = new ConsoleLogger(LoggingLevel);

			if (loggers.TryAdd(typeof(ConsoleLogger).Name, logger))
			{
				LogInformation += logger.Information;
				LogDebug += logger.Debug;
				LogWarning += logger.Warning;
				LogError += logger.Error;
				LogVerbose += logger.Verbose;
			}

			return this;
		}
	}
}
