#region Usings

using System;
using System.Collections.Generic;
using System.Data;

#endregion

namespace SototiData
{

    using SototiCore.Data;
    using SototiCore.Data.Common;
    using SototiCore.DataProviders;
    using SototiCore.Exceptions;
    using SototiData;
    using SototiData.DataProviders;

    using SototiCore.Data;

    /// <summary>
    /// Реализация контекста данных приложения.
    /// </summary>
    public class DataContext : IDataContext
    {
        #region Fields

        /// <summary>
        /// Уникальный идентификатор контекста.
        /// </summary>
        private readonly string _uuid = Guid.NewGuid().ToString("n");

        /// <summary>
        /// Логирование событий.
        /// </summary>
        private readonly ILogger _log;

        /// <summary>
        /// Фабрика поставщиков данных.
        /// </summary>
        private readonly IDataProviderFactory _factory;

        /// <summary>
        /// Кеш созданных поставщиков данных.
        /// </summary>
        private readonly IDictionary<string, IDataProvider> _dataProviders;

        /// <summary>
        /// Признак освобождения ресурсов.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Вложенность транзакции.
        /// </summary>
        private int _transactionLevel;

        /// <summary>
        /// Признак того что транзакцию надо откатить.
        /// </summary>
        private bool _rollback;

        /// <summary>
        /// Текущая транзакция.
        /// </summary>
        private IDbTransaction _transaction;

        /// <summary>
        /// Признак области действия соединения.
        /// </summary>
        //private bool _isScopeExists;

        /// <summary>
        /// Модель данных.
        /// </summary>
        private SototiDbManager dbManager;

        #endregion

        #region Internal свойства



        /// <summary>
        /// Модель данных.
        /// </summary>
        internal SototiDbManager DbManager
        {
            get
            {
				//if (_isScopeExists == false && this.dbManager == null)
				//{
				//	_log.Error("Использование модели вне области действия соединения.");
				//	throw new InternalException("Использование модели вне области действия соединения.");
				//}

                return this.dbManager ?? (this.dbManager = CreateModel());
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DbDataProvider"/> class.
        /// </summary>
        /// <param name="log">Логирование событий.</param>
        /// <param name="factory">Фабрика поставщиков данных.</param>
        /// <param name="user">Контекст текущего пользователя.</param>
        public DataContext(ILogger log, IDataProviderFactory factory, ICurrentUser user)
        {
            _log = log;
            _factory = factory;
            _dataProviders = new Dictionary<string, IDataProvider>();
            //_isScopeExists = false;
            _user = user;
        }

        #region Implementation of IDataContext

        /// <summary>
        /// Возвращает экземпляр поставщика данных.
        /// </summary>
        /// <typeparam name="T">Тип поставщика данных.</typeparam>
        /// <returns>Экземпляр поставщика данных.</returns>
        public T Get<T>() where T : IDataProvider
        {
            var typeName = typeof(T).FullName;
            if (!_dataProviders.ContainsKey(typeName))
            {
                var dataProvider = _factory.Create<T, DataContext>(this);
                _dataProviders.Add(typeName, dataProvider);
            }

            return (T)_dataProviders[typeName];
        }

        /// <summary>
        /// Создает новую область ограничивающую открытость соединения.
        /// </summary>
        /// <returns>Возвращает новый марекрный обхект области использования соединения.</returns>
        public IConnectionScope ConnectionScope()
        {
            // Соединение надо закрывать если нет активной области действия соединения.
            var closeConnection = //_isScopeExists == false && 
				this.dbManager == null;
            //_isScopeExists = true;
            return new DataConnectionScope(this, closeConnection);
        }

        private ICurrentUser _user;

        #endregion

        #region Implementation of ITransactionable

        /// <summary>
        /// Начать транзакцию.
        /// </summary>
        public void BeginTransaction()
        {
            if (_transactionLevel++ == 0)
            {
                _transaction = DbManager.BeginTransaction().Transaction;
                _log.Trace("Новая транзакция: {0}:{1}.", _transactionLevel, _uuid);
            }
            else
            {
                _log.Trace("Повышение вложенности транзакции: {0}:{1}.", _transactionLevel, _uuid);
            }
        }

        /// <summary>
        /// Потдтвердить транзакцию.
        /// </summary>
        public void CommitTransaction()
        {
            _log.Trace("Отметка о подтверждении транзакция: {0}:{1}.", _transactionLevel, _uuid);
            CheckTransaction();
        }

        /// <summary>
        /// Откатить транзакцию.
        /// </summary>
        public void RollbackTransaction()
        {
            _log.Trace("Отметка об откате транзакция: {0}:{1}.", _transactionLevel, _uuid);
            _rollback = true;
            CheckTransaction();
        }

        /// <summary>
        /// Сохраняет savepoint.
        /// </summary>
        /// <param name="savepointName">Наименование savepoint.</param>
        public void SaveTransaction(string savepointName)
        {
            /*
                        _log.Trace("Создание точки отката: {0}:{1}:{2}.", _transactionLevel, _uuid, savepointName);

                        if (_transactionLevel <= 0)
                            throw new InternalException("Нельзя сделать точку отката вне транзакции.");

                        ((OracleTransaction)_transaction).Save(savepointName);
            */
        }

        /// <summary>
        /// Откатывает транзакцию до заданного savepoint.
        /// </summary>
        /// <param name="savepointName">Наименование savepoint.</param>
        public void Rollback(string savepointName)
        {
            /*
                        _log.Trace("Откат до точки: {0}:{1}:{2}.", _transactionLevel, _uuid, savepointName);
                        ((OracleTransaction)_transaction).Rollback(savepointName);
            */
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Implement IDisposable.
        /// Do not make this method virtual.
        /// A derived class should not be able to override this method.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// Создает новую модель даннях.
        /// </summary>
        /// <returns>Возвращает новую модель.</returns>
        private SototiDbManager CreateModel()
        {
            return new SototiDbManager(_user);
        }

        /// <summary>
        /// Закрывает соединение и отменяет активную транзакцию, если она есть.
        /// </summary>
        protected internal virtual void Close()
        {
            // При необходимости закрываем транзакцию.
            if (_transaction != null)
            {
                CheckTransaction(true);
            }

            // Закрываем соединение с базой данных.
            if (this.dbManager != null)
            {
                this.dbManager.Dispose();
                this.dbManager = null;
            }

            //_isScopeExists = false;
        }

        /// <summary>
        /// Dispose(bool disposing) executes in two distinct scenarios.
        /// If disposing equals true, the method has been called directly
        /// or indirectly by a user's code. Managed and unmanaged resources
        /// can be disposed.
        /// </summary>
        /// <param name="disposing">
        /// If disposing equals false, the method has been called by the
        /// runtime from inside the finalizer and you should not reference
        /// other objects. Only unmanaged resources can be disposed.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (this.DbManager != null)
                    {
                        if (_transaction != null)
                        {
                            _log.Trace("Откат транзакции при освобождении контекста: {0}:{1}.", _transactionLevel, _uuid);
                            this.DbManager.RollbackTransaction();
                            _transaction = null;
                        }

                        this.DbManager.Dispose();
                    }
                }

                _disposed = true;
            }
        }

