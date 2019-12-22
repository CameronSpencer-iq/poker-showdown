using PokerHandShowdown.Core.Logic.Rules;
using System.Linq;

namespace PokerHandShowdown.Core.Factory.Showdown
{
	/// <summary>
	/// Reponsible for creating the Showdown rules
	/// </summary>
	public interface IShowdownRulesFactory
	{
		/// <summary>
		/// Build action
		/// </summary>
		/// <returns></returns>
		IOrderedEnumerable<AbstractShowdownRule> BuildRules();
	}
}
