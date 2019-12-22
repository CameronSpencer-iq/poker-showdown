using PokerHandShowdown.Models.Cards;
using System.Collections.Generic;

namespace PokerHandShowdown.Models.Players
{
	/// <summary>
	/// 
	/// </summary>
	public class PlayerHandChallengedCards
	{
		public PlayerHand PlayerHand { get; set; }	
		public IEnumerable<Card> ChallengedCards { get; set; }

		/// <summary>
		/// Empty Response for Player hands that can possible win
		/// </summary>
		public PlayerHandChallengedCards()
		{
			ChallengedCards = ChallengedCards ?? new List<Card>();
		}

		/// <summary>
		/// A player with an empty collection of hands + Challenged cards
		/// </summary>
		/// <param name="playerHand"></param>
		public PlayerHandChallengedCards(PlayerHand playerHand): this()
		{
			PlayerHand = playerHand;
		}

	}
}
