namespace SototiSite.Code
{
    using System;
    using System.Web.Mvc;

    using Newtonsoft.Json;

    using Sototi.Web;

    /// <summary>
	/// The json net result.
	/// </summary>
	public class JsonNetResult : JsonResult
	{
		/// <summary>
		/// Массив собственных конвертеров.
		/// </summary>
		private static readonly JsonConverter[] DtConverter = new JsonConverter[] { new CustomDateTimeConverter() };

		/// <summary>
		/// The execute result.
		/// </summary>
		/// <param name="context">
		/// The context.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Неизвестное исключение.
		/// </exception>
		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null) throw new ArgumentNullException("context");

			var response = context.HttpContext.Response;

			response.ContentType = !String.IsNullOrEmpty(this.ContentType) ? this.ContentType : "application/json";

			if (this.ContentEncoding != null) response.ContentEncoding = this.ContentEncoding;

			if (this.Data == null) return;

			// If you need special handling, you can call another form of SerializeObject below
			var serializedObject = JsonConvert.SerializeObject(this.Data, DtConverter);

			response.Write(serializedObject);
		}
	}
}