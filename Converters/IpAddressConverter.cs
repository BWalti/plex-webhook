namespace Webhook.Converters
{
    using System;
    using System.Net;

    using Newtonsoft.Json;

    public class IpAddressConverter : JsonConverter<IPAddress>
    {
        public override void WriteJson(JsonWriter writer, IPAddress value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToString());
        }

        public override IPAddress ReadJson(JsonReader reader, Type objectType, IPAddress existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var stringValue = (string)reader.Value;
            var ipAddress = IPAddress.Parse(stringValue);

            return ipAddress;
        }
    }
}