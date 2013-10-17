#region Usings

using System;
using System.Globalization;


#endregion

namespace SototiCore.Exceptions
{
	using SototiCore.Annotations;

	/// <summary>
	/// Базовый класс для исключений в проекте.
	/// </summary>
	[Serializable]
	public class InternalException : Exception
	{
		#region Конструкторы

		/// <summary>
		/// Создает новый экземпляр класса <see cref="InternalException"/> с указанием кода, текста ошибки и вложенного исключения.
		/// </summary>
		/// <param name="innerException">Вложенное исключение.</param>
		/// <param name="message">Текстовое описание ошибки.</param>
		public InternalException(Exception innerException, string message)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Создает новый экземпляр класса <see cref="InternalException"/>.
		/// </summary>
		public InternalException()
			: this((Exception)null, null)
		{
		}

		/// <summary>
		/// Создает новый экземпляр класса <see cref="InternalException"/> с указанием кода и текста ошибки.
		/// </summary>
		/// <param name="message">Текстовое описание ошибки.</param>
		public InternalException(string message)
			: this(null, message)
		{
		}

		/// <summary>
		/// Создает новый экземпляр класса <see cref="InternalException"/> с указанием кода, текста ошибки и вложенного исключения.
		/// </summary>
		/// <param name="innerException">Вложенное исключение.</param>
		/// <param name="format">Строка форматирования описания ошибки.</param>
		/// <param name="args">Аргументы к строке форматирования.</param>
		[StringFormatMethod("format")]
		public InternalException(Exception innerException, string format, params object[] args)
			: this(innerException, String.Format(CultureInfo.InvariantCulture, format, args))
		{
		}

		/// <summary>
		/// Создает новый экземпляр класса <see cref="InternalException"/> с указанием кода, текста ошибки и вложенного исключения.
		/// </summary>
		/// <param name="format">Строка форматирования описания ошибки.</param>
		/// <param name="args">Аргументы к строке форматирования.</param>
		[StringFormatMethod("format")]
		public InternalException(string format, params object[] args)
			: this(String.Format(CultureInfo.InvariantCulture, format, args))
		{
		}

		#endregion
	}
}