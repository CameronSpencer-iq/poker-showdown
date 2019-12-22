using PokerHandShowdown.Models.Players;
using System.Collections.Generic;

namespace PokerHandShowdown.Core.Logic.Rules
{
	/// <summary>
	/// Showdown rules
	/// </summary>
	public interface IShowdownRule
	{
		/// <summary>
		/// Returns a list of players that have been identified as potential winners.
		/// </summary>
		/// <param name="allPlayerHands"></param>
		/// <returns></returns>
		IEnumerable<PlayerHandChallengedCards> GetChallengers(IEnumerable<PlayerHand> allPlayerHands);

		/// <summary>
		/// The potential winner hands are ranked then picked.
		/// </summary>
		/// <param name="remainingPlayerHands"></param>
		/// <returns></returns>
		IEnumerable<PlayerHand> BreakTie(IEnumerable<PlayerHandChallengedCards> remainingPlayerHands);

		/// <summary>
		/// Function that will execute both GetChallengers + BreakTie function to determine Winner for given rule
		/// </summary>
		/// <param name="allPlayerHands"></param>
		/// <returns></returns>
		IEnumerable<PlayerHand> GetWinners(IEnumerable<PlayerHand> allPlayerHands);

	}
}
