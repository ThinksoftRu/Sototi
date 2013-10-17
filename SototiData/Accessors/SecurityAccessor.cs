#region Usings

using System;
using BLToolkit.DataAccess;

#endregion

namespace SototiData.Accessors
{
    using BLToolkit.Data;

    using SototiCore.Data.Common;

    using SototiCore.Data.Common;

    /// <summary>
    /// Класс доступа к методам пакета secure_pkg.
    /// </summary>
    public abstract class SecurityAccessor : BaseAccessor
    {
        /// <summary>
        /// Возвращает серверное время.
        /// </summary>
        /// <returns>Дата и время на сервере базы данных.</returns>
        [SqlQuery("select sysdate from dual")]
        protected internal abstract DateTime SysDate();

        /// <summary>
        /// The impersonate.
        /// </summary>
        /// <param name="login">
        /// The login.
        /// </param>
        protected internal void Impersonate(string login)
        {
            Model.SetSpCommand("SECURE_PKG.Impersonate", DbManager.Parameter("p_login", login))
                .ExecuteNonQuery();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        protected internal int GetUserIdByLogin(string login)
        {
            return 1;
            /*return
                Model.SetSpCommand("SECURE_PKG.getUserIdByLogin", DbManager.Parameter("p_user_login", login))
                    .ExecuteScalar<string>(ScalarSourceType.ReturnValue);*/
        }

        protected internal void RegisterSid(Guid sid)
        {
        }

        /// <summary>
        /// The get user state.
        /// </summary>
        /// <param name="login">
        /// The login.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The <see cref="UserState"/>.
        /// </returns>
        protected internal UserState GetUserState(string login, string password)
        {
            return UserState.Active;
            
            return
                Model.SetSpCommand(
                    "SECURE_PKG.GetUserState", DbManager.Parameter("p_user_login", login), DbManager.Parameter("p_user_pass", password))
                    .ExecuteScalar<UserState>(ScalarSourceType.ReturnValue);
        }
    }
}