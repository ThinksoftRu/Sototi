#region Usings

using System.Web;

#endregion

namespace Sototi.Web
{
    using System;

    using SototiCore.Data.Common;

    /// <summary>
    /// Текущий пользователь запроса.
    /// </summary>
    public class RequestUser : ICurrentUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestUser"/> class.
        /// </summary>
        public RequestUser()
        {
            var httpContext = HttpContext.Current;
            if (httpContext == null || httpContext.User == null) return;

            Uid = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : null;

            var sid = httpContext.Request.Cookies.Get("SID");
            if (sid != null) Sid = Guid.Parse(sid.Value);
        }

        #region Implementation of ICurrentUser

        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public string Uid { get; set; }


        public Guid? Sid { get; set; }

        #endregion
    }
}