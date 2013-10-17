#region Usings

using System;
using BLToolkit.Data;
using SototiCore.Exceptions;

#endregion

namespace SototiData
{
	using SototiCore.Data;

	/// <summary>
	/// Вспомогательный класс для вызовов методов DataAccessor-ов и стандартной обработки ошибок данных.
	/// </summary>
	internal class FluentInvoker : FluentInvokerBase<int>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FluentInvoker"/> class.
		/// </summary>
		/// <param name="action">Делегат для вызова метода.</param>
		public	FluentInvoker(Action action)
			: base(() => { action(); return 0; })
		{
		}

		/// <summary>
		/// Добавляет новый обработчик в словарь с делегатами частных ошибок.
		/// </summary>
		/// <param name="code">Код (номер) ошибки.</param>
		/// <param name="func">Делегат для формирование исключения.</param>
		/// <returns>Текущий экземпляр построителя.</returns>
		public FluentInvoker OnError(int code, Func<DataException, SototiException> func)
		{
			AddErrorHandler(code, func);
			return this;
		}

		/// <summary>
		/// Задает логгер в который будут писаться сообщения об ошибках.
		/// </summary>
		/// <param name="log">Логгер для записи ошибок.</param>
		/// <returns>Текущий экземпляр построителя.</returns>
		public FluentInvoker LogTo(ILogger log)
		{
			SetLogger(log);
			return this;
		}

		/// <summary>
		/// Функция возвращает результат выполнения делегата.
		/// </summary>
		/// <exception cref="SototiException">В случае возникновения обработанной ошибоки вызова делегата.</exception>
		/// <exception cref="InternalException">В случае возникновения необработанной ошибоки вызова делегата.</exception>
		public void Exec()
		{
			ExecInternal();
		}
	}
}