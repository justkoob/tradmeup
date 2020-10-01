using Alpaca.Markets;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeMeUp.RunTime.Interfaces;

namespace TradeMeUp.RunTime.Strategies
{
	public abstract class BaseStrategy : IStrategy
	{
		public List<string> Subscriptions { get; protected set; }

		public abstract Task Initialize();

		public abstract void OnDataReceived(IStreamAgg obj);

		public BaseStrategy()
		{
			Subscriptions = new List<string>();
		}
	}
}
