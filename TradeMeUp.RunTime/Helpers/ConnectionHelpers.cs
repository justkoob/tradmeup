using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeMeUp.RunTime
{
	public static partial class RunManager
	{
		internal static async Task Connect()
		{
			if (!AlpacaStreamingClient_IsConnected)
			{
				// AlpacaStreamingClient connect
				await AlpacaStreamingClient.ConnectAsync();
			}

			if (!PolygonStreamingClient_IsConnected)
			{
				// PolygonStreamingClient connect
				await PolygonStreamingClient.ConnectAsync();

				// PolygonStreamingClient minute agg subscribe
				if (PolygonStreamingClient_Subscribed)
				{
					PolygonStreamingClient.UnsubscribeMinuteAgg("TSLA");
				}

				PolygonStreamingClient.SubscribeMinuteAgg("TSLA");
				PolygonStreamingClient_Subscribed = true;
			}
		}

		internal static async Task Disconnect()
		{
			if (AlpacaStreamingClient_IsConnected)
			{
				// AlpacaStreamingClient disconnect
				await AlpacaStreamingClient.DisconnectAsync();
			}

			if (PolygonStreamingClient_IsConnected)
			{
				// PolygonStreamingClient disconnect
				await PolygonStreamingClient.DisconnectAsync();

				// PolygonStreamingClient minute agg unsubscribe
				if (PolygonStreamingClient_Subscribed)
				{
					PolygonStreamingClient.UnsubscribeMinuteAgg("TSLA");
					PolygonStreamingClient_Subscribed = false;

				}
			}
		}
	}
}
