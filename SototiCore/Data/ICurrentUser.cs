#region Usings

#endregion

namespace SototiCore.Data.Common
{
    using System;

    /// <summary>
    /// Информация о текущем пользователе.
    /// </summary>
    public interface ICurrentUser
    {
        /// <summary>
        /// ID пользователя.
        /// </summary>
        /// 
        /// 
        string Uid { get; set; }
        /// <summary>
        /// Идентификатор сессии
        /// </summary>
        Guid? Sid { get; set; }
    }
}