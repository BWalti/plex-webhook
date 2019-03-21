namespace Webhook.Models.PlexWebhook
{
    using System;

    using Newtonsoft.Json;

    public class PlexAccount
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("thumb")]
        public Uri Thumb { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}