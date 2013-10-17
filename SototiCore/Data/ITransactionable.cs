#region Usings

using System;

#endregion

namespace SototiCore.Data
{
	/// <summary>
	/// Интерфейс для поддержки транзакционности.
	/// </summary>
	public interface ITransactionable
	{
		/// <summary>
		/// Начать транзакцию.
		/// </summary>
		void BeginTransaction();

		/// <summary>
		/// Потдтвердить транзакцию.
		/// </summary>
		void CommitTransaction();

		/// <summary>
		/// Откатить транзакцию.
		/// </summary>
		void RollbackTransaction();

		/// <summary>
		/// Сохраняет savepoint.
		/// </summary>
		/// <param name="savepointName">Наименование savepoint.</param>
		void SaveTransaction(string savepointName);

		/// <summary>
		/// Откатывает транзакцию до заданного savepoint.
		/// </summary>
		/// <param name="savepointName">Наименование savepoint.</param>
		void Rollback(string savepointName);
	}
}