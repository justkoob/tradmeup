using Alpaca.Markets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMeUp.Configuration;

namespace TradeMeUp.RunTime
{
	public static class RunManager
	{
		private static AppSettings appSettings;

		private static IAlpacaTradingClient alpacaTradingClient;
		private static IAlpacaStreamingClient alpacaStreamingClient;
		private static IPolygonDataClient polygonDataClient;
		private static IPolygonStreamingClient polygonStreamingClient;

		public static bool Live => appSettings.live;
		public static bool Extended => appSettings.extendedTrading;

		static RunManager()
		{
			appSettings = ConfigManager.Load();

			// Alpaca client initialize
			alpacaTradingClient = appSettings.Environment.GetAlpacaTradingClient(appSettings.SecretKey);

			alpacaStreamingClient = appSettings.Environment.GetAlpacaStreamingClient(appSettings.SecretKey);
			alpacaStreamingClient.OnAccountUpdate += AlpacaStreamingClient_OnAccountUpdate;
			alpacaStreamingClient.OnTradeUpdate += AlpacaStreamingClient_OnTradeUpdate;
			alpacaStreamingClient.SocketOpened += AlpacaStreamingClient_SocketOpened;
			alpacaStreamingClient.SocketClosed += AlpacaStreamingClient_SocketClosed;
			alpacaStreamingClient.Connected += AlpacaStreamingClient_Connected;

			// Polygon client initialize
			polygonDataClient = appSettings.Environment.GetPolygonDataClient(appSettings.Id);

			polygonStreamingClient = appSettings.Environment.GetPolygonStreamingClient(appSettings.Id);
			polygonStreamingClient.Connected += PolygonStreamingClient_Connected;
			polygonStreamingClient.SocketOpened += PolygonStreamingClient_SocketOpened;
			polygonStreamingClient.SocketClosed += PolygonStreamingClient_SocketClosed;
			polygonStreamingClient.MinuteAggReceived += PolygonStreamingClient_MinuteAggReceived;
		}

		private static void PolygonStreamingClient_MinuteAggReceived(IStreamAgg obj)
		{
			throw new NotImplementedException();
		}

		private static void PolygonStreamingClient_SocketClosed()
		{
			throw new NotImplementedException();
		}

		private static void PolygonStreamingClient_SocketOpened()
		{
			throw new NotImplementedException();
		}

		private static void PolygonStreamingClient_Connected(AuthStatus obj)
		{
			throw new NotImplementedException();
		}

		private static void AlpacaStreamingClient_SocketClosed()
		{
			throw new NotImplementedException();
		}

		private static void AlpacaStreamingClient_SocketOpened()
		{
			throw new NotImplementedException();
		}

		private static void AlpacaStreamingClient_OnTradeUpdate(ITradeUpdate obj)
		{
			throw new NotImplementedException();
		}

		private static void AlpacaStreamingClient_OnAccountUpdate(IAccountUpdate obj)
		{
			throw new NotImplementedException();
		}

		private static void AlpacaStreamingClient_Connected(AuthStatus obj)
		{
			throw new NotImplementedException();
		}

		public static async void StartAsync()
		{

		}
	}
}
