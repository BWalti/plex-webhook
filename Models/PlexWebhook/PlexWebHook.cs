namespace Webhook.Models.PlexWebhook
{
    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;

    using Webhook.ModelBinder;

    [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "payload")]
    public class PlexWebHook
    {
        [JsonProperty("event")]
        public PlexEvent Event { get; set; }

        [JsonProperty("user")]
        public bool IsUser { get; set; }

        [JsonProperty("owner")]
        public bool IsOwner { get; set; }

        [JsonProperty("Account")]
        public PlexAccount Account { get; set; }

        [JsonProperty("Server")]
        public PlexServer Server { get; set; }

        [JsonProperty("Player")]
        public PlexPlayer Player { get; set; }

        [JsonProperty("Metadata")]
        public PlexMetadata Metadata { get; set; }
    }
}