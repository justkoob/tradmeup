using Alpaca.Markets;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace TradeMeUp.RunTime.Interfaces
{
	public interface IStrategy
	{
		List<string> Subscriptions { get; }
		Task Initialize();
		void OnDataReceived(IStreamAgg obj);
	}
}
