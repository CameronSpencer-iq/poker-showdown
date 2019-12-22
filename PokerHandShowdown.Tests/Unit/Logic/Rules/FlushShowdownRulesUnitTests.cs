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
	public class FlushShowdownRulesUnitTests : IDisposable
	{
		private readonly ShowdownSettings _showDownSettings;
		private readonly IShowdownRule _rule;

		public FlushShowdownRulesUnitTests()
		{
			_showDownSettings = new ShowdownSettings();
			_rule = new FlushShowdownRule(priority: 0, _showDownSettings, NullLoggerFactory.Instance);
		}

		[Fact]
		public void FlushShowdownRule_Default_NoChallengers()
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
		public void FlushShowdownRule_Default_OneChallenger()
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
					new Card(CardSuitEnum.Diamond, 6)

				})
			};

			var actualResult = _rule.GetChallengers(players);

			// Assert
			actualResult.Count().Should().Be(1);
		}

		[Fact]
		public void FlushShowdownRule_Default_MultipleChallengers()
		{
			// Act
			var players = new List<PlayerHand>
			{
				new PlayerHand(new Player("Bob"), new List<Card>
				{
					new Card(CardSuitEnum.Spade, (int)CardValueEnum.Ace),
					new Card(CardSuitEnum.Spade, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Spade, (int)CardValueEnum.Ten),
					new Card(CardSuitEnum.Spade, (int)CardValueEnum.Three),
					new Card(CardSuitEnum.Spade, (int)CardValueEnum.Two)

				}),
				new PlayerHand(new Player("Stacy"), new List<Card>
				{
					new Card(CardSuitEnum.Heart, (int)CardValueEnum.King),
					new Card(CardSuitEnum.Heart, (int)CardValueEnum.Ten),
					new Card(CardSuitEnum.Heart, (int)CardValueEnum.Seven),
					new Card(CardSuitEnum.Heart, (int)CardValueEnum.Five),
					new Card(CardSuitEnum.Heart, (int)CardValueEnum.Two)

				}),
				new PlayerHand(new Player("Fred"), new List<Card>
				{
					new Card(CardSuitEnum.Diamond, (int)CardValueEnum.Ace),
					new Card(CardSuitEnum.Diamond, (int)CardValueEnum.King),
					new Card(CardSuitEnum.Diamond, (int)CardValueEnum.Queen),
					new Card(CardSuitEnum.Diamond, (int)CardValueEnum.Eight),
					new Card(CardSuitEnum.Diamond, (int)CardValueEnum.Two)

				}),
				new PlayerHand(new Player("Jen"), new List<Card>
				{
					new Card(CardSuitEnum.Club, (int)CardValueEnum.Queen),
					new Card(CardSuitEnum.Club, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Club, (int)CardValueEnum.Ten),
					new Card(CardSuitEnum.Club, (int)CardValueEnum.Nine),
					new Card(CardSuitEnum.Club, (int)CardValueEnum.Three)

				})
			};

			var actualResult = _rule.GetChallengers(players);

			// Assert
			actualResult.Count().Should().Be(4);
		}

		[Fact]
		public void FlushShowdownRule_Default_MultipleChallengers_2()
		{
			// Act
			var players = new List<PlayerHand>
			{
				new PlayerHand(new Player("Bob"), new List<Card>
				{
					new Card(CardSuitEnum.Spade, (int)CardValueEnum.Ace),
					new Card(CardSuitEnum.Spade, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Spade, (int)CardValueEnum.Ten),
					new Card(CardSuitEnum.Spade, (int)CardValueEnum.Three),
					new Card(CardSuitEnum.Spade, (int)CardValueEnum.Two)

				}),
				new PlayerHand(new Player("Stacy"), new List<Card>
				{
					new Card(CardSuitEnum.Heart, (int)CardValueEnum.King),
					new Card(CardSuitEnum.Heart, (int)CardValueEnum.Ten),
					new Card(CardSuitEnum.Heart, (int)CardValueEnum.Seven),
					new Card(CardSuitEnum.Heart, (int)CardValueEnum.Five),
					new Card(CardSuitEnum.Heart, (int)CardValueEnum.Two)

				}),
				new PlayerHand(new Player("Fred"), new List<Card>
				{
					new Card(CardSuitEnum.Diamond, (int)CardValueEnum.Ace),
					new Card(CardSuitEnum.Diamond, (int)CardValueEnum.King),
					new Card(CardSuitEnum.Diamond, (int)CardValueEnum.Queen),
					new Card(CardSuitEnum.Diamond, (int)CardValueEnum.Eight),
					new Card(CardSuitEnum.Diamond, (int)CardValueEnum.Two)

				}),
				new PlayerHand(new Player("Jen"), new List<Card>
				{
					new Card(CardSuitEnum.Club, (int)CardValueEnum.Queen),
					new Card(CardSuitEnum.Club, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Club, (int)CardValueEnum.Ten),
					new Card(CardSuitEnum.Club, (int)CardValueEnum.Nine),
					new Card(CardSuitEnum.Diamond, (int)CardValueEnum.Three)

				})
			};

			var actualResult = _rule.GetChallengers(players);

			// Assert
			actualResult.Count().Should().Be(3);
			actualResult.All(x => x.PlayerHand.PlayerInfo.Name != "Jen").Should().Be(true);

		}


		[Fact]
		public void FlushShowdownRule_FairMode_MultipleWinners()
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
			actualWinners.Count().Should().Be(2);
		}

		[Fact]
		public void FlushShowdownRule_FairMode_OneWinner_2()
		{
			// Act
			var bobsHand = new PlayerHand(new Player("Bob"), new List<Card>
				{
					new Card(CardSuitEnum.Spade, (int)CardValueEnum.Ace),
					new Card(CardSuitEnum.Spade, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Spade, (int)CardValueEnum.Ten),
					new Card(CardSuitEnum.Spade, (int)CardValueEnum.Three),
					new Card(CardSuitEnum.Spade, (int)CardValueEnum.Two)

				});
			var stacysHand = new PlayerHand(new Player("Stacy"), new List<Card>
				{
					new Card(CardSuitEnum.Heart, (int)CardValueEnum.King),
					new Card(CardSuitEnum.Heart, (int)CardValueEnum.Ten),
					new Card(CardSuitEnum.Heart, (int)CardValueEnum.Seven),
					new Card(CardSuitEnum.Heart, (int)CardValueEnum.Five),
					new Card(CardSuitEnum.Heart, (int)CardValueEnum.Two)

				});
			var fredsHand = new PlayerHand(new Player("Fred"), new List<Card>
				{
					new Card(CardSuitEnum.Diamond, (int)CardValueEnum.Ace),
					new Card(CardSuitEnum.Diamond, (int)CardValueEnum.King),
					new Card(CardSuitEnum.Diamond, (int)CardValueEnum.Queen),
					new Card(CardSuitEnum.Diamond, (int)CardValueEnum.Eight),
					new Card(CardSuitEnum.Diamond, (int)CardValueEnum.Two)

				});
			var jensHand = new PlayerHand(new Player("Jen"), new List<Card>
				{
					new Card(CardSuitEnum.Club, (int)CardValueEnum.Queen),
					new Card(CardSuitEnum.Club, (int)CardValueEnum.Jack),
					new Card(CardSuitEnum.Club, (int)CardValueEnum.Ten),
					new Card(CardSuitEnum.Club, (int)CardValueEnum.Nine),
					new Card(CardSuitEnum.Club, (int)CardValueEnum.Three)

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
				},
				new PlayerHandChallengedCards(fredsHand)
				{
					ChallengedCards = fredsHand.Hand
				},
				new PlayerHandChallengedCards(jensHand)
				{
					ChallengedCards = jensHand.Hand
				}
			};

			var actualWinners = _rule.BreakTie(challengers);

			// Assert
			actualWinners.Count().Should().Be(1);
			actualWinners.First().PlayerInfo.Name.Should().Be("Jen");
		}

	
		[Fact]
		public void FlushShowdownRule_Winners_OneWinner()
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
			var stacysHand =
				new PlayerHand(new Player("Stacy"), new List<Card>
				{
					new Card(CardSuitEnum.Diamond, 2),
					new Card(CardSuitEnum.Diamond, 3),
					new Card(CardSuitEnum.Diamond, 4),
					new Card(CardSuitEnum.Diamond, 5),
					new Card(CardSuitEnum.Diamond, 7)

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
