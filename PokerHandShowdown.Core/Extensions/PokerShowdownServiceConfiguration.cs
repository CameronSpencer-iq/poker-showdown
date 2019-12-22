using Microsoft.Extensions.DependencyInjection;
using PokerHandShowdown.Core.Factory.Cards;
using PokerHandShowdown.Core.Factory.Players;
using PokerHandShowdown.Core.Factory.Showdown;
using PokerHandShowdown.Core.Logic;
using PokerHandShowdown.Core.Validation;
using PokerHandShowdown.Core.Validation.Players;
using PokerHandShowdown.Models.Cards;
using PokerHandShowdown.Models.Players;
using PokerHandShowdown.Models.Showdown;

namespace PokerHandShowdown.Core.Extensions
{
	public static class PokerShowdownServiceConfiguration
	{
		public static IServiceCollection InjectPokerShowdownDependencies(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<ICardFactory, CardFactory>();
			serviceCollection.AddSingleton<IPlayerHandFactory, PlayerHandFactory>();
			serviceCollection.AddSingleton<IShowdownRulesFactory, ShowdownRulesFactory>();
			serviceCollection.AddSingleton<IPokerShowdown, PokerShowdown>();
			serviceCollection.AddSingleton<ISimpleValidation<PlayerHandFactoryModel>, PlayerHandFactoryModelValidation>();
			serviceCollection.AddSingleton(new ShowdownSettings());
			serviceCollection.AddSingleton(new CardFactorySettings());
			return serviceCollection;

		}
	}
}
