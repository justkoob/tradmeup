using Alpaca.Markets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeMeUp.RunTime
{
	public static partial class RunManager
	{
		internal static async Task<IReadOnlyList<ICalendar>> GetTradingCalendarsAsync()
		{
			return await AlpacaTradingClient.ListCalendarAsync(new CalendarRequest());
		}
	}
}
