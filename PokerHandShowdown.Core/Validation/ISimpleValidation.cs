namespace PokerHandShowdown.Core.Validation
{
	/// <summary>
	/// Simple validation
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface ISimpleValidation<T>
	{
		/// <summary>
		/// Will return if the item is valid or not.
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		bool Validate(T model);
	}
}
