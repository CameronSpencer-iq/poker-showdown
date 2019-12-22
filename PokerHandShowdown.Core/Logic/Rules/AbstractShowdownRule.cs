using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PokerHandShowdown.Models.Players;
using PokerHandShowdown.Models.Showdown;

namespace PokerHandShowdown.Core.Logic.Rules
{
	/// <summary>
	/// Abstract class with common properties shared by all showdown rules
	/// </summary>
	public abstract class AbstractShowdownRule : IShowdownRule
	{
		public readonly int Priority;

		protected readonly ShowdownSettings ShowdownSettings;

		/// <summary>
		/// Abstract class with common properties shared by all showdown rules
		/// </summary>
		/// <param name="priority"></param>
		/// <param name="showdownSettings"></param>
		protected AbstractShowdownRule(int priority, ShowdownSettings showdownSettings)
		{
			Priority = priority;
			ShowdownSettings = showdownSettings;
		}

		/// <inheritdoc />
		public abstract IEnumerable<PlayerHandChallengedCards> GetChallengers(IEnumerable<PlayerHand> allPlayerHands);
		public abstract IEnumerable<PlayerHand> BreakTie(IEnumerable<PlayerHandChallengedCards> remainingPlayerHands);

		/// <inheritdoc />
		public IEnumerable<PlayerHand> GetWinners(IEnumerable<PlayerHand> allPlayerHands)
		{
			var challengers = GetChallengers(allPlayerHands);
			var winners = BreakTie(challengers);
			return winners;
		}
	}
}
