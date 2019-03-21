namespace Webhook.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    [Route("api/[controller]")]
    [ApiController]
    public class PlexController : ControllerBase
    {
        private static readonly List<PlexWebHook> Hooks;

        static PlexController()
        {
            Hooks = new List<PlexWebHook>();
        }

        [HttpPost]
        public ActionResult<PlexWebHook> Post(PlexWebHook hook)
        {
            Hooks.Add(hook);
            
            return this.CreatedAtAction(nameof(this.Get), new { id = Hooks.Count - 1 }, hook);
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlexWebHook>> Get()
        {
            return Hooks;
        }

        [HttpGet("{id}")]
        public ActionResult<PlexWebHook> Get(int id)
        {
            return Hooks[id];
        }

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

        public class PlexAccount
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("thumb")]
            public Uri Thumb { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }
        }

        public class PlexServer
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("uuid")]
            public string Id { get; set; }
        }

        public class PlexPlayer
        {
            [JsonProperty("local")]
            public bool IsLocal { get; set; }

            [JsonProperty("publicAddress")]
            [JsonConverter(typeof(IpAddressConverter))]
            public IPAddress PublicAddress { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("uuid")]
            public string Id { get; set; }
        }

        public class PlexMetadata
        {
            [JsonProperty("librarySectionType")]
            public string LibrarySectionType { get; set; }

            [JsonProperty("ratingKey")]
            public string RatingKey { get; set; }

            [JsonProperty("key")]
            public string Key { get; set; }

            [JsonProperty("parentRatingKey")]
            public string ParentRatingKey { get; set; }

            [JsonProperty("grandparentRatingKey")]
            public string GrandparentRatingKey { get; set; }

            [JsonProperty("guid")]
            public string FullUri { get; set; }

            [JsonProperty("librarySectionID")]
            public int LibrarySectionId { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("grandparentKey")]
            public string GrandparentKey { get; set; }

            [JsonProperty("parentKey")]
            public string ParentKey { get; set; }

            [JsonProperty("grandparentTitle")]
            public string GrandparentTitle { get; set; }

            [JsonProperty("parentTitle")]
            public string ParentTitle { get; set; }

            [JsonProperty("summary")]
            public string Summary { get; set; }

            [JsonProperty("index")]
            public int Index { get; set; }

            [JsonProperty("parentIndex")]
            public int ParentIndex { get; set; }

            [JsonProperty("ratingCount")]
            public int RatingCount { get; set; }

            [JsonProperty("thumb")]
            public Uri Thumb { get; set; }

            [JsonProperty("art")]
            public Uri Art { get; set; }

            [JsonProperty("parentThumb")]
            public Uri ParentThumb { get; set; }

            [JsonProperty("grandparentThumb")]
            public Uri GrandparentThumb { get; set; }

            [JsonProperty("grandparentArt")]
            public Uri GrandparentArt { get; set; }

            [JsonProperty("addedAt")]
            [JsonConverter(typeof(EpochToDateTimeConverter))]
            public DateTime AddedAt { get; set; }

            [JsonProperty("updatedAt")]
            [JsonConverter(typeof(EpochToDateTimeConverter))]
            public DateTime UpdatedAt { get; set; }
        }

        public enum PlexEvent
        {
            [EnumMember(Value = "media.play")]
            Play,

            [EnumMember(Value = "media.pause")]
            Pause,

            [EnumMember(Value = "media.resume")]
            Resume,

            [EnumMember(Value = "media.stop")]
            Stop,

            [EnumMember(Value = "media.scrobble")]
            Scrobble,

            [EnumMember(Value = "media.rate")]
            Rate
        }
    }
}