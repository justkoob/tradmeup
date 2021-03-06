﻿using Alpaca.Markets;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeMeUp.RunTime.Jobs
{
	public class ConnectionManagerJob : IJob
	{
		public async Task Execute(IJobExecutionContext context)
		{
			RunManager.Logger.LogDebug("ConnectionManagerJob");

			RunManager.Clock = await RunManager.AlpacaTradingClient.GetClockAsync();

			RunManager.Logger.LogDebug($"Market IsOpen: {RunManager.Clock.IsOpen}");
			
			if (RunManager.Clock.IsOpen)
			{
				// Market should be open.  We need to ensure we are connected and subscribed.
				await RunManager.Connect();
			}
			else
			{
				// Market should be closed.  We need to ensure we are disconnected and unsubscribed.
				await RunManager.Disconnect();
			}
		}
	}
}
