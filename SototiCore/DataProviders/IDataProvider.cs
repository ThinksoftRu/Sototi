namespace SototiCore.DataProviders
{
	using SototiCore.Data;

	/// <summary>
	/// Базовый интерфейс для Data Provider.
	/// </summary>
	public interface IDataProvider
	{
		/// <summary>
		/// Контекст данных.
		/// </summary>
		IDataContext Context { get; }
	}
}