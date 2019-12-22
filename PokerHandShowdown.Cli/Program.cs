using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using PokerHandShowdown.Core.Extensions;
using System;

namespace PokerHandShowdown.Cli
{
	class Program
	{
		static void Main(string[] args)
		{
			//setup our DI
			var serviceProvider = new ServiceCollection()
				.AddLogging(c => {
					c.AddConsole();
					c.SetMinimumLevel(LogLevel.Debug);
				})
				.AddSingleton<IApp, App>()
				.InjectPokerShowdownDependencies()
				.BuildServiceProvider();

			var logger = serviceProvider.GetService<ILoggerFactory>()
				.CreateLogger<Program>();
			logger.LogDebug("Starting application");

			var app = serviceProvider.GetService<IApp>();
			var players = app.AddPlayers();
			app.Run(players);
		}
	}
}
