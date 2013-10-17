namespace SototiSite.Controllers
{
    using System.Web.Mvc;

    using SototiCore.Data;

    using SototiSite.Code;

    /// <summary>
    /// The base controller.
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Создает новый экземпляр класса.
        /// </summary>
        /// <param name="log">Протоколирование событий.</param>
        /// <param name="dataContext">Контекст данных.</param>
        protected BaseController(ILogger log, IDataContext dataContext, ILocalizer localizer)
        {
            this.Log = log;
            this.DataContext = dataContext;
            this.Localizer = localizer;
        }

        /// <summary>
        /// Протоколирование событий.
        /// </summary>
        protected ILogger Log { get; private set; }

        /// <summary>
        /// Контекст данных.
        /// </summary>
        protected IDataContext DataContext { get; private set; }


        protected ILocalizer Localizer { get; private set; }

        /// <summary>
        /// переопределенный метод JSON ответа.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <param name="contentType">
        /// The content type.
        /// </param>
        /// <param name="contentEncoding">
        /// The content encoding.
        /// </param>
        /// <param name="behavior">
        /// The behavior.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        protected override JsonResult Json(
            object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult { Data = data, ContentEncoding = contentEncoding, JsonRequestBehavior = behavior };
        }

        /// <summary>
        /// Обощенный ответ клиенту когда не требуется возсращать данные, а просто его уведовить. Перехватывается глобальным обработчиком AXAX не клиенте (см. init.js).
        /// </summary>
        /// <param name="success">
        /// The success.
        /// </param>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// Обобщенный ответ сервера.
        /// </returns>
        protected dynamic GetGenericAnswear(bool success, string title, string message)
        {
            return new { ResponseSuccess = success, ResponseTitle = title, ResponseMessage = message };
        }
    }
}