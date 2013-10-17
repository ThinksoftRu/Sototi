#region Usings

using System;
using System.Text.RegularExpressions;

using BLToolkit.Data;

#endregion

namespace SototiData
{
	/// <summary>
	/// Расширения для работы с моделью данных.
	/// </summary>
	internal static class Extensions
	{
		/// <summary>
		/// Парсит исключение и извлекает из него читаемый текст.
		/// </summary>
		/// <param name="e">Обрабатывамое исключение.</param>
		/// <returns>Текст ошибки.</returns>
		public static string ParseMessage(this DataException e)
		{
			var match = Regex.Match(e.Message, @"^ORA-\d{5}: (.*)$", RegexOptions.Multiline);
			return match.Success ? match.Groups[1].Value : e.Message;
		}

		/// <summary>
		/// Парсит исключение и извлекает из него читаемый текст.
		/// </summary>
		/// <param name="e">Обрабатывамое исключение.</param>
		/// <returns>Текст ошибки.</returns>
		public static string ParseMessage(this Exception e)
		{
			return e.Message;
		}
	}
}