namespace Sototi.Web
{
	using System;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Converters;

	/// <summary>
	/// The my date time converter.
	/// </summary>
	public class CustomDateTimeConverter : DateTimeConverterBase
	{

		/// <summary>
		/// The write json.
		/// </summary>
		/// <param name="writer">
		/// The writer.
		/// </param>
		/// <param name="value">
		/// The value.
		/// </param>
		/// <param name="serializer">
		/// The serializer.
		/// </param>
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var res = ((DateTime)value).ToShortDateString() + " " + ((DateTime)value).ToLongTimeString();
			writer.WriteValue(res);
		}

		/// <summary>
		/// The read json.
		/// </summary>
		/// <param name="reader">
		/// The reader.
		/// </param>
		/// <param name="objectType">
		/// The object type.
		/// </param>
		/// <param name="existingValue">
		/// The existing value.
		/// </param>
		/// <param name="serializer">
		/// The serializer.
		/// </param>
		/// <returns>
		/// The <see cref="object"/>.
		/// </returns>
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{

			return DateTime.Parse(reader.Value.ToString());
		}
	}
}