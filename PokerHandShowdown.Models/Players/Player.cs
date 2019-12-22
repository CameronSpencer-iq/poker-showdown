using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHandShowdown.Models.Players
{
	/// <summary>
	/// 
	/// </summary>
	public class Player
	{
		public string Name { get; set; }

		/// <summary>
		/// Player defaulted to Unknown
		/// </summary>
		public Player()
		{
			Name = "Unknown";
		}

		/// <summary>
		/// Initialize player with name
		/// </summary>
		/// <param name="name"></param>
		public Player(string name)
		{
			Name = name;
		}
	}
}
