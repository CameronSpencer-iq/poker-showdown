using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using PokerHandShowdown.Models.Cards;
using PokerHandShowdown.Models.Players;
using PokerHandShowdown.Models.Showdown;

namespace PokerHandShowdown.Core.Logic.Rules
{
	public class FlushShowdownRule : AbstractShowdownRule
	{
		private readonly ILogger<FlushShowdownRule> _logger;
		public FlushShowdownRule(int priority, ShowdownSettings showdownSettings, ILoggerFactory logger) : base(priority, showdownSettings)
		{
			_logger = logger.CreateLogger<FlushShowdownRule>();
		}

		/// <inheritdoc />
		public override IEnumerable<PlayerHand> BreakTie(IEnumerable<PlayerHandChallengedCards> remainingPlayerHands)
		{
			var rankedCards = remainingPlayerHands
				.SelectMany(x => x.ChallengedCards)
				.GroupBy(x => x.Value)
				.Where(x => x.Count() <= 1)
				.OrderByDescending(x => x.Key);

			if(!rankedCards.Any() && ShowdownSettings.IsFairMode)
			{
				return remainingPlayerHands.Select(x => x.PlayerHand);

			} else if (!rankedCards.Any() && !ShowdownSettings.IsFairMode)
			{
				return new List<PlayerHand>();
			} else
			{
				var winningCard = rankedCards.Select(x => x.Key).First();
				return remainingPlayerHands
					.Where(x => x.ChallengedCards.Any(y => y.Value == winningCard))
					.Select(x => x.PlayerHand);
			}
		}

		/// <inheritdoc />
		public override IEnumerable<PlayerHandChallengedCards> GetChallengers(IEnumerable<PlayerHand> allPlayerHands)
		{
			var challengers = new List<PlayerHandChallengedCards>();
			foreach (var playerhand in allPlayerHands)
			{
				var challengedCards = playerhand.Hand
					.GroupBy(p => p.Suit)
					.Where(gSuits => gSuits.Count() >= ShowdownSettings.FlushHandCount)
					.SelectMany(g => g.AsEnumerable());

				if (challengedCards.Any())
				{
					var playerHandChallenged = new PlayerHandChallengedCards(playerhand)
					{
						ChallengedCards = challengedCards
					};
					challengers.Add(playerHandChallenged);
				}
			}
			if(challengers.Any())
			{
				_logger?.LogDebug("Challengers detected. Challengers: {0}", string.Join(",", challengers.Select(x => x.PlayerHand.PlayerInfo.Name)));
			}
			return challengers;
		}
	}
}
