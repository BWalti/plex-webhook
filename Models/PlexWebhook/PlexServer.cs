namespace Webhook.Models.PlexWebhook
{
    using Newtonsoft.Json;

    public class PlexServer
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("uuid")]
        public string Id { get; set; }
    }
}