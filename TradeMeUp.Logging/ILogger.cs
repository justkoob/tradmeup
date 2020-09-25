using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeMeUp.Logging
{
	interface ILogger
	{
		void Information(string message);
		void Debug(string message);
		void Warning(string message);
		void Error(string message);
		void Verbose(string message);
	}
}
