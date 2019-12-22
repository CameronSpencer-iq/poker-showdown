using PokerHandShowdown.Models.Players;
using System.Collections.Generic;

namespace PokerHandShowdown.Models.Showdown
{
	/// <summary>
	/// Request model when executing a showdown
	/// </summary>
	public class ShowdownRequest
	{
		public IEnumerable<PlayerHand> PlayerHands { get; set; }
	}
}
