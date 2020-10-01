using Alpaca.Markets;
using System;
using System.Linq;
using System.Threading.Tasks;
using TradeMeUp.RunTime.Enums;
using TradeMeUp.RunTime.Extensions;
using TradeMeUp.RunTime.Models;

namespace TradeMeUp.RunTime.Indicators
{
	public abstract class BaseIndicator
	{
		private readonly FixedSizeQueue<IAgg> queue;
		
		public string Symbol { get; private set; }
		public int Period { get; private set; }
		public AggregationPeriod AggregationPeriod { get; private set; }
		public Field Field { get; private set; }
		public abstract decimal Value { get; }
		public bool IsReady => queue.Size == queue.Count;

		public BaseIndicator(string symbol, int period, AggregationPeriod aggregationPeriod, Field field = Field.Close)
		{
			if (period < 1)
				throw new Exception("Period must be greater than 1.");

			// TODO: Add support for other AggregationPeriodUnit values
			if (aggregationPeriod.Unit != AggregationPeriodUnit.Day)
				throw new NotImplementedException("AggregationPeriodUnit Day is the only supported unit.");

			queue = new FixedSizeQueue<IAgg>(period);
			Symbol = symbol;
			Period = Period;
			AggregationPeriod = aggregationPeriod;
			Field = field;
		}

		public async Task Update()
		{
			int period = IsReady ? 1 : Period;
			var aggregatesRequest = new AggregatesRequest(Symbol, AggregationPeriod);
			var from = RunManager.TradingCalendars.SubtractTradingDays(period);
			aggregatesRequest.SetInclusiveTimeInterval(from, DateTime.UtcNow);
			var history = await RunManager.PolygonDataClient.ListAggregatesAsync(aggregatesRequest);

			foreach (var agg in history.Items)
			{
				queue.Enqueue(agg);
			}
		}
	}
}
