using Alpaca.Markets;
using System;

namespace TradeMeUp.RunTime
{
	public static partial class RunManager
	{
		private static void PolygonStreamingClient_MinuteAggReceived(IStreamAgg obj)
		{
			Securities[obj.Symbol].OnMinuteAggReceived?.Invoke(obj);
		}

		private static void AlpacaStreamingClient_OnTradeUpdate(ITradeUpdate obj)
		{
			// TODO: Implement trade updates for Securities objects
			throw new NotImplementedException();
		}

		private static void AlpacaStreamingClient_OnAccountUpdate(IAccountUpdate obj)
		{
			Logger.LogInformation($"AlpacaStreamingClient_OnAccountUpdate: {obj}");
		}
	}
}
