using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

namespace TradeMeUp.RunTime
{
	public partial class RunManager
	{
		public const string EVERY_MINUTE_FORVER = "0 0/1 * ? * * *";
		
		private static async Task StartScheduler()
		{
			Logger.LogDebug("StartScheduler");

			Scheduler = await new StdSchedulerFactory().GetScheduler();
			await Scheduler.Start();
			await Scheduler.ScheduleJob(GetJob(typeof(ConnectionManagerJob)), GetTrigger(EVERY_MINUTE_FORVER));
		}

		private static IJobDetail GetJob(Type type)
		{
			return JobBuilder.Create(type)
				.WithIdentity(type.Name)
				.Build();
		}

		private static ITrigger GetTrigger(string cronExpression)
		{
			return TriggerBuilder.Create()
				.WithCronSchedule(cronExpression)
				.Build();
		}
	}
}
