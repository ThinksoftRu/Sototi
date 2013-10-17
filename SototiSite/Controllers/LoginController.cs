namespace SototiSite.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

    using SototiCore.Data.Common;
    using SototiCore.Data;
    using SototiCore.DataProviders;

    /// <summary>
    /// The login controller.
    /// </summary>
    public class LoginController : BaseController
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LoginController"/>.
        /// </summary>
        /// <param name="log">
        /// The log.
        /// </param>
        /// <param name="dataContext">
        /// The data context.
        /// </param>
        public LoginController(ILogger log, IDataContext dataContext, ILocalizer localizer)
            : base(log, dataContext, localizer)
        {
        }


        /// <summary>
        /// The login.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <param name="code">
        /// Код авторизации в смс.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public async Task<ActionResult> Login(string username, string password, int? code)
        {
            if (this.Request.IsAuthenticated) return this.RedirectToAction("Index", "User");

            var user = UserState.NotExists;

            using (this.DataContext.ConnectionScope())
            {
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password)) user = await DataContext.Get<ISecurityDataProvider>().GetUserState(username, password);

                if (user == UserState.Active
                    || (this.TempData["code"] != null && code != null
                        && Int32.Parse(this.TempData["code"].ToString()) == code))
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    this.SetSid();
                    return this.RedirectToAction("Index", "User");
                }
            }

            this.TempData.Remove("code"); // Удаляем временный код 

            switch (user)
            {
                case UserState.Blocked:
                {
                    this.ViewData["Error"] = "Пользователь заблокирован.";
                    break;
                }
                case UserState.Deleted:
                {
                    this.ViewData["Error"] = "Пользователь удален из системы.";
                    break;
                }
                case UserState.DoubleAutorization:
                {
                    this.ViewData["Error"] = "Введите код авторизации из смс.";
                    var rand = new Random().Next(10000, 99999);
                    var res = true; // Stuff.SendSms(rand.ToString());
                    if (!res) this.ViewData["Error"] = "Ошибка отправки смс.";
                    else this.TempData["code"] = rand;
                    break;
                }
                case UserState.NotExists:
                {
                    if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password)) this.ViewData["Error"] = "Сочетание логин/пароль не найдено.";
                    break;
                }
                default:
                {
                    this.ViewData["Error"] = null;
                }
                break;
            }
            return this.View(user);
        }

        /// <summary>
        /// The logout.
        /// </summary>
        /// <returns>
        /// The <see cref="RedirectToRouteResult"/>.
        /// </returns>
        public RedirectToRouteResult Logout()
        {
            FormsAuthentication.SignOut();
            return this.RedirectToAction("Login", "Login");
        }

        private void SetSid()
        {
            var sid = Guid.NewGuid();

            this.DataContext.Get<ISecurityDataProvider>().RegisterSid(sid);

            this.HttpContext.Response.Cookies.Add(
                new HttpCookie("SID") { Expires = DateTime.Today.AddDays(365), HttpOnly = true, Value = sid.ToString() });
        }
    }
}
