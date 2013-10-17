#region Usings

using System;

#endregion

namespace SototiCore.Data
{
	using SototiCore.DataProviders;

	/// <summary>
	/// Интерфейс для анонимного контекста работы с данными.
	/// </summary>
	public interface IDataContext : ITransactionable
	{
		/// <summary>
		/// Возвращает экземпляр поставщика данных.
		/// </summary>
		/// <typeparam name="T">Тип поставщика данных.</typeparam>
		/// <returns>Экземпляр поставщика данных.</returns>
		T Get<T>() where T : IDataProvider;

		/// <summary>
		/// Создает новую область ограничивающую открытость соединения.
		/// </summary>
		/// <returns>Область ограничивающая жизнь соединения.</returns>
		IConnectionScope ConnectionScope();
	}
}