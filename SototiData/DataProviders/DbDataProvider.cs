#region Usings



#endregion

namespace SototiData.DataProviders
{
    using System;
    using System.Threading.Tasks;

    using SototiData;

    using SototiCore.Data;
    using SototiCore.DataProviders;

    using SototiCore.Data;

    /// <summary>
    /// Базовый класс для реализации объектов данных.
    /// </summary>
    public class DbDataProvider : IDataProvider
    {
        /// <summary>
        /// Контекст данных.
        /// </summary>
        private readonly DataContext context;

        /// <summary>
        /// Контекст данных.
        /// </summary>
        public IDataContext Context
        {
            get { return this.context; }
        }


        /// <summary>
        /// Модель данных.
        /// </summary>
        internal SototiDbManager SototiDbManager
        {
            get { return this.context.DbManager; }
        }

        /// <summary>
        /// Логирование событий.
        /// </summary>
        protected ILogger Log { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbDataProvider"/> class.
        /// </summary>
        /// <param name="log">Логирование событий.</param>
        /// <param name="context">Контекст данных.</param>
        public DbDataProvider(ILogger log, IDataContext context)
        {
            this.Log = log;
            this.context = (DataContext) context;
        }

        /// <summary>
        /// Вполняет функцию переданную в качестве аргумента с обработкой ошибок.
        /// </summary>
        /// <typeparam name="TR">Тип результата функции.</typeparam>
        /// <param name="action">Делегат который возвращает результат.</param>
        /// <param name="fluentInvoker">Делегат по дополнительной настройке обработчика ошибок.</param>
        /// <returns>Результат выполнения функции.</returns>
        internal TR Exec<TR>(Func<TR> action, Action<FluentInvoker<TR>> fluentInvoker = null)
        {
            var invoker = new FluentInvoker<TR>(action).LogTo(this.Log);
            if (fluentInvoker != null) fluentInvoker(invoker);
            return invoker.Exec();
        }

        /// <summary>
        /// Вполняет функцию переданную в качестве аргумента с обработкой ошибок.
        /// </summary>
        /// <typeparam name="TR">Тип результата функции.</typeparam>
        /// <param name="action">Делегат который возвращает результат.</param>
        /// <param name="fluentInvoker">Делегат по дополнительной настройке обработчика ошибок.</param>
        /// <returns>Результат выполнения функции.</returns>
         internal Task<TR> ExecAsync<TR>(Func<TR> action, Action<FluentInvoker<TR>> fluentInvoker = null)
        {
            var invoker = new FluentInvoker<TR>(action).LogTo(this.Log);
            if (fluentInvoker != null) fluentInvoker(invoker);
            return Task.Factory.StartNew<TR>(invoker.Exec);
        }

        /// <summary>
        /// Вполняет метод переданную в качестве аргумента с обработкой ошибок.
        /// </summary>
        /// <param name="action">Делегат который выполняется с обработкой ошибок.</param>
        /// <param name="fluentInvoker">Делегат по дополнительной настройке обработчика ошибок.</param>
         internal void Exec(Action action, Action<FluentInvoker> fluentInvoker = null)
        {
            var invoker = new FluentInvoker(action).LogTo(this.Log);
            if (fluentInvoker != null) fluentInvoker(invoker);
            invoker.Exec();
        }

        /// <summary>
        /// Вполняет метод переданную в качестве аргумента с обработкой ошибок.
        /// </summary>
        /// <param name="action">Делегат который выполняется с обработкой ошибок.</param>
        /// <param name="fluentInvoker">Делегат по дополнительной настройке обработчика ошибок.</param>
        /// <returns>Асинхронный результат.</returns>
         internal Task ExecAsync(Action action, Action<FluentInvoker> fluentInvoker = null)
        {
            var invoker = new FluentInvoker(action).LogTo(this.Log);
            if (fluentInvoker != null) fluentInvoker(invoker);
            return Task.Factory.StartNew(invoker.Exec);
        }
    }
}