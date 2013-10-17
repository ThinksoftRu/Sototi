#region Usings

using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;


#endregion

namespace SototiCore.Exceptions
{
	using SototiCore.Annotations;

	/// <summary>
	/// Базовый класс для исключений в проекте.
	/// </summary>
	[Serializable]
	public class SototiException : Exception
	{
		#region Конструкторы

		/// <summary>
		/// Создает новый экземпляр класса <see cref="SototiException"/> с указанием кода, текста ошибки и вложенного исключения.
		/// </summary>
		/// <param name="innerException">Вложенное исключение.</param>
		/// <param name="errorNumber">Код ошибки.</param>
		/// <param name="message">Текстовое описание ошибки.</param>
		public SototiException(Exception innerException, string errorNumber, string message) : base(message, innerException)
		{
			ErrorNumber = errorNumber;
		}

		/// <summary>
		/// Создает новый экземпляр класса <see cref="SototiException"/>.
		/// </summary>
		public SototiException() : this((Exception)null, String.Empty, null)
		{
		}

		/// <summary>
		/// Создает новый экземпляр класса <see cref="SototiException"/>.
		/// </summary>
		/// <param name="errorNumber">Код ошибки.</param>
		public SototiException(string errorNumber) : this((Exception)null, errorNumber, null)
		{
		}

		/// <summary>
		/// Создает новый экземпляр класса <see cref="SototiException"/> с указанием кода и текста ошибки.
		/// </summary>
		/// <param name="errorNumber">Номер ошибки.</param>
		/// <param name="message">Текстовое описание ошибки.</param>
		public SototiException(string errorNumber, string message) : this(null, errorNumber, message)
		{
		}

		/// <summary>
		/// Создает новый экземпляр класса <see cref="SototiException"/> с указанием кода, текста ошибки и вложенного исключения.
		/// </summary>
		/// <param name="innerException">Вложенное исключение.</param>
		/// <param name="errorNumber">Номер ошибки.</param>
		/// <param name="format">Строка форматирования описания ошибки.</param>
		/// <param name="args">Аргументы к строке форматирования.</param>
		[StringFormatMethod("format")]
		public SototiException(Exception innerException, string errorNumber, string format, params object[] args)
			: this(innerException, errorNumber, String.Format(CultureInfo.InvariantCulture, format, args))
		{
		}

		/// <summary>
		/// Создает новый экземпляр класса <see cref="SototiException"/> с указанием кода, текста ошибки и вложенного исключения.
		/// </summary>
		/// <param name="errorNumber">Номер ошибки.</param>
		/// <param name="format">Строка форматирования описания ошибки.</param>
		/// <param name="args">Аргументы к строке форматирования.</param>
		[StringFormatMethod("format")]
		public SototiException(string errorNumber, string format, params object[] args)
			: this(errorNumber, String.Format(CultureInfo.InvariantCulture, format, args))
		{
		}

		#endregion

		/// <summary>
		/// Номер ошибки (текстовый).
		/// </summary>
		public string ErrorNumber { get; private set; }

		/// <summary>
		/// Текст ошибки.
		/// </summary>
		public string ErrorText
		{
			get { return base.Message; }
		}

		/// <summary>
		/// Текст исключения.
		/// </summary>
		public override string Message
		{
			get { return String.Format("{0}: {1}", ErrorNumber, base.Message); }
		}

		/// <summary>
		/// Сериализует дополнительные поля класса.
		/// </summary>
		/// <param name="info">Информация о сериализации.</param>
		/// <param name="context">Контектс потока.</param>
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ErrorNumber", ErrorNumber);
		}
	}
}