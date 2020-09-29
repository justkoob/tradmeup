using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace TradeMeUp.RunTime.Strategies
{
	public interface IStrategy
	{
		List<string> Subscriptions { get; }
		Task Initialize();
		void OnData(IStream obj);
	}
}
