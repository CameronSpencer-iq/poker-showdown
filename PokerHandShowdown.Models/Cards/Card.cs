using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHandShowdown.Models.Cards
{
	/// <summary>
	/// A card
	/// </summary>
	public class Card
	{
		/// <summary>
		/// 
		/// </summary>
		public string Suit  { get; }

		/// <summary>
		/// 
		/// </summary>
		public int Value { get; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="suit"></param>
		/// <param name="value"></param>
		public Card(CardSuitEnum suit, int value)
		{
			Suit = suit.ToString();
			Value = value;
		}

		public override int GetHashCode()
		{
			return (Suit, Value).GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if(obj is Card c)
			{
				return (Suit, Value).Equals((c.Suit, c.Value));
			}
			else
			{
				return false;
			}
		}
	}
}
