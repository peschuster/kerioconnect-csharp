using System;
using System.Globalization;
using Newtonsoft.Json;

namespace KerioConnect
{
    /// <summary>
    /// JSON converter for kerio "UtcDateTime" strings (following RFC 2445).
    /// </summary>
    public class JsonDateTimeConverterRfc2445 : Newtonsoft.Json.Converters.DateTimeConverterBase
    {
        private const string DefaultDateTimeFormat = "yyyyMMdd'T'HHmmss'Z'";


        private const string DefaultDateFormat = "yyyyMMdd";

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter"/> to write to.</param><param name="value">The value.</param><param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string text;

            if (value is DateTime)
            {
                var dateTime = (DateTime)value;

                text = dateTime.ToString(DefaultDateTimeFormat, CultureInfo.InvariantCulture);
            }
            else if (value is DateTimeOffset)
            {
                var dateTimeOffset = (DateTimeOffset)value;

                text = dateTimeOffset.ToString(DefaultDateTimeFormat, CultureInfo.InvariantCulture);
            }
            else
            {
                throw new JsonSerializationException();
            }

            writer.WriteValue(text);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader"/> to read from.</param><param name="objectType">Type of the object.</param><param name="existingValue">The existing value of object being read.</param><param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            bool nullable = objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>);

            Type t = (nullable)
                ? Nullable.GetUnderlyingType(objectType)
                : objectType;

            if (reader.TokenType == JsonToken.Null)
            {
                if (!nullable)
                    throw new JsonSerializationException(string.Format(CultureInfo.InvariantCulture,"Cannot convert null value to {0}.", objectType));

                return null;
            }

            if (reader.TokenType == JsonToken.Date)
            {
                if (t == typeof(DateTimeOffset))
                    return reader.Value is DateTimeOffset ? reader.Value : new DateTimeOffset((DateTime)reader.Value);

                return reader.Value;
            }

            if (reader.TokenType != JsonToken.String)
                throw new JsonSerializationException(string.Format(CultureInfo.InvariantCulture, "Unexpected token parsing date. Expected String, got {0}.", reader.TokenType));

            string dateText = reader.Value.ToString();

            if (string.IsNullOrEmpty(dateText) && nullable)
                return null;

            if (t == typeof(DateTimeOffset))
            {
                return DateTimeOffset.ParseExact(dateText, DefaultDateTimeFormat, CultureInfo.InvariantCulture);
            }

            DateTime result;
            if (DateTime.TryParseExact(dateText, DefaultDateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out result))
            {
                return result;
            }

            if (DateTime.TryParseExact(dateText, DefaultDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out result))
            {
                return result.Date;
            }

            throw new JsonSerializationException(string.Format(CultureInfo.InvariantCulture, "Unexpected date format: {0}.", dateText));
        }
    }
}
