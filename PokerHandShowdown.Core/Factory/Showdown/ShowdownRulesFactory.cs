using System.Linq;
using Microsoft.Extensions.Logging;
using PokerHandShowdown.Core.Logic.Rules;
using PokerHandShowdown.Models.Showdown;

namespace PokerHandShowdown.Core.Factory.Showdown
{
	/// <summary>
	/// Simple implementation 
	/// </summary>
	public class ShowdownRulesFactory : IShowdownRulesFactory
	{
		private readonly ShowdownSettings _showdownSettings;
		private readonly ILoggerFactory _loggerFactory;
		/// <summary>
		/// Hard-coded implementation of showdown rules
		/// </summary>
		/// <param name="showdownSettings"></param>
		public ShowdownRulesFactory(ShowdownSettings showdownSettings, ILoggerFactory logger)
		{
			_showdownSettings = showdownSettings;
			_loggerFactory = logger;
		}

		/// <inheritdoc />
		public IOrderedEnumerable<AbstractShowdownRule> BuildRules()
		{
			var rulesByPriority = new AbstractShowdownRule[]
			{
				new FlushShowdownRule(priority: 0, _showdownSettings, _loggerFactory),
				new ThreeOfKindShowdownRule(priority: 1, _showdownSettings, _loggerFactory),
				new OnePairShowdownRule(priority: 2, _showdownSettings, _loggerFactory),
				new HighCardShowdownRule(priority: 3, _showdownSettings, _loggerFactory)
			};
			return rulesByPriority.OrderBy(r => r.Priority);
		}
	}
}
