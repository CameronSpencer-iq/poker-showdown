using PokerHandShowdown.Models.Cards;

namespace PokerHandShowdown.Core.Factory.Cards
{
	/// <summary>
	/// Responsible for creating a Card.
	/// </summary>
	public interface ICardFactory
	{
		/// <summary>
		/// Build action based on the given model
		/// </summary>
		/// <param name="cardFactoryModel"></param>
		/// <returns></returns>
		Card BuildCard(CardFactoryModel cardFactoryModel);
	}
}
