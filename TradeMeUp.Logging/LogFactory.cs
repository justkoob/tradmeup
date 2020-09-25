using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeMeUp.Logging
{
	public partial class LogFactory
	{
		private readonly ConcurrentDictionary<string, ILogger> loggers = new ConcurrentDictionary<string, ILogger>();
		public LoggingLevel LoggingLevel { get; set; }

		public delegate void InformationDelegate(string message);
		public delegate void DebugDelegate(string message);
		public delegate void WarningDelegate(string message);
		public delegate void ErrorDelegate(string message);
		public delegate void VerboseDelegate(string message);

		public InformationDelegate LogInformation { get; private set; }
		public DebugDelegate LogDebug { get; private set; }
		public WarningDelegate LogWarning { get; private set; }
		public ErrorDelegate LogError { get; private set; }
		public VerboseDelegate LogVerbose { get; private set; }

		public LogFactory MinimumInformation()
		{
			LoggingLevel = LoggingLevel.Information;
			return this;
		}

		public LogFactory MinimumDebug()
		{
			LoggingLevel = LoggingLevel.Debug;
			return this;
		}

		public LogFactory MinimumVerbose()
		{
			LoggingLevel = LoggingLevel.Verbose;
			return this;
		}
	}
}
