using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokerHandShowdown.Models.Cards;
using PokerHandShowdown.Models.Players;
using PokerHandShowdown.Models.Showdown;

namespace PokerHandShowdown.Core.Logic.Rules
{
	/// <summary>
	/// Abstract class used to help with similar rules regarding cards with "n" of a kind
	/// </summary>
	/// <typeparam name="T">The parent type</typeparam>
	public abstract class NOfaKindShowdownRule<T> : AbstractShowdownRule
	{
		protected readonly int NthKind;
		protected readonly ILogger<T> _logger;
		public NOfaKindShowdownRule(int nthKind, int priority, ShowdownSettings showdownSettings, ILoggerFactory logger): base(priority, showdownSettings)
		{
			NthKind = nthKind;
			_logger = logger?.CreateLogger<T>();
		}

		public override IEnumerable<PlayerHand> BreakTie(IEnumerable<PlayerHandChallengedCards> remainingPlayerHands)
		{
			var cardsToFilterOut = new HashSet<Card>(remainingPlayerHands.SelectMany(x => x.ChallengedCards));
			var rankedCards = remainingPlayerHands
				.SelectMany(x => x.PlayerHand.Hand)
				.Where(x => !cardsToFilterOut.Contains(x))
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

		public override IEnumerable<PlayerHandChallengedCards> GetChallengers(IEnumerable<PlayerHand> allPlayerHands)
		{
			var challengers = new List<PlayerHandChallengedCards>();
			var communityMax = 0;
			foreach (var playerhand in allPlayerHands)
			{
				var cardsPairedByValue = playerhand.Hand
					.GroupBy(p => p.Value)
					.Where(gVal => gVal.Count() == NthKind)
					.ToList();

				if (cardsPairedByValue.Any())
				{
					var maxFromPlayer = cardsPairedByValue.Max(x => x.Key);
					if (!challengers.Any() || maxFromPlayer > communityMax)
					{
						// reset challengers list.
						challengers.Clear();
						communityMax = maxFromPlayer;
						AddPlayerAsChallenger(playerhand, challengers, cardsPairedByValue);
					}
					else if (maxFromPlayer == communityMax)
					{
						AddPlayerAsChallenger(playerhand, challengers, cardsPairedByValue);
					}
				}
			}
			if (challengers.Any())
			{
				_logger?.LogDebug("Challengers detected. Challengers: {0}", string.Join(",", challengers.Select(x => x.PlayerHand.PlayerInfo.Name)));
			}
			return challengers;
		}

		private void AddPlayerAsChallenger(PlayerHand playerhand, List<PlayerHandChallengedCards> currentChallengerList, List<IGrouping<int, Card>> challengedCardsWithPair)
		{
			var challengedCards = challengedCardsWithPair.SelectMany(g => g.Where(x => g.Key == x.Value)).ToList();
			var playerHandChallenged = new PlayerHandChallengedCards(playerhand)
			{
				ChallengedCards = challengedCards
			};
			currentChallengerList.Add(playerHandChallenged);
		}
	}
}
