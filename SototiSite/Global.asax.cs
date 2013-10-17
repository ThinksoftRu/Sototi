using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace SototiSite
{
    using System.Globalization;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        /// <summary>
        /// Application BeginRequest.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var langCookies = Request.Cookies["lang"];
            var lang = langCookies == null ? "ru" : langCookies.Value;

            if (lang.Length != 2) lang = "ru";

            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);


            if (langCookies == null)
            {
                var newCookies = new HttpCookie("lang", "ru") { Expires = DateTime.UtcNow.AddDays(365) };
                Response.AppendCookie(newCookies);
            }

            if (Request.IsAuthenticated || Request.RawUrl.ToLower().Contains("login/login"))
                return;

            //Response.Redirect("~/Login/Login");
        }

        /// <summary>
        /// The application_ error.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Application_Error(Object sender, EventArgs e)
        {
            var exc = Server.GetLastError();
            if (exc == null) return;

            Response.Clear();
            Server.ClearError();

            if (Request.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                var mess =
                    exc.Message.Replace('"', ' ')
                       .Replace('\\', ' ')
                       .Replace(':', ' ')
                       .Replace('.', ' ')
                       .Replace("\n", "");
                //Response.Write("{\"ResponseSuccess\":false,\"ResponseTitle\":\"Ошибка\",\"ResponseMessage\":\"Во время обработки запроса произошла ошибка\"}");
                Response.Write(
                    "{\"ResponseSuccess\":false,\"ResponseTitle\":\"Ошибка\",\"ResponseMessage\":\" " + mess + "  \"}");
                Response.End();
            }
            else
            {
                //var routeData = new RouteData();
                //routeData.Values["controller"] = "User";
                //routeData.Values["action"] = "Error";
                //Response.StatusCode = 500;
                //IController controller = new UserController();
                //var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
                //controller.Execute(rc);
            }
        }
    }
}