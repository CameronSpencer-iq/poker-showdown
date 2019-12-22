using PokerHandShowdown.Models.Players;
using PokerHandShowdown.Models.Showdown;
using System.Linq;

namespace PokerHandShowdown.Core.Validation.Players
{
	/// <summary>
	/// Validation Implementation for PlayerHandFactoryModel
	/// </summary>
	public class PlayerHandFactoryModelValidation : ISimpleValidation<PlayerHandFactoryModel>
	{
		private readonly ShowdownSettings _showDownSettings;

		/// <summary>
		/// Simple Validation to make sure the basics are met.
		/// </summary>
		/// <param name="settings"></param>
		public PlayerHandFactoryModelValidation(ShowdownSettings settings)
		{
			_showDownSettings = settings;
		}

		/// <inheritdoc />
		public bool Validate(PlayerHandFactoryModel model)
		{
			return !string.IsNullOrEmpty(model.PlayerName) && model.Cards.Count() == _showDownSettings.HandSize;
		}
	}
}
