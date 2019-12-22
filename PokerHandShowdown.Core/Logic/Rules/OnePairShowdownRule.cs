using Microsoft.Extensions.Logging;
using PokerHandShowdown.Models.Showdown;

namespace PokerHandShowdown.Core.Logic.Rules
{
	/// <summary>
	/// One Pair Showdown rule
	/// </summary>
	public class OnePairShowdownRule : NOfaKindShowdownRule<OnePairShowdownRule>
	{
		/// <summary>
		/// When "n" is 2
		/// </summary>
		/// <param name="priority"></param>
		/// <param name="showdownSettings"></param>
		public OnePairShowdownRule(int priority, ShowdownSettings showdownSettings, ILoggerFactory logger) : base(showdownSettings.OnePairOfKindCount, priority, showdownSettings, logger)
		{
		}
	}
}
