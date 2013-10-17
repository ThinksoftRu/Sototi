#region Usings

using System;


#endregion

namespace SototiData
{
	using System.Data;

	using SototiCore.Exceptions;

	using SototiCore.Data;

	/// <summary>
	/// Вспомогательный класс для вызовов методов DataAccessor-ов и стандартной обработки ошибок данных.
	/// </summary>
	/// <typeparam name="TR">Тип возвращаемого результата.</typeparam>
	internal class FluentInvoker<TR> : FluentInvokerBase<TR>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FluentInvoker{TR}"/> class.
		/// </summary>
		/// <param name="action">Делегат для вызова метода.</param>
		public FluentInvoker(Func<TR> action) : base(action)
		{
		}

		/// <summary>
		/// Добавляет новый обработчик в словарь с делегатами частных ошибок.
		/// </summary>
		/// <param name="code">Код (номер) ошибки.</param>
		/// <param name="func">Делегат для формирование исключения.</param>
		/// <returns>Текущий экземпляр построителя.</returns>
		public FluentInvoker<TR> OnError(int code, Func<DataException, SototiException> func)
		{
			AddErrorHandler(code, func);
			return this;
		}

		/// <summary>
		/// Задает логгер в который будут писаться сообщения об ошибках.
		/// </summary>
		/// <param name="log">Логгер для записи ошибок.</param>
		/// <returns>Текущий экземпляр построителя.</returns>
		public FluentInvoker<TR> LogTo(ILogger log)
		{
			SetLogger(log);
			return this;
		}

		/// <summary>
		/// Функция возвращает результат выполнения делегата.
		/// </summary>
		/// <returns>Результат операции.</returns>
		/// <exception cref="SototiException">В случае возникновения обработанной ошибоки вызова делегата.</exception>
		/// <exception cref="InternalException">В случае возникновения необработанной ошибоки вызова делегата.</exception>
		public TR Exec()
		{
			return ExecInternal();
		}
	}
}