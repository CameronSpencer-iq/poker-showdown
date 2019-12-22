using System.Linq;
using Microsoft.Extensions.Logging;
using PokerHandShowdown.Core.Factory.Showdown;
using PokerHandShowdown.Core.Logic.Rules;
using PokerHandShowdown.Models.Showdown;

namespace PokerHandShowdown.Core.Logic
{
	/// <summary>
	/// Implementation for Executing a Showdown
	/// </summary>
	public class PokerShowdown : IPokerShowdown
	{
		private readonly IOrderedEnumerable<AbstractShowdownRule> _rules;
		private readonly ILogger<PokerShowdown> _logger;


		/// <summary>
		/// Based on the collection of rules, a winner or winners will be determined
		/// </summary>
		/// <param name="showdownRulesFactory"></param>
		public PokerShowdown(IShowdownRulesFactory showdownRulesFactory, ILoggerFactory logger)
		{
			_rules = showdownRulesFactory.BuildRules();
			_logger = logger?.CreateLogger<PokerShowdown>();
		}

		/// <inheritdoc />
		public ShowdownResponse RevealWinner(ShowdownRequest request)
		{
			foreach (var rule in _rules)
			{
				var winners = rule.GetWinners(request.PlayerHands);
				if (winners.Any())
				{
					return new ShowdownResponse
					{
						Winners = winners
					};
				}
			}
			return new ShowdownResponse();
		}
	}
}
