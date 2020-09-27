using System.Threading.Tasks;

namespace TradeMeUp.RunTime
{
	public static partial class RunManager
	{
		internal static async Task Connect()
		{
			if (!AlpacaStreamingClient_IsConnected)
			{
				Logger.LogDebug("AlpacaStreamingClient connect");

				// AlpacaStreamingClient connect
				await AlpacaStreamingClient.ConnectAsync();
			}

			if (!PolygonStreamingClient_IsConnected)
			{
				Logger.LogDebug("PolygonStreamingClient connect");

				// PolygonStreamingClient connect
				await PolygonStreamingClient.ConnectAsync();

				// PolygonStreamingClient minute agg subscribe
				if (PolygonStreamingClient_Subscribed)
				{
					Logger.LogDebug("PolygonStreamingClient minute agg unsubscribe");

					PolygonStreamingClient.UnsubscribeMinuteAgg("TSLA");
				}

				Logger.LogDebug("PolygonStreamingClient minute agg subscribe");

				PolygonStreamingClient.SubscribeMinuteAgg("TSLA");
				PolygonStreamingClient_Subscribed = true;
			}
		}

		internal static async Task Disconnect()
		{
			if (AlpacaStreamingClient_IsConnected)
			{
				Logger.LogDebug("AlpacaStreamingClient disconnect");

				// AlpacaStreamingClient disconnect
				await AlpacaStreamingClient.DisconnectAsync();
			}

			if (PolygonStreamingClient_IsConnected)
			{
				Logger.LogDebug("PolygonStreamingClient disconnect");

				// PolygonStreamingClient disconnect
				await PolygonStreamingClient.DisconnectAsync();

				// PolygonStreamingClient minute agg unsubscribe
				if (PolygonStreamingClient_Subscribed)
				{
					Logger.LogDebug("PolygonStreamingClient minute agg unsubscribe");

					PolygonStreamingClient.UnsubscribeMinuteAgg("TSLA");
					PolygonStreamingClient_Subscribed = false;

				}
			}
		}
	}
}
