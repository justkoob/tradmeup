using Alpaca.Markets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TradeMeUp.RunTime.Extensions
{
	public static class Data
	{
		public static DateTime SubtractTradingDays(this IReadOnlyList<ICalendar> calendars, int days, DateTime? start = null)
		{
			if (start == null)
				start = DateTime.UtcNow;

			var dateTime = DateTime.UtcNow;
			var sortedCalendars = calendars.OrderByDescending(c => c.TradingDateUtc).ToList();
			var startingCalendar = sortedCalendars.Where(c => c.TradingDateUtc == start.GetValueOrDefault().Date).FirstOrDefault();
			int indexOfStart = -1;

			if (startingCalendar != null)
			{
				indexOfStart = sortedCalendars.IndexOf(startingCalendar);
			}

			if (indexOfStart > -1 && 
				indexOfStart + days + 1 < sortedCalendars.Count)
			{
				dateTime = sortedCalendars[indexOfStart + days + 1].TradingDateUtc;
			}

			return dateTime;
		}
	}
}
