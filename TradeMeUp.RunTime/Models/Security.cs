using Alpaca.Markets;

namespace TradeMeUp.RunTime.Models
{
	public delegate void IStreamAggDelegate(IStreamAgg obj);

	public class Security
	{
		public string Symbol { get; private set; }
		public IStreamAggDelegate OnMinuteAggReceived { get; set; }
		
		// public IStreamAggDelegate OnSecondAggReceived { get; set; }
		// LastOrder
		// LastFilledOrder
		// Invested
		// TransactionDate
		// OrderHistroy

		public Security(string symbol)
		{
			Symbol = symbol;
		}
	}
}
