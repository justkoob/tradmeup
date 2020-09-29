using Alpaca.Markets;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMeUp.Configuration;
using TradeMeUp.Logging;
using TradeMeUp.RunTime.Strategies;

namespace TradeMeUp.RunTime
{
	public static partial class RunManager
	{
		private static AppSettings appSettings;
		private static readonly List<IStrategy> strategies = new List<IStrategy>();

		internal static readonly LogFactory Logger;
		internal static IScheduler Scheduler { get; set; }
		internal static IAlpacaTradingClient AlpacaTradingClient { get; set; }
		internal static IAlpacaStreamingClient AlpacaStreamingClient { get; set; }
		internal static IPolygonDataClient PolygonDataClient { get; set; }
		internal static IPolygonStreamingClient PolygonStreamingClient { get; set; }

		// TODO: Should be potentially thread safe object.  Implement an object lock for these???
		internal static bool AlpacaStreamingClient_IsConnected { get; set; }
		internal static bool PolygonStreamingClient_IsConnected { get; set; }
		internal static bool PolygonStreamingClient_Subscribed { get; set; }
		internal static IClock Clock { get; set; }

		public static bool Live => appSettings.live;
		public static bool Extended => appSettings.extendedTrading;

		static RunManager()
		{
			Logger = new LogFactory()
#if DEBUG
				.MinimumDebug()
#else
				.MinimumInformation()
#endif
				.AddConsole()
				.AddFile();

			appSettings = ConfigManager.Load();

			// Alpaca client initialize
			AlpacaTradingClient = appSettings.Environment.GetAlpacaTradingClient(appSettings.SecretKey);

			AlpacaStreamingClient = appSettings.Environment.GetAlpacaStreamingClient(appSettings.SecretKey);
			AlpacaStreamingClient.SocketOpened += AlpacaStreamingClient_SocketOpened;
			AlpacaStreamingClient.Connected += AlpacaStreamingClient_Connected;
			AlpacaStreamingClient.SocketClosed += AlpacaStreamingClient_SocketClosed;
			AlpacaStreamingClient.OnError += AlpacaStreamingClient_OnError;
			AlpacaStreamingClient.OnAccountUpdate += AlpacaStreamingClient_OnAccountUpdate;
			AlpacaStreamingClient.OnTradeUpdate += AlpacaStreamingClient_OnTradeUpdate;

			// Polygon client initialize
			PolygonDataClient = appSettings.Environment.GetPolygonDataClient(appSettings.Id);
			PolygonStreamingClient = appSettings.Environment.GetPolygonStreamingClient(appSettings.Id);
			PolygonStreamingClient.SocketOpened += PolygonStreamingClient_SocketOpened;
			PolygonStreamingClient.Connected += PolygonStreamingClient_Connected;
			PolygonStreamingClient.SocketClosed += PolygonStreamingClient_SocketClosed;
			PolygonStreamingClient.OnError += PolygonStreamingClient_OnError;
			PolygonStreamingClient.MinuteAggReceived += PolygonStreamingClient_MinuteAggReceived;
		}

		private static void PolygonStreamingClient_MinuteAggReceived(IStreamAgg obj)
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

		public static async void StartAsync()
		{
			Logger.LogDebug("Starting RunManager");

			await Initialize();

			while (Scheduler.IsShutdown)
			{
				await Task.Delay(1000);
			}
		}

		private static async Task Initialize()
		{
			Logger.LogDebug("Initializing");
			
			await LoadStrategies();
			await StartScheduler();

			Logger.LogInformation("Initialize complete.");
		}

		public static async Task StopAsync()
		{
			// TODO: Should we liquidate on stop?
			await Disconnect();
			AlpacaTradingClient?.Dispose();
			AlpacaStreamingClient?.Dispose();
			PolygonDataClient?.Dispose();
			PolygonStreamingClient?.Dispose();
			await Scheduler?.Shutdown();
		}
	}
}
