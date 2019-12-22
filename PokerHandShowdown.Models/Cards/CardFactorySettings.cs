using System.Collections.Generic;

namespace PokerHandShowdown.Models.Cards
{
	/// <summary>
	/// 
	/// </summary>
	public class CardFactorySettings
	{
		/// <summary>
		/// When a card is created, this property will determine if the value should be overriden and to what value
		/// </summary>
		public Dictionary<CardValueEnum, int> CardValueOverrides { get; set; }

		/// <summary>
		/// General settings for card creation
		/// </summary>
		public CardFactorySettings()
		{
			CardValueOverrides = new Dictionary<CardValueEnum, int>();
		}
	}
}
