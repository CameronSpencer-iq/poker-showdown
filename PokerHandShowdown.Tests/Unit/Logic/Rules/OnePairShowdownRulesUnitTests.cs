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
	public class OnePairShowdownRulesUnitTests : IDisposable
	{
		private readonly ShowdownSettings _showDownSettings;
		private readonly IShowdownRule _rule;

		public OnePairShowdownRulesUnitTests()
		{
			_showDownSettings = new ShowdownSettings();
			_rule = new OnePairShowdownRule(priority: 0, _showDownSettings, NullLoggerFactory.Instance);
		}

		[Fact]
		public void OnePairShowdownRule_Default_NoChallengers()
		{
			// Act
			var players = new List<PlayerHand>
			{
				new PlayerHand(new Player("Bob"), new List<Card>
				{
					new Card(CardSuitEnum.Club, 2),
					new Card(CardSuitEnum.Club, 3),
					new Card(CardSuitEnum.Club, 4),
					new Card(CardSuitEnum.Club, 5),
					new Card(CardSuitEnum.Spade, 6)

				}),
				new PlayerHand(new Player("Stacy"), new List<Card>
				{
					new Card(CardSuitEnum.Diamond, 2),
					new Card(CardSuitEnum.Diamond, 3),
					new Card(CardSuitEnum.Diamond, 4),
					new Card(CardSuitEnum.Diamond, 5),
					new Card(CardSuitEnum.Spade, 6)

				})
			};

			var actualResult = _rule.GetChallengers(players);

			// Assert
			actualResult.Any().Should().Be(false);
		}

		[Fact]
		public void OnePairShowdownRule_Default_OneChallenger()
		{
			// Act
			var players = new List<PlayerHand>
			{
				new PlayerHand(new Player("Bob"), new List<Card>
				{
					new Card(CardSuitEnum.Spade, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Heart, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Diamond, 4),
					new Card(CardSuitEnum.Club, 6),
					new Card(CardSuitEnum.Spade, 5)

				}),
				new PlayerHand(new Player("Stacy"), new List<Card>
				{
					new Card(CardSuitEnum.Club, 9),
					new Card(CardSuitEnum.Diamond, 8),
					new Card(CardSuitEnum.Diamond, 3),
					new Card(CardSuitEnum.Heart, 6),
					new Card(CardSuitEnum.Diamond, 4)
				})
			};

			var actualResult = _rule.GetChallengers(players);

			// Assert
			actualResult.Count().Should().Be(1);
			actualResult.First().PlayerHand.PlayerInfo.Name.Should().Be("Bob");
		}


		[Fact]
		public void OnePairShowdownRule_Default_OneChallenger_2()
		{
			// Act
			var players = new List<PlayerHand>
			{
				new PlayerHand(new Player("Bob"), new List<Card>
				{
					new Card(CardSuitEnum.Spade, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Heart, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Diamond, 4),
					new Card(CardSuitEnum.Club, 6),
					new Card(CardSuitEnum.Spade, 5)

				}),
				new PlayerHand(new Player("Stacy"), new List<Card>
				{
					new Card(CardSuitEnum.Club, (int)CardValueEnum.Queen),
					new Card(CardSuitEnum.Diamond, (int)CardValueEnum.Queen),
					new Card(CardSuitEnum.Diamond, 3),
					new Card(CardSuitEnum.Heart, 6),
					new Card(CardSuitEnum.Diamond, 4)
				}),
				new PlayerHand(new Player("Fred"), new List<Card>
				{
					new Card(CardSuitEnum.Club, 2),
					new Card(CardSuitEnum.Diamond, 2),
					new Card(CardSuitEnum.Diamond, 3),
					new Card(CardSuitEnum.Heart, 3),
					new Card(CardSuitEnum.Diamond, 4)
				})
			};

			var actualResult = _rule.GetChallengers(players);

			// Assert
			actualResult.Count().Should().Be(1);
			actualResult.First().PlayerHand.PlayerInfo.Name.Should().Be("Stacy");
		}

		[Fact]
		public void OnePairShowdownRule_Default_MultipleChallengers()
		{
			// Act
			var players = new List<PlayerHand>
			{
				new PlayerHand(new Player("Bob"), new List<Card>
				{
					new Card(CardSuitEnum.Spade, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Heart, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Diamond, 4),
					new Card(CardSuitEnum.Club, 6),
					new Card(CardSuitEnum.Spade, 5)

				}),
				new PlayerHand(new Player("Stacy"), new List<Card>
				{
					new Card(CardSuitEnum.Club, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Diamond, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Diamond, 3),
					new Card(CardSuitEnum.Heart, 6),
					new Card(CardSuitEnum.Diamond, 4)
				}),
				new PlayerHand(new Player("Fred"), new List<Card>
				{
					new Card(CardSuitEnum.Club, (int)CardValueEnum.Two),
					new Card(CardSuitEnum.Diamond, (int)CardValueEnum.Two),
					new Card(CardSuitEnum.Diamond, 3),
					new Card(CardSuitEnum.Heart, 6),
					new Card(CardSuitEnum.Diamond, 4)
				})
			};

			var actualResult = _rule.GetChallengers(players);

			// Assert
			actualResult.Count().Should().Be(2);
		}

		[Fact]
		public void OnePairShowdownRule_FairMode_MultipleWinners()
		{
			// Act
			var bobsHand = new PlayerHand(new Player("Bob"), new List<Card>
				{
					new Card(CardSuitEnum.Spade, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Heart, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Diamond, 4),
					new Card(CardSuitEnum.Club, 6),
					new Card(CardSuitEnum.Spade, 5)

				});
			var stacysHand = new PlayerHand(new Player("Stacy"), new List<Card>
				{
					new Card(CardSuitEnum.Club, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Diamond, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Heart, 5),
					new Card(CardSuitEnum.Heart, 6),
					new Card(CardSuitEnum.Club, 4)
				});

			var challengers = new List<PlayerHandChallengedCards>
			{
				new PlayerHandChallengedCards(bobsHand)
				{
					ChallengedCards = new List<Card> {
						new Card(CardSuitEnum.Spade, (int)CardValueEnum.Jack),
					    new Card(CardSuitEnum.Heart, (int)CardValueEnum.Jack)
					}
				},
				new PlayerHandChallengedCards(stacysHand)
				{
					ChallengedCards = new List<Card> {
						new Card(CardSuitEnum.Club, (int)CardValueEnum.Jack),
					    new Card(CardSuitEnum.Diamond, (int)CardValueEnum.Jack)
					}
				}
			};

			var actualWinners = _rule.BreakTie(challengers);

			// Assert
			actualWinners.Count().Should().Be(2);
		}

		[Fact]
		public void OnePairShowdownRule_Winners_OneWinner()
		{
			// Act
			var bobsHand = new PlayerHand(new Player("Bob"), new List<Card>
				{
					new Card(CardSuitEnum.Spade, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Heart, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Diamond, 4),
					new Card(CardSuitEnum.Club, 6),
					new Card(CardSuitEnum.Spade, 5)

				});
			var stacysHand = new PlayerHand(new Player("Stacy"), new List<Card>
				{
					new Card(CardSuitEnum.Club, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Diamond, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Heart, 5),
					new Card(CardSuitEnum.Heart, 6),
					new Card(CardSuitEnum.Club, 2)
				});

			var challengers = new List<PlayerHandChallengedCards>
			{
				new PlayerHandChallengedCards(bobsHand)
				{
					ChallengedCards = new List<Card> {
						new Card(CardSuitEnum.Spade, (int)CardValueEnum.Jack),
					    new Card(CardSuitEnum.Heart, (int)CardValueEnum.Jack)
					}
				},
				new PlayerHandChallengedCards(stacysHand)
				{
					ChallengedCards = new List<Card> {
						new Card(CardSuitEnum.Club, (int)CardValueEnum.Jack),
					    new Card(CardSuitEnum.Diamond, (int)CardValueEnum.Jack)
					}
				}
			};

			var actualResult = _rule.BreakTie(challengers);

			// Assert
			actualResult.Count().Should().Be(1);
			actualResult.First().PlayerInfo.Name.Should().Be("Bob");
		}

		public void Dispose() { }
	}
}
