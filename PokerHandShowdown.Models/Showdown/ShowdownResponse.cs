using PokerHandShowdown.Models.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHandShowdown.Models.Showdown
{
	/// <summary>
	/// Response model when showdown executed
	/// </summary>
	public class ShowdownResponse
	{
		/// <summary>
		/// Winners. There can be more than 1
		/// </summary>
		public IEnumerable<PlayerHand> Winners { get; set; }

		/// <summary>
		/// Response model when showdown executed
		/// </summary>
		public ShowdownResponse()
		{
			Winners = new List<PlayerHand>();
		}
	}
}
