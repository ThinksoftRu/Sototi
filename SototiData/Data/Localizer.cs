namespace SototiData
{
	using SototiCore.Data;

	using SototiData;

	/// <summary>
	/// The localizer.
	/// </summary>
	public class Localizer : ILocalizer
	{
		/// <summary>
		/// TВозвращает строку ко ключу.
		/// </summary>
		/// <param name="key">
		/// Значение ключа.
		/// </param>
		/// <returns>
		/// Локализованная строка.
		/// </returns>
		public string this[string key]
		{
			get
			{
				return Localization.ResourceManager.GetString(key);
			}
		}
	}
}
