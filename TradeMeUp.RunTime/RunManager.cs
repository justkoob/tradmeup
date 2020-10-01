using Alpaca.Markets;
using Quartz;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeMeUp.Configuration;
using TradeMeUp.Logging;
using TradeMeUp.RunTime.Models;
using TradeMeUp.RunTime.Strategies;
using ICalendar = Alpaca.Markets.ICalendar;

namespace TradeMeUp.RunTime
{
	public static partial class RunManager
	{
		private static readonly AppSettings appSettings;

		internal static readonly ConcurrentDictionary<string, Security> Securities = new ConcurrentDictionary<string, Security>();
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
		internal static IReadOnlyList<ICalendar> TradingCalendars { get; set; }

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
