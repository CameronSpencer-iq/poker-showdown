using PokerHandShowdown.Models.Cards;

namespace PokerHandShowdown.Core.Factory.Cards
{
	/// <summary>
	/// A simple implementation for creating a card.
	/// </summary>
	public class CardFactory : ICardFactory
	{
		private readonly CardFactorySettings _cardFactorySettings;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cardFactorySettings">Values that alter the card creation</param>
		public CardFactory(CardFactorySettings cardFactorySettings)
		{
			_cardFactorySettings = cardFactorySettings;
		}

		/// <inheritdoc />
		public Card BuildCard(CardFactoryModel cardFactoryModel)
		{
			var cEnum = cardFactoryModel.CardEnumTuple;
			int value = (int)cEnum.Item2;
			if (_cardFactorySettings.CardValueOverrides.ContainsKey(cEnum.Item2))
			{
				value = _cardFactorySettings.CardValueOverrides[cEnum.Item2];
			}
			return new Card(cEnum.Item1, value);
		}
	}
}
