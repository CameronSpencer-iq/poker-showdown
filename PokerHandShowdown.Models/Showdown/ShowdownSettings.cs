using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHandShowdown.Models.Showdown
{
	/// <summary>
	/// General settings for a showdown
	/// </summary>
	public class ShowdownSettings
	{
		/// <summary>
		/// The number of cards allowed in a player's hand
		/// </summary>
		public int HandSize { get; set; }

		/// <summary>
		/// Configuration to determine how many cards are needed to be identified as a flush
		/// </summary>
		public int FlushHandCount { get; set; }

		/// <summary>
		///  Configurations that will most likely never change.
		/// This might make sense as a constant.
		/// </summary>
		public int ThreeOfKindCount { get; set; }

		/// <summary>
		/// Configurations that will most likely never change.
		/// This might make sense as a constant.
		/// </summary>
		public int OnePairOfKindCount { get; set; }

		/// <summary>
		/// If enabled, More that one winner is allowed (otherwise no one wins)
		/// </summary>
		public bool IsFairMode { get; set; }

		/// <summary>
		/// Inits defaults
		/// </summary>
		public ShowdownSettings()
		{
			HandSize = 5;
			ThreeOfKindCount = 3;
			FlushHandCount = 5;
			IsFairMode = true;
			OnePairOfKindCount = 2;
		}
	}
}
