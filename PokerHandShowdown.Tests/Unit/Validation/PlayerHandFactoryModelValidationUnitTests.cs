using FluentAssertions;
using PokerHandShowdown.Core.Factory.Cards;
using PokerHandShowdown.Core.Factory.Players;
using PokerHandShowdown.Core.Validation;
using PokerHandShowdown.Core.Validation.Players;
using PokerHandShowdown.Models.Cards;
using PokerHandShowdown.Models.Players;
using PokerHandShowdown.Models.Showdown;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PokerHandShowdown.Tests.Unit.Validation
{
	public class PlayerHandFactoryModelValidationUnitTests : IDisposable
	{
		private readonly ShowdownSettings _showDownSettings;
		private readonly ISimpleValidation<PlayerHandFactoryModel> _validator;
		public PlayerHandFactoryModelValidationUnitTests()
		{
			// Arrange
			_showDownSettings = new ShowdownSettings { HandSize = 5 };
			_validator = new PlayerHandFactoryModelValidation(_showDownSettings);
		}

		[Fact]
		public void PlayerHandFactoryModelValidation_Basic_ValidInput()
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
			var actualValidationResult = _validator.Validate(model);

			// Assert
			actualValidationResult.Should().Be(true);
		}

		[Fact]
		public void PlayerHandFactoryModelValidation_Basic_InvalidInput()
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
					new CardFactoryModel { CardEnumTuple = (CardSuitEnum.Spade, CardValueEnum.King) }
				}
			};
			var actualValidationResult = _validator.Validate(model);

			// Assert
			actualValidationResult.Should().Be(false, because: $"The showdown settings is setup to have a handsize of 5. Card count is {model.Cards.Count()}");
		}


		public void Dispose()
		{
			
		}
	}
}
