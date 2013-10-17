#region Usings

#endregion

namespace SototiCore.DataProviders
{
	using SototiCore.Data;

	/// <summary>
	/// Фабрика поставщиков данных.
	/// </summary>
	public interface IDataProviderFactory
	{
		/// <summary>
		/// Создает новый экземпляр поставщика данных.
		/// </summary>
		/// <typeparam name="TD">Тип поставщика данных.</typeparam>
		/// <typeparam name="TC">Тип контекста.</typeparam>
		/// <param name="context">Контекст данных.</param>
		/// <returns>Созданный поставщик данных.</returns>
		TD Create<TD, TC>(TC context)
			where TD : IDataProvider
			where TC : IDataContext;
	}
}