#region Usings

using System;
using System.Collections.Generic;
using BLToolkit.Data;
using SototiCore.Exceptions;

#endregion

namespace SototiData
{
	using SototiData;

	using SototiCore.Data;

	/// <summary>
    /// Вспомогательный класс для вызовов методов DataAccessor-ов и стандартной обработки ошибок данных.
    /// </summary>
    /// <typeparam name="TR">Тип возвращаемого результата.</typeparam>
    internal class FluentInvokerBase<TR>
    {
        /// <summary>
        /// Словарь с делегатами частных ошибок.
        /// </summary>
        private readonly IDictionary<int?, Func<DataException, SototiException>> _errors;

        /// <summary>
        /// Делегат для вызова метода.
        /// </summary>
        private readonly Func<TR> _action;

        /// <summary>
        /// Логирование событий.
        /// </summary>
        private ILogger _log;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentInvokerBase{TR}"/> class.
        /// </summary>
        /// <param name="action">Делегат для вызова метода.</param>
        public FluentInvokerBase(Func<TR> action)
        {
            _action = action;
            _errors = new Dictionary<int?, Func<DataException, SototiException>>();
        }

        /// <summary>
        /// Добавляет новый обработчик в словарь с делегатами частных ошибок.
        /// </summary>
        /// <param name="code">Код (номер) ошибки.</param>
        /// <param name="func">Делегат для формирование исключения.</param>
        protected void AddErrorHandler(int code, Func<DataException, SototiException> func)
        {
            _errors[code] = func;
        }

        /// <summary>
        /// Задает логгер в который будут писаться сообщения об ошибках.
        /// </summary>
        /// <param name="log">Логгер для записи ошибок.</param>
        protected void SetLogger(ILogger log)
        {
            _log = log;
        }

        /// <summary>
        /// Функция возвращает результат выполнения делегата.
        /// </summary>
        /// <returns>Результат операции.</returns>
        /// <exception cref="SototiException">В случае возникновения обработанной ошибоки вызова делегата.</exception>
        /// <exception cref="InternalException">В случае возникновения необработанной ошибоки вызова делегата.</exception>
        protected TR ExecInternal()
        {
            try
            {
                return _action();
            }
            catch (DataException e)
            {
                if (_log != null) _log.Warn(e, e.Message);

                if (e.Number.HasValue && _errors.ContainsKey(e.Number)) throw _errors[e.Number](e);

                DecodeAndThrow(e);
                throw e;
            }
            catch (SototiException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (_log != null) _log.Fatal(e, e.Message);
	            throw new InternalException(e, "Unhandled");
            }
        }

        #region Функции обработки ошибок

        /// <summary>
        /// Генерирует декодированное сообщение об ошибке.
        /// </summary>
        /// <param name="exception">Код ошибки.</param>
        private static void DecodeAndThrow(DataException exception)
        {
            switch (exception.Number)
            {
                case 20005:
                throw new SototiException("ER-20005", exception.ParseMessage());

                case 20006:
                throw new SototiException("ER-20006", exception.ParseMessage());
            }
        }

        #endregion
    }
}