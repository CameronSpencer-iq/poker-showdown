namespace PokerHandShowdown.Models.Cards
{
	/// <summary>
	/// A model for UIs to use for creating a valid card
	/// </summary>
	public class CardFactoryModel
	{
		/// <summary>
		/// 
		/// </summary>
		public (CardSuitEnum, CardValueEnum) CardEnumTuple { get; set; }
	}
}
