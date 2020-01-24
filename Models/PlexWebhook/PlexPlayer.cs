namespace Webhook.Models.PlexWebhook
{
    using System.Net;

    using Newtonsoft.Json;

    using Webhook.Converters;

    public class PlexPlayer
    {
        [JsonProperty("local")]
        public bool IsLocal { get; set; }

        [JsonProperty("publicAddress")]
        public string PublicAddress { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("uuid")]
        public string Id { get; set; }
    }
}