using PokerHandShowdown.Models.Cards;
using System.Collections.Generic;

namespace PokerHandShowdown.Models.Players
{
	/// <summary>
	/// For the UI when creating a player's hand
	/// </summary>
	public class PlayerHandFactoryModel
	{
		public string PlayerName { get; set; }
		public IEnumerable<CardFactoryModel> Cards { get; set; }
	}
}
