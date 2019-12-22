using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using PokerHandShowdown.Core.Factory.Cards;
using PokerHandShowdown.Core.Factory.Players;
using PokerHandShowdown.Core.Factory.Showdown;
using PokerHandShowdown.Core.Logic;
using PokerHandShowdown.Core.Validation.Players;
using PokerHandShowdown.Models.Cards;
using PokerHandShowdown.Models.Players;
using PokerHandShowdown.Models.Showdown;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PokerHandShowdown.Tests.Unit.Logic
{
	public class PokerShowdownUnitTests : IDisposable
	{
		// TODO: Test Fixtures
		private readonly IPlayerHandFactory _playerHandFactory;
		private readonly ShowdownSettings _showDownSettings;
		private readonly IPokerShowdown _pokerShowdown;

		public PokerShowdownUnitTests()
		{
			_showDownSettings = new ShowdownSettings();
			_playerHandFactory = new PlayerHandFactory(simpleValidation: new PlayerHandFactoryModelValidation(_showDownSettings), cardFactory: new CardFactory(new CardFactorySettings()));
			_pokerShowdown = new PokerShowdown(showdownRulesFactory: new ShowdownRulesFactory(_showDownSettings, NullLoggerFactory.Instance), NullLoggerFactory.Instance);
		}

		[Fact]
		public void PokerShowdown_Example1_JoeWins()
		{
			var joeHand = _playerHandFactory.BuildHand(new PlayerHandFactoryModel() {
				PlayerName = "Joe",
				Cards = new List<CardFactoryModel>
				{
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Heart, CardValueEnum.Three) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Heart, CardValueEnum.Six) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Heart, CardValueEnum.Eight) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Heart, CardValueEnum.Jack) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Heart, CardValueEnum.King) }
				}
			});

			var jenHand = _playerHandFactory.BuildHand(new PlayerHandFactoryModel()
			{
				PlayerName = "Jen",
				Cards = new List<CardFactoryModel>
				{
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Three) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Diamond, CardValueEnum.Three) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Spade, CardValueEnum.Three) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Eight) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Diamond, CardValueEnum.Ten) }
				}
			});

			var bobsHand = _playerHandFactory.BuildHand(new PlayerHandFactoryModel()
			{
				PlayerName = "Bob",
				Cards = new List<CardFactoryModel>
				{
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Heart, CardValueEnum.Two) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Five) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Spade, CardValueEnum.Seven) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Ten) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Ace) }
				}
			});
			var allHands = new List<PlayerHand>
			{
				joeHand,
				jenHand,
				bobsHand
			};

			var actualPowerShowdownResponse = _pokerShowdown.RevealWinner(new ShowdownRequest { PlayerHands = allHands });
			actualPowerShowdownResponse.Winners.First().PlayerInfo.Name.Should().Be("Joe");
		}

		[Fact]
		public void PokerShowdown_Example2_JenWins()
		{
			var joeHand = _playerHandFactory.BuildHand(new PlayerHandFactoryModel()
			{
				PlayerName = "Joe",
				Cards = new List<CardFactoryModel>
				{
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Heart, CardValueEnum.Three) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Diamond, CardValueEnum.Four) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Nine) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Diamond, CardValueEnum.Nine) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Heart, CardValueEnum.Queen) }
				}
			});

			var jenHand = _playerHandFactory.BuildHand(new PlayerHandFactoryModel()
			{
				PlayerName = "Jen",
				Cards = new List<CardFactoryModel>
				{
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Five) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Diamond, CardValueEnum.Seven) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Heart, CardValueEnum.Nine) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Spade, CardValueEnum.Nine) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Spade, CardValueEnum.Queen) }
				}
			});

			var bobsHand = _playerHandFactory.BuildHand(new PlayerHandFactoryModel()
			{
				PlayerName = "Bob",
				Cards = new List<CardFactoryModel>
				{
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Heart, CardValueEnum.Two) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Two) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Spade, CardValueEnum.Five) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Ten) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Heart, CardValueEnum.Ace) }
				}
			});
			var allHands = new List<PlayerHand>
			{
				joeHand,
				jenHand,
				bobsHand
			};

			var actualPowerShowdownResponse = _pokerShowdown.RevealWinner(new ShowdownRequest { PlayerHands = allHands });
			actualPowerShowdownResponse.Winners.First().PlayerInfo.Name.Should().Be("Jen");
		}

		[Fact]
		public void PokerShowdown_Example3_JoeAndJenWins()
		{
			var joeHand = _playerHandFactory.BuildHand(new PlayerHandFactoryModel()
			{
				PlayerName = "Joe",
				Cards = new List<CardFactoryModel>
				{
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Heart, CardValueEnum.Five) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Diamond, CardValueEnum.Seven) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Nine) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Diamond, CardValueEnum.Nine) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Heart, CardValueEnum.Queen) }
				}
			});

			var jenHand = _playerHandFactory.BuildHand(new PlayerHandFactoryModel()
			{
				PlayerName = "Jen",
				Cards = new List<CardFactoryModel>
				{
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Five) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Heart, CardValueEnum.Seven) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Heart, CardValueEnum.Nine) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Spade, CardValueEnum.Nine) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Spade, CardValueEnum.Queen) }
				}
			});
			var allHands = new List<PlayerHand>
			{
				joeHand,
				jenHand
			};

			var actualPowerShowdownResponse = _pokerShowdown.RevealWinner(new ShowdownRequest { PlayerHands = allHands });
			actualPowerShowdownResponse.Winners.Count().Should().Be(2);
		}

		[Fact]
		public void PokerShowdown_Example4_HighCards_BobWins()
		{
			var joeHand = _playerHandFactory.BuildHand(new PlayerHandFactoryModel()
			{
				PlayerName = "Joe",
				Cards = new List<CardFactoryModel>
				{
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Heart, CardValueEnum.Two) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Diamond, CardValueEnum.Three) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Four) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Diamond, CardValueEnum.Six) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Heart, CardValueEnum.Seven) }
				}
			});

			var jenHand = _playerHandFactory.BuildHand(new PlayerHandFactoryModel()
			{
				PlayerName = "Jen",
				Cards = new List<CardFactoryModel>
				{
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Two) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Diamond, CardValueEnum.Three) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Heart, CardValueEnum.Five) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Spade, CardValueEnum.Six) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Spade, CardValueEnum.Eight) }
				}
			});

			var bobsHand = _playerHandFactory.BuildHand(new PlayerHandFactoryModel()
			{
				PlayerName = "Bob",
				Cards = new List<CardFactoryModel>
				{
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Heart, CardValueEnum.Two) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Three) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Spade, CardValueEnum.Five) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Eight) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Heart, CardValueEnum.Nine) }
				}
			});
			var allHands = new List<PlayerHand>
			{
				joeHand,
				jenHand,
				bobsHand
			};

			var actualPowerShowdownResponse = _pokerShowdown.RevealWinner(new ShowdownRequest { PlayerHands = allHands });
			actualPowerShowdownResponse.Winners.First().PlayerInfo.Name.Should().Be("Bob");
		}



		public void Dispose()
		{
		}
	}
}
