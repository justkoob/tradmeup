using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TradeMeUp.RunTime.Models;
using TradeMeUp.RunTime.Strategies;

namespace TradeMeUp.RunTime
{
	public static partial class RunManager
	{
		private static async Task LoadStrategies()
		{
			var iStrategyTypes = GetIStrategyTypes();
			var iStrategyNames = GetIStrategyNames();
			var initializeTasks = new List<Task>();

			Logger.LogInformation($"Initializing strategies.");

			foreach (var strategyName in appSettings.strategies)
			{
				if (iStrategyNames.Contains(strategyName))
				{
					var strategyType = iStrategyTypes.Where(t => t.Name == strategyName).First();
					var strategy = Activator.CreateInstance(strategyType) as IStrategy;

					var task = Task.Run(async () =>
					{
						Logger.LogDebug($"Initializing: {strategyName}");
						await strategy.Initialize();
					});

					initializeTasks.Add(task);
					
					foreach (var symbol in strategy.Subscriptions)
					{
						var security = Securities.GetOrAdd(symbol, new Security(symbol));
						security.OnMinuteAggReceived += strategy.OnDataReceived;
					}
				}
			}

			if (Securities.Count > 0)
			{
				Task.WaitAll(initializeTasks.ToArray());
				// TODO: No longer outputing the correct information, fix
				var jsonNames = JsonConvert.SerializeObject(Securities.Select(s => s.Value.GetType().Name));
				Logger.LogInformation($"Initialized strategies: {jsonNames}");
			}
			else
			{
				Logger.LogError("No strategies found.");
				await StopAsync();
			}
		}

		private static IEnumerable<Type> GetIStrategyTypes()
		{
			Type parentType = typeof(IStrategy);
			Assembly assembly = Assembly.GetExecutingAssembly();
			Type[] types = assembly.GetTypes();

			return types.Where(t => t.GetInterfaces().Contains(parentType));
		}

		private static IEnumerable<string> GetIStrategyNames()
		{
			return GetIStrategyTypes().Select(t => t.Name);
		}
	}
}
