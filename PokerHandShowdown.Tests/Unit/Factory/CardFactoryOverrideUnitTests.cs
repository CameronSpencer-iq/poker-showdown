using FluentAssertions;
using PokerHandShowdown.Core.Factory.Cards;
using PokerHandShowdown.Models.Cards;
using System;
using System.Collections.Generic;
using Xunit;

namespace PokerHandShowdown.Tests.Unit.Factory
{
	public class CardFactoryUnitTests : IDisposable
	{
		private readonly ICardFactory _cardFactory;
		public CardFactoryUnitTests()
		{
			var cardFactorySettings = new CardFactorySettings
			{
				CardValueOverrides = new Dictionary<CardValueEnum, int> {
				{ CardValueEnum.Jack, 100 }
			}
			};

			_cardFactory = new CardFactory(cardFactorySettings);
		}

		[Fact]
		public void CardFactory_OverridedValues_JackAsOneHundred()
		{
			var model = new CardFactoryModel
			{
				CardEnumTuple = (CardSuitEnum.Club, CardValueEnum.Jack)
			};
			var actualCard = _cardFactory.BuildCard(model);
			actualCard.Value.Should().Be(100);
		}


		public void Dispose()
		{
			
		}
	}
}
