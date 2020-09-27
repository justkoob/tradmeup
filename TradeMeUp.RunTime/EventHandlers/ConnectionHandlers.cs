using Alpaca.Markets;
using System;

namespace TradeMeUp.RunTime
{
	public static partial class RunManager
	{
		private static void AlpacaStreamingClient_SocketOpened()
		{
			Logger.LogInformation("AlpacaStreamingClient_SocketOpened");
		}

		private static void AlpacaStreamingClient_Connected(AuthStatus obj)
		{
			AlpacaStreamingClient_IsConnected = true;
			Logger.LogInformation($"AlpacaStreamingClient_Connected: {obj}");
		}

		private static void AlpacaStreamingClient_SocketClosed()
		{
			AlpacaStreamingClient_IsConnected = false;
			Logger.LogWarning("AlpacaStreamingClient_SocketClosed");
		}

		private static void AlpacaStreamingClient_OnError(Exception obj)
		{
			Logger.LogError($"AlpacaStreamingClient_OnError: {obj}");
		}

		private static void PolygonStreamingClient_SocketOpened()
		{
			Logger.LogInformation("PolygonStreamingClient_SocketOpened");
		}

		private static void PolygonStreamingClient_Connected(AuthStatus obj)
		{
			PolygonStreamingClient_IsConnected = true;
			Logger.LogInformation($"PolygonStreamingClient_Connected: {obj}");
		}

		private static void PolygonStreamingClient_SocketClosed()
		{
			PolygonStreamingClient_IsConnected = false;
			Logger.LogWarning("PolygonStreamingClient_SocketClosed");
		}

		private static void PolygonStreamingClient_OnError(Exception obj)
		{
			Logger.LogError($"PolygonStreamingClient_OnError: {obj}");
		}
	}
}