        /// <summary>
        /// Завершение транзакции.
        /// </summary>
        /// <param name="close">Признак необходимости принудительного закрытия транзакции.</param>
        private void CheckTransaction(bool close = false)
        {
            if (close && _transactionLevel != 0)
            {
                _log.Warn("Выход транзакции '{0}' за область действия соединения.", _uuid);
                _rollback = true;
                _transactionLevel = 1;
            }

            if (--_transactionLevel > 0) return;

            if (_rollback)
            {
                _log.Trace("Откат транзакции: {0}:{1}.", _transactionLevel, _uuid);
                DbManager.RollbackTransaction();
            }
            else
            {
                _log.Trace("Подтверждение транзакции: {0}:{1}.", _transactionLevel, _uuid);
                DbManager.CommitTransaction();
            }

            _transaction = null;
        }

        /// <summary>
        /// Контекст видимости соединения.
        /// </summary>
        private sealed class DataConnectionScope : IConnectionScope
        {
            /// <summary>
            /// Текущий контекст данных.
            /// </summary>
            private readonly DataContext context;

            /// <summary>
            /// Признак необходимости закрыть родительское соединение.
            /// </summary>
            private readonly bool close;

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Object"/> class.
            /// </summary>
            /// <param name="context">Контекст данных.</param>
            /// <param name="close">Признак необходимости закрывать соединение после выхода из области.</param>
            public DataConnectionScope(DataContext context, bool close)
            {
                this.context = context;
                this.close = close;
            }

            /// <summary>
            /// При необходимости закрывает родительское соединение.
            /// </summary>
            public void Dispose()
            {
                if (this.close)
                {
                    this.context.Close();
                }
            }
        }
    }
}