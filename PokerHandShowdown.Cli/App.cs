using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PokerHandShowdown.Core.Factory.Players;
using PokerHandShowdown.Core.Logic;
using PokerHandShowdown.Models.Cards;
using PokerHandShowdown.Models.Players;
using PokerHandShowdown.Models.Showdown;

namespace PokerHandShowdown.Cli
{
	public class App : IApp
	{
		private readonly IPlayerHandFactory _playerHandFactory;
		private readonly IPokerShowdown _pokerShowdown;

		public App(IPlayerHandFactory playerHandFactory, IPokerShowdown pokerShowdown)
		{
			_playerHandFactory = playerHandFactory;
			_pokerShowdown = pokerShowdown;
		}
		public IEnumerable<PlayerHand> AddPlayers()
		{
			var playerHandModels = new List<PlayerHandFactoryModel>();
			while (true)
			{
				Console.WriteLine("Enter Player Name");
				var playerName = Console.ReadLine();

				Console.WriteLine("Enter Player Hand (formatted like Ks-Kd-Kc-4c-8h)");
				var playerHandString = Console.ReadLine();
				var regex = new Regex(@"([JQAK\d]*)([sdch])");
				var matches = regex.Matches(playerHandString);

				var cardModels = new List<CardFactoryModel>();
				foreach (Match match in matches)
				{
					var value = match.Groups[1].Value.ToUpper();
					var suit = match.Groups[2].Value;

					cardModels.Add(new CardFactoryModel { CardEnumTuple = (GetSuit(suit), GetValue(value)) });
				}

				var playerHandModel = new PlayerHandFactoryModel
				{
					PlayerName = playerName,
					Cards = cardModels
				};
				playerHandModels.Add(playerHandModel);

				Console.WriteLine("Add another? (y/n)");
				var s = Console.ReadLine();
				if (s.Equals("n"))
				{
					break;
				}
			}

			return playerHandModels.Select(phm => _playerHandFactory.BuildHand(phm));
		}

		public void Run(IEnumerable<PlayerHand> playerHands)
		{
			var showdownResults = _pokerShowdown.RevealWinner(new ShowdownRequest()
			{
				PlayerHands = playerHands
			});
			Console.WriteLine($"Winner(s): {string.Join(",", showdownResults.Winners.Select(x => x.PlayerInfo.Name))}");
			Console.WriteLine($"Press enter to exit.");
			Console.ReadLine();
		}

		private CardValueEnum GetValue(string value)
		{
			try
			{
				switch (value)
				{
					case "J": return CardValueEnum.Jack;
					case "Q": return CardValueEnum.Queen;
					case "K": return CardValueEnum.King;
					case "A": return CardValueEnum.Ace;
					default: return (CardValueEnum)int.Parse(value);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Something happened when parsing card value", ex);
			}

		}

		private CardSuitEnum GetSuit(string suit)
		{
			try
			{
				switch (suit)
				{
					case "s": return CardSuitEnum.Spade;
					case "d": return CardSuitEnum.Diamond;
					case "c": return CardSuitEnum.Club;
					case "h": return CardSuitEnum.Heart;
					default: throw new Exception("Unable to determine suit");
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Something happened when parsing card suit", ex);
			}

		}
	}
}
