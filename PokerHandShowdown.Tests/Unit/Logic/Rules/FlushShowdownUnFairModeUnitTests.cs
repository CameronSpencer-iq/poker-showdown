using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using PokerHandShowdown.Core.Logic.Rules;
using PokerHandShowdown.Models.Cards;
using PokerHandShowdown.Models.Players;
using PokerHandShowdown.Models.Showdown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PokerHandShowdown.Tests.Unit.Logic.Rules
{
	public class FlushShowdownUnFairModeUnitTests: IDisposable
	{
		private readonly ShowdownSettings _showDownSettings;
		private readonly IShowdownRule _rule;

		public FlushShowdownUnFairModeUnitTests()
		{
			_showDownSettings = new ShowdownSettings() { IsFairMode = false };
			_rule = new FlushShowdownRule(priority: 0, _showDownSettings, NullLoggerFactory.Instance);
		}


		[Fact]
		public void FlushShowdownRule_Default_NoWinner()
		{
			// Act
			var bobsHand = new PlayerHand(new Player("Bob"), new List<Card>
				{
					new Card(CardSuitEnum.Club, 2),
					new Card(CardSuitEnum.Club, 3),
					new Card(CardSuitEnum.Club, 4),
					new Card(CardSuitEnum.Club, 5),
					new Card(CardSuitEnum.Club, 6)

				});
			var stacysHand = new PlayerHand(new Player("Stacy"), new List<Card>
				{
					new Card(CardSuitEnum.Diamond, 2),
					new Card(CardSuitEnum.Diamond, 3),
					new Card(CardSuitEnum.Diamond, 4),
					new Card(CardSuitEnum.Diamond, 5),
					new Card(CardSuitEnum.Diamond, 6)

				});

			var challengers = new List<PlayerHandChallengedCards>
			{
				new PlayerHandChallengedCards(bobsHand)
				{
					ChallengedCards = bobsHand.Hand
				},
				new PlayerHandChallengedCards(stacysHand)
				{
					ChallengedCards = stacysHand.Hand
				}
			};

			var actualWinners = _rule.BreakTie(challengers);

			// Assert
			actualWinners.Any().Should().Be(false);
		}

		public void Dispose() {}
	}
}
