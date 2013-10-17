#region Usings

using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using SototiCore.Annotations;

#endregion

namespace SototiCore.Exceptions
{
	/// <summary>
	/// Исключение при вызове API.
	/// </summary>
	[Serializable]
	public class ApiException : SototiException
	{
		/// <summary>
		/// Создает новый экземпляр класса <see cref="ApiException"/>.
		/// </summary>
		/// <param name="innerException">Вложенное исключение.</param>
		/// <param name="httpStatusCode">HTTP код возврата.</param>
		/// <param name="errorNumber">Код ошибки (в соотвествии с классификатором).</param>
		/// <param name="message">Описание ошибки.</param>
		public ApiException(Exception innerException, int httpStatusCode, string errorNumber, string message)
			: base(innerException, errorNumber, message)
		{
			HttpStatusCode = httpStatusCode;
		}

		/// <summary>
		/// Создает новый экземпляр класса <see cref="ApiException"/>.
		/// </summary>
		/// <param name="exception">Вложенное исключение.</param>
		/// <param name="httpStatusCode">HTTP код возврата.</param>
		public ApiException(SototiException exception, int httpStatusCode)
			: this(exception, httpStatusCode, exception.ErrorNumber, exception.ErrorText)
		{
		}

		/// <summary>
		/// Создает новый экземпляр класса <see cref="SototiException"/> с указанием кода, текста ошибки и вложенного исключения.
		/// </summary>
		/// <param name="innerException">Вложенное исключение.</param>
		/// <param name="httpStatusCode">HTTP код возврата.</param>
		/// <param name="errorNumber">Код ошибки.</param>
		/// <param name="format">Строка форматирования описания ошибки.</param>
		/// <param name="args">Аргументы к строке форматирования.</param>
		[StringFormatMethod("format")]
		public ApiException(Exception innerException, int httpStatusCode, string errorNumber, string format, params object[] args)
			: this(innerException, httpStatusCode, errorNumber, String.Format(CultureInfo.InvariantCulture, format, args))
		{
		}

		/// <summary>
		/// Создает новый экземпляр класса <see cref="SototiException"/> с указанием кода, текста ошибки и вложенного исключения.
		/// </summary>
		/// <param name="httpStatusCode">HTTP код возврата.</param>
		/// <param name="errorNumber">Код ошибки.</param>
		/// <param name="format">Строка форматирования описания ошибки.</param>
		/// <param name="args">Аргументы к строке форматирования.</param>
		[StringFormatMethod("format")]
		public ApiException(int httpStatusCode, string errorNumber, string format, params object[] args)
			: this(null, httpStatusCode, errorNumber, String.Format(CultureInfo.InvariantCulture, format, args))
		{
		}

		/// <summary>
		/// Создает новый экземпляр класса <see cref="ApiException"/>.
		/// </summary>
		/// <param name="httpStatusCode">HTTP код возврата.</param>
		/// <param name="errorNumber">Код ошибки (в соотвествии с классификатором).</param>
		/// <param name="message">Описание ошибки.</param>
		public ApiException(int httpStatusCode, string errorNumber, string message)
			: this(null, httpStatusCode, errorNumber, message)
		{
		}

		/// <summary>
		/// Создает новый экземпляр класса <see cref="ApiException"/>.
		/// </summary>
		/// <param name="errorNumber">Код ошибки (в соотвествии с классификатором).</param>
		/// <param name="message">Описание ошибки.</param>
		public ApiException(string errorNumber, string message)
			: this(400, errorNumber, message)
		{
		}

		/// <summary>
		/// HTTP код возврата.
		/// </summary>
		public int HttpStatusCode { get; private set; }

		/// <summary>
		/// Сериализует дополнительные поля класса.
		/// </summary>
		/// <param name="info">Информация о сериализации.</param>
		/// <param name="context">Контектс потока.</param>
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("HttpStatusCode", HttpStatusCode);
		}
	}
}