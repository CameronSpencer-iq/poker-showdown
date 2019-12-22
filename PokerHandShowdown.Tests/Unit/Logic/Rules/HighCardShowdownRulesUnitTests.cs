using PokerHandShowdown.Core.Logic.Rules;
using PokerHandShowdown.Models.Cards;
using PokerHandShowdown.Models.Players;
using PokerHandShowdown.Models.Showdown;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using System.Linq;
using Microsoft.Extensions.Logging.Abstractions;

namespace PokerHandShowdown.Tests.Unit.Logic.Rules
{
	public class HighCardShowdownRulesUnitTests : IDisposable
	{
		private readonly ShowdownSettings _showDownSettings;
		private readonly IShowdownRule _rule;

		public HighCardShowdownRulesUnitTests()
		{
			_showDownSettings = new ShowdownSettings();
			_rule = new HighCardShowdownRule(priority: 0, _showDownSettings, NullLoggerFactory.Instance);
		}

		[Fact]
		public void HighCardShowdownRule_FairMode_MultipleWinners()
		{
			// Act
			var bobsHand = new PlayerHand(new Player("Bob"), new List<Card>
				{
					new Card(CardSuitEnum.Club, 2),
					new Card(CardSuitEnum.Spade, 3),
					new Card(CardSuitEnum.Diamond, 4),
					new Card(CardSuitEnum.Heart, 5),
					new Card(CardSuitEnum.Club, 6)

				});
			var stacysHand = new PlayerHand(new Player("Stacy"), new List<Card>
				{
					new Card(CardSuitEnum.Spade, 2),
					new Card(CardSuitEnum.Diamond, 3),
					new Card(CardSuitEnum.Heart, 4),
					new Card(CardSuitEnum.Club, 5),
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
			actualWinners.Count().Should().Be(2);
		}

		[Fact]
		public void HighCardShowdownRule_Winners_OneWinner()
		{
			// Act
			var bobsHand = new PlayerHand(new Player("Bob"), new List<Card>
				{
					new Card(CardSuitEnum.Club, 2),
					new Card(CardSuitEnum.Spade, 3),
					new Card(CardSuitEnum.Diamond, 4),
					new Card(CardSuitEnum.Heart, 5),
					new Card(CardSuitEnum.Club, 6)

				});
			var stacysHand = new PlayerHand(new Player("Stacy"), new List<Card>
				{
					new Card(CardSuitEnum.Spade, 2),
					new Card(CardSuitEnum.Diamond, 7),
					new Card(CardSuitEnum.Heart, 4),
					new Card(CardSuitEnum.Club, 5),
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


			var actualResult = _rule.BreakTie(challengers);

			// Assert
			actualResult.Count().Should().Be(1);
			actualResult.First().PlayerInfo.Name.Should().Be("Stacy");
		}


		public void Dispose() { }
	}
}
