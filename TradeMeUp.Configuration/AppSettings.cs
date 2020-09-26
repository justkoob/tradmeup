using Alpaca.Markets;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TradeMeUp.Configuration
{
	public class AppSettings
	{
		[Required]
		public bool live { get; set; }

		[Required]
		public string liveId { get; set; }

		[Required]
		public string liveKey { get; set; }

		[Required]
		public string paperId { get; set; }

		[Required]
		public string paperKey { get; set; }

		[Required]
		public List<string> strategies { get; set; }

		[Required]
		public bool extendedTrading { get; set; }

		public string Id => live ? liveId : paperId;
		public string Key => live ? liveKey : paperKey;
		public IEnvironment Environment => live ? Environments.Live : Environments.Paper;
		public SecretKey SecretKey => new SecretKey(Id, Key);
	}
}
