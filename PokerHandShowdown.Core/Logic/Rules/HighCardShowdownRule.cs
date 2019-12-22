using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using PokerHandShowdown.Models.Players;
using PokerHandShowdown.Models.Showdown;

namespace PokerHandShowdown.Core.Logic.Rules
{
	public class HighCardShowdownRule : AbstractShowdownRule
	{
		private readonly ILogger<FlushShowdownRule> _logger;

		public HighCardShowdownRule(int priority, ShowdownSettings showdownSettings, ILoggerFactory logger) : base(priority, showdownSettings)
		{
			_logger = logger?.CreateLogger<FlushShowdownRule>();
		}

		/// <inheritdoc />
		public override IEnumerable<PlayerHand> BreakTie(IEnumerable<PlayerHandChallengedCards> remainingPlayerHands)
		{
			var rankedCards = remainingPlayerHands
				.SelectMany(x => x.PlayerHand.Hand)
				.GroupBy(x => x.Value)
				.Where(x => x.Count() <= 1)
				.OrderByDescending(x => x.Key);

			if (!rankedCards.Any() && ShowdownSettings.IsFairMode)
			{
				return remainingPlayerHands.Select(x => x.PlayerHand);

			}
			else if (!rankedCards.Any() && !ShowdownSettings.IsFairMode)
			{
				return new List<PlayerHand>();
			}
			else
			{
				var winningCard = rankedCards.Select(x => x.Key).First();
				return remainingPlayerHands
					.Where(x => x.PlayerHand.Hand.Any(y => y.Value == winningCard))
					.Select(x => x.PlayerHand);
			}
		}

		/// <inheritdoc />
		public override IEnumerable<PlayerHandChallengedCards> GetChallengers(IEnumerable<PlayerHand> allPlayerHands)
		{
			_logger?.LogDebug("Challengers detected. Any players could potentially win");

			return allPlayerHands.Select(p => new PlayerHandChallengedCards { PlayerHand = p });
		}
	}
}
