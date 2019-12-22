using Microsoft.Extensions.Logging;
using PokerHandShowdown.Models.Showdown;

namespace PokerHandShowdown.Core.Logic.Rules
{
	/// <summary>
	/// Three of a Kind Showdown rule
	/// </summary>
	public class ThreeOfKindShowdownRule : NOfaKindShowdownRule<ThreeOfKindShowdownRule>
	{
		/// <summary>
		/// When "n" is 3
		/// </summary>
		/// <param name="priority"></param>
		/// <param name="showdownSettings"></param>
		public ThreeOfKindShowdownRule(int priority, ShowdownSettings showdownSettings, ILoggerFactory logger) : base(showdownSettings.ThreeOfKindCount, priority, showdownSettings, logger)
		{
		}

	}
}
