using Alpaca.Markets;
using System.Threading.Tasks;

namespace TradeMeUp.RunTime.Strategies
{
	public class TSLASwingTrade : BaseStrategy
	{

		public override Task Initialize()
		{
			// Complete any initialization operations here
			Subscriptions.Add("TSLA");

			return Task.CompletedTask;
		}

		public override void OnDataReceived(IStreamAgg obj)
		{
			// Implement the strategy
		}
	}
}
