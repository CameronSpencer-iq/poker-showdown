using FluentAssertions;
using PokerHandShowdown.Core.Factory.Cards;
using PokerHandShowdown.Core.Factory.Players;
using PokerHandShowdown.Core.Validation;
using PokerHandShowdown.Core.Validation.Players;
using PokerHandShowdown.Models.Cards;
using PokerHandShowdown.Models.Players;
using PokerHandShowdown.Models.Showdown;
using System;
using System.Linq;
using Xunit;

namespace PokerHandShowdown.Tests.Unit.Factory
{
	public class PlayerHandFactoryUnitTests : IDisposable
	{
		private readonly IPlayerHandFactory _playerHandFactory;
		private readonly ShowdownSettings _showDownSettings;

		public PlayerHandFactoryUnitTests()
		{
			// Arrange
			ICardFactory cardFactory = new CardFactory(new CardFactorySettings());
			_showDownSettings = new ShowdownSettings { HandSize = 5 };
			ISimpleValidation<PlayerHandFactoryModel> validator = new PlayerHandFactoryModelValidation(_showDownSettings);

			_playerHandFactory = new PlayerHandFactory(validator, cardFactory);
		}

		[Fact]
		public void PlayerHandFactory_Basic_ValidInput()
		{
			// Act
			var model = new PlayerHandFactoryModel
			{
				PlayerName = "Bob",
				Cards = new CardFactoryModel[] {
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Ace) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Eight) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Five) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Four) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.King) }
				}
			};
			var actualPlayerHand = _playerHandFactory.BuildHand(model);

			// Assert
			actualPlayerHand.PlayerInfo.Name.Should().Be("Bob");
			actualPlayerHand.Hand.Count().Should().Be(_showDownSettings.HandSize);
		}

		[Fact]
		public void PlayerHandFactory_Basic_InvalidInput()
		{
			// Act
			var model = new PlayerHandFactoryModel
			{
				PlayerName = "Bob",
				Cards = new CardFactoryModel[] {
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Ace) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Eight) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Five) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Four) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.King) },
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Spade, CardValueEnum.Nine) }
				}
			};
			
			_playerHandFactory
				.Invoking(p => p.BuildHand(model))
				.Should() // Assert
				.Throw<ArgumentException>();
		}

		public void Dispose()
		{

		}
	}
}
