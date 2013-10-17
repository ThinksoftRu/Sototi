namespace SototiCore.Data
{
	/// <summary>
	/// The Localizer interface.
	/// </summary>
	public interface ILocalizer
	{
		/// <summary>
		/// The this.
		/// </summary>
		/// <param name="key">
		/// The key.
		/// </param>
		/// <returns>
		/// The <see cref="string"/>.
		/// </returns>
		string this[string key] { get; }

	}
}
