using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;
using TradeMeUp.RunTime.Jobs;

namespace TradeMeUp.RunTime
{
	public partial class RunManager
	{
		public const string WEEKDAYS_AT_EVERY_MINUTE = "0 0/1 * ? * MON,TUE,WED,THU,FRI *";
		public const string WEEKDAYS_AT_2AM = "0 0 2 ? * MON,TUE,WED,THU,FRI *";

		private static async Task StartScheduler()
		{
			Logger.LogDebug("StartScheduler");

			Scheduler = await new StdSchedulerFactory().GetScheduler();
			await Scheduler.Start();
			await Scheduler.ScheduleJob(GetJob(typeof(ConnectionManagerJob)), GetTrigger(WEEKDAYS_AT_EVERY_MINUTE));
			await Scheduler.ScheduleJob(GetJob(typeof(DailyJob)), GetTrigger(WEEKDAYS_AT_2AM));
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
