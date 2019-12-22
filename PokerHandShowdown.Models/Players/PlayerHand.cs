using PokerHandShowdown.Models.Cards;
using System.Collections.Generic;

namespace PokerHandShowdown.Models.Players
{
	/// <summary>
	/// 
	/// </summary>
	public class PlayerHand
	{
		public Player PlayerInfo { get; set; }	
		public IEnumerable<Card> Hand { get; set; }

		/// <summary>
		/// Empty hand
		/// </summary>
		public PlayerHand()
		{
			Hand = Hand ?? new List<Card>();
		}

		/// <summary>
		/// Empty player hand
		/// </summary>
		/// <param name="playerInfo"></param>
		public PlayerHand(Player playerInfo) : this()
		{
			PlayerInfo = playerInfo;
		}

		/// <summary>
		/// Player with Hand
		/// </summary>
		/// <param name="playerInfo"></param>
		/// <param name="hand"></param>
		public PlayerHand(Player playerInfo, IEnumerable<Card> hand) : this(playerInfo)
		{
			Hand = hand;
		}
	}
}
