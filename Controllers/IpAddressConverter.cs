namespace Webhook.Controllers
{
    using System;
    using System.Net;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class IpAddressConverter : JsonConverter<IPAddress>
    {
        public override void WriteJson(JsonWriter writer, IPAddress value, JsonSerializer serializer)
        {
            var token = JToken.FromObject(value.ToString());
            token.WriteTo(writer);
        }

        public override IPAddress ReadJson(JsonReader reader, Type objectType, IPAddress existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var stringValue = (string)reader.Value;
            var ipAddress = IPAddress.Parse(stringValue);

            return ipAddress;
        }
    }
}