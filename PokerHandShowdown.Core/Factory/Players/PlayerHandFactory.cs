using System;
using System.Linq;
using PokerHandShowdown.Core.Factory.Cards;
using PokerHandShowdown.Core.Validation;
using PokerHandShowdown.Models.Players;

namespace PokerHandShowdown.Core.Factory.Players
{
	/// <summary>
	/// Simple implementation to build a player's hand
	/// </summary>
	public class PlayerHandFactory : IPlayerHandFactory
	{
		private readonly ISimpleValidation<PlayerHandFactoryModel> _simpleValidation;
		private readonly ICardFactory _cardFactory;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="simpleValidation"></param>
		/// <param name="cardFactory"></param>
		public PlayerHandFactory(ISimpleValidation<PlayerHandFactoryModel> simpleValidation, ICardFactory cardFactory)
		{
			_simpleValidation = simpleValidation;
			_cardFactory = cardFactory;
		}

		/// <inheritdoc />
		public PlayerHand BuildHand(PlayerHandFactoryModel playerHandFactoryModel)
		{
			if(_simpleValidation.Validate(playerHandFactoryModel))
			{
				var playerHand = new PlayerHand()
				{
					PlayerInfo = new Player(playerHandFactoryModel.PlayerName)
				};
				playerHand.Hand = playerHandFactoryModel.Cards.ToArray().Select(c => _cardFactory.BuildCard(c));
				return playerHand;
			} else
			{
				throw new ArgumentException("A Player's hand was not valid.");
			}
		}
	}
}
