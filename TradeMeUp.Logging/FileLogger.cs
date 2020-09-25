using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeMeUp.Logging
{
	public class FileLogger : ILogger
	{
		private static readonly ILog logger = LogManager.GetLogger(typeof(FileLogger));

		public static LoggingLevel LoggingLevel { get; set; }

		private readonly object messageLock = new object();

		public FileLogger(LoggingLevel loggingLevel = LoggingLevel.Information)
		{
			LoggingLevel = loggingLevel;
			Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

			PatternLayout patternLayout = new PatternLayout();
			patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
			patternLayout.ActivateOptions();

			RollingFileAppender roller = new RollingFileAppender();
			roller.AppendToFile = false;
			roller.File = @"logs\trademeup.log";
			roller.DatePattern = "yyyy-MM-dd";
			roller.Layout = patternLayout;
			roller.PreserveLogFileNameExtension = true;
			roller.MaxSizeRollBackups = 5;
			roller.MaximumFileSize = "1GB";
			roller.RollingStyle = RollingFileAppender.RollingMode.Date;
			roller.StaticLogFileName = false;
			roller.ActivateOptions();

			hierarchy.Root.AddAppender(roller);

			MemoryAppender memoryAppender = new MemoryAppender();
			memoryAppender.ActivateOptions();
			hierarchy.Root.AddAppender(memoryAppender);

			hierarchy.Configured = true;
		}

		public async void Information(string message)
		{
			lock (messageLock)
			{
				logger.Info(message);
			}
		}

		public async void Debug(string message)
		{
			lock (messageLock)
			{
				if (LoggingLevel >= LoggingLevel.Debug)
				{
					logger.Debug(message);
				}
			}
		}
		public async void Warning(string message)
		{
			lock (messageLock)
			{
				logger.Warn(message);
			}
		}

		public async void Error(string message)
		{
			lock (messageLock)
			{
				logger.Error(message);
			}
		}

		public async void Verbose(string message)
		{
			lock (messageLock)
			{
				if (LoggingLevel == LoggingLevel.Verbose)
				{
					logger.Info(message);
				}
			}
		}
	}

	public partial class LogFactory
	{
		public LogFactory AddFile()
		{
			var logger = new FileLogger(LoggingLevel);

			if (loggers.TryAdd(typeof(FileLogger).Name, logger))
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
