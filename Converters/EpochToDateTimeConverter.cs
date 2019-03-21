namespace Webhook.Converters
{
    using System;

    using Newtonsoft.Json;

    public class EpochToDateTimeConverter : JsonConverter<DateTime>
    {
        private static readonly DateTime EpochDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            var date = value;
            var diff = date - EpochDate;

            var secondsSinceEpoch = (int)diff.TotalSeconds;
            serializer.Serialize(writer, secondsSinceEpoch);
        }

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var timestamp = Convert.ToInt32(reader.Value);
            return EpochDate.AddSeconds(timestamp);
        }
    }
}