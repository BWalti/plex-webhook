namespace Webhook.Models.PlexWebhook
{
    using System;

    using Newtonsoft.Json;

    using Webhook.Converters;

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
}