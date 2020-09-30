using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeMeUp.RunTime.Models;

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

				Subscribe();
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

				Unsubscribe();
			}
		}

		private static void Subscribe()
		{
			Unsubscribe();

			Logger.LogDebug("PolygonStreamingClient minute agg subscribe");

			PolygonStreamingClient.SubscribeMinuteAgg(Securities.Keys);
			PolygonStreamingClient_Subscribed = true;
		}

		private static void Unsubscribe()
		{
			// PolygonStreamingClient minute agg subscribe
			if (PolygonStreamingClient_Subscribed)
			{
				Logger.LogDebug("PolygonStreamingClient minute agg unsubscribe");

				PolygonStreamingClient.UnsubscribeMinuteAgg(Securities.Keys);
			}
		}
	}
}
