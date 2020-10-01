using Quartz;
using System.Threading.Tasks;

namespace TradeMeUp.RunTime.Jobs
{
	public class DailyJob : IJob
	{
		public async Task Execute(IJobExecutionContext context)
		{
			RunManager.TradingCalendars = await RunManager.GetTradingCalendarsAsync();
		}
	}
}
