using PokerHandShowdown.Models.Showdown;

namespace PokerHandShowdown.Core.Logic
{
	/// <summary>
	/// The Interface for executing a Showdown
	/// </summary>
	public interface IPokerShowdown
	{
		ShowdownResponse RevealWinner(ShowdownRequest request);
	}
}
