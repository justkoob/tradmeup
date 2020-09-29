using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace TradeMeUp.RunTime.Strategies
{
	public abstract class BaseStrategy : IStrategy
	{
		public List<string> Subscriptions { get; protected set; }

		public abstract Task Initialize();

		public abstract void OnData(IStream obj);

		public BaseStrategy()
		{
			Subscriptions = new List<string>();
		}
	}
}
