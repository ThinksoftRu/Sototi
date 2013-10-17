#region Usings

using System;
using BLToolkit.Data;
using BLToolkit.DataAccess;
using SototiData.Accessors;

#endregion

namespace SototiData
{
    using SototiCore.Data.Common;

    /// <summary>
    /// Фищическая модель данных.
    /// </summary>
    public  class SototiDbManager : DbManager
    {
        #region Поля  Ацессоров


        /// <summary>
        /// Класс доступа к системным процедурам.
        /// </summary>
        private SecurityAccessor securityAccessor;

        #endregion

        private readonly ICurrentUser _user;

        #region Конструкторы

        /// <summary>
        /// Конструктор класса, логинится текущим веб-пользователем.
        /// </summary>
        /// <param name="uid">Уникальный идентификатор текущего пользователя.</param>
        public SototiDbManager(ICurrentUser uid)
        {
            if (uid.Uid == null) return;
            _user = uid;
            _user.Uid = SecurityAccessor.GetUserIdByLogin(uid.Uid).ToString();

            ////Security.Login(uid);
        }

        #endregion

        #region  Публичные свойства

        /// <summary>
        /// Класс доступа к системным процедурам.
        /// </summary>
        public SecurityAccessor SecurityAccessor
        {
            get
            {
                return this.securityAccessor ?? CreateInstance(securityAccessor);
            }
        }

        /// <summary>
        /// Создание нового ацессора и установка его свойства текущего пользователя.
        /// </summary>
        /// <param name="to">
        /// The to.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        private T CreateInstance<T>(T to) where T : BaseAccessor
        {
            to = DataAccessor.CreateInstance<T>(this);
            to.User = _user;
            return to;
        }

        #endregion
    }
}