using PokerHandShowdown.Models.Players;

namespace PokerHandShowdown.Core.Factory.Players
{
	/// <summary>
	/// Responsible for creating a player's hand
	/// </summary>
	public interface IPlayerHandFactory
	{
		/// <summary>
		/// Build action based on the given model
		/// </summary>
		/// <param name="playerHandFactoryModel"></param>
		/// <returns></returns>
		PlayerHand BuildHand(PlayerHandFactoryModel playerHandFactoryModel);
	}
}
