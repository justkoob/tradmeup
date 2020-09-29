using System.Runtime.InteropServices.ComTypes;
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

		public override void OnData(IStream obj)
		{
			// Implement the strategy
		}
	}
}
