
using System.Web.Mvc;

namespace SototiSite.Controllers
{
    using System;

    using SototiCore.DataProviders;

    using SototiCore.Data;

    public class UserController : BaseController
    {
        public UserController(ILogger log, IDataContext dataContext, ILocalizer localizer)
            : base(log, dataContext, localizer)
        {
        }

        public ActionResult Index()
        {
            var dc = this.DataContext;
            var dp = dc.Get<ISecurityDataProvider>();
            Log.Trace("Index в {0}", DateTime.Now);
            return View();
        }

        #region Заголовок всех страниц (синяя полоса), отрисовывается в _Layout.cshtml

        /// <summary>
        /// The base info.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult BaseInfo()
        {
            return this.View((object)User.Identity.Name);
        }

        #endregion

        #region User profile

        /// <summary>
        /// The u ser profile.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult UserProfile()
        {
            return this.View();
        }

        #endregion

        #region User settings

        /// <summary>
        /// The user settings.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult UserSettings()
        {
            return this.View();
        }

        #endregion
    }
}
