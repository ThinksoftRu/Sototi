#region Usings

using System;
using System.Threading.Tasks;

#endregion

namespace SototiData.DataProviders
{
    using SototiCore.Data.Common;

    using SototiCore.Data;
    using SototiCore.DataProviders;

    using SototiData.DataProviders;

    using SototiCore.Data;

    /// <summary>
    /// Реализация интерфейса доступа к функциям безопасности.
    /// </summary>
    public class SecurityDataProvider : DbDataProvider, ISecurityDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the  <see  cref="SecurityDataProvider"/> class.
        /// </summary>
        /// <param name="log">Логирование событий.</param>
        /// <param name="context">Контекст данных.</param>
        public SecurityDataProvider(ILogger log, IDataContext context)
            : base(log, context)
        {
        }

        #region Implementation of ISecurityDataProvider

        /// <summary>
        /// Возвращает серверное время.
        /// </summary>
        /// <returns>Дата и время на сервере базы данных.</returns>
        public Task<DateTime> ServerDateTimeAsync()
        {
            return ExecAsync(() => SototiDbManager.SecurityAccessor.SysDate());
        }

        /// <summary>
        /// The get user id by login.
        /// </summary>
        /// <param name="login">
        /// The login.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<int> GetUserIdByLogin(string login)
        {
            return this.ExecAsync(() => SototiDbManager.SecurityAccessor.GetUserIdByLogin(login));
        }

        public void RegisterSid(Guid sid)
        {
            ExecAsync(() => SototiDbManager.SecurityAccessor.RegisterSid(sid));
        }

        /// <summary>
        /// The impersonate.
        /// </summary>
        /// <param name="login">
        /// The login.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task Impersonate(string login)
        {
            return ExecAsync(() => SototiDbManager.SecurityAccessor.Impersonate(login));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<UserState> GetUserState(string login, string password)
        {
            return this.ExecAsync(() => SototiDbManager.SecurityAccessor.GetUserState(login, password));
        }
    }

        #endregion
}
