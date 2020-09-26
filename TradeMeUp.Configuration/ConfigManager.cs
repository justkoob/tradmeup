using Microsoft.Extensions.Configuration;
using System;

namespace TradeMeUp.Configuration
{
	public static class ConfigManager
    {
        private static readonly string basePath = Environment.CurrentDirectory;
		public static AppSettings AppSettings { get; private set; }

        public static AppSettings Load()
		{
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)

#if DEBUG
                .AddJsonFile("dev.appsettings.json", false, false);
#else
                .AddJsonFile("prod.appsettings.json", false, false);
#endif

            var configuration = builder.Build();
            configuration.Bind("appSettings", AppSettings = new AppSettings());

            return AppSettings;
		}
    }
}
