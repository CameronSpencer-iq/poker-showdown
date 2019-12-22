using PokerHandShowdown.Models.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHandShowdown.Cli
{
	public interface IApp
	{
		IEnumerable<PlayerHand> AddPlayers();

		void Run(IEnumerable<PlayerHand> playerHands);
	}
}
