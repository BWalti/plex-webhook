namespace Webhook.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    [Route("api/[controller]")]
    [ApiController]
    public class PlexController : ControllerBase
    {
        private static readonly List<PlexWebHook> Hooks;

        static PlexController()
        {
            Hooks = new List<PlexWebHook>
                        {
                            //new PlexWebHook
                            //    {
                            //        Account = new PlexAccount { Id = 1, Thumb = "thumb", Title = "title" },
                            //        Event = "event",
                            //        Metadata = new PlexMetadata
                            //                       {
                            //                           Thumb = "thumb",
                            //                           Title = "title",
                            //                           AddedAt = 2,
                            //                           Art = "art",
                            //                           GrandparentArt = "grandparentArt",
                            //                           GrandparentKey = "gpKey",
                            //                           GrandparentRatingKey = "gpRK",
                            //                           GrandparentThumb = "gpT",
                            //                           GrandparentTitle = "gpT",
                            //                           Guid = "guid",
                            //                           Index = 3,
                            //                           Key = "key",
                            //                           LibrarySectionId = 4,
                            //                           LibrarySectionType = "lsT",
                            //                           ParentIndex = 5,
                            //                           ParentKey = "pK",
                            //                           ParentRatingKey = "pRK",
                            //                           ParentThumb = "pT",
                            //                           ParentTitle = "pT",
                            //                           RatingCount = 6,
                            //                           RatingKey = "rK",
                            //                           Summary = "summary",
                            //                           Type = "type",
                            //                           UpdatedAt = 7
                            //                       },
                            //        IsOwner = true,
                            //        Player = new PlexPlayer
                            //                     {
                            //                         Title = "title",
                            //                         IsLocal = true,
                            //                         PublicAddress = "200.200.200.200",
                            //                         Uuid = "uuid"
                            //                     },
                            //        Server = new PlexServer { Title = "title", Uuid = "uuid" },
                            //        IsUser = false
                            //    }
                        };
        }

        [HttpPost]
        public ActionResult<PlexWebHook> Post(PlexWebHook hook)
        {
            Hooks.Add(hook);

            /*
             {"event":"media.pause","user":true,"owner":true,
             "Account":
                {"id":1,"thumb":"https://plex.tv/users/da827a7781308c1d/avatar?c=1551813116","title":"bibolorean"},
              "Server":
                {"title":"Bibo-k8s","uuid":"0b651269e2a64016eb8f691ea457ebdaaa001531"},
              "Player":
                {"local":true,"publicAddress":"212.51.137.58","title":"Firefox","uuid":"xs93pr5ze0hxxo5u6npf7fkk"},
              "Metadata":
                {"librarySectionType":"show","ratingKey":"10881","key":"/library/metadata/10881","parentRatingKey":"10877","grandparentRatingKey":"10876","guid":"com.plexapp.agents.thetvdb://350665/1/4?lang=de","librarySectionTitle":"TV Shows","librarySectionID":2,"librarySectionKey":"/library/sections/2","type":"episode","title":"The Switch","titleSort":"Switch","grandparentKey":"/library/metadata/10876","parentKey":"/library/metadata/10877","grandparentTitle":"The Rookie","parentTitle":"Season 1","contentRating":"TV-14","summary":"The rookies are temporarily paired with new training officers, and Officer Nolan is paired with Officer Lopez. When Nolan and Lopez track down an escaped criminal, they discover a little kindness goes a long way. Meanwhile, Jackson is forced to face his fears when he is partnered with Officer Bradford, while Officer Chen and Nolan must face a hard truth.","index":4,"parentIndex":1,"rating":10.0,"viewOffset":787000,"lastViewedAt":1552465047,"year":2018,"thumb":"/library/metadata/10881/thumb/1551843117","art":"/library/metadata/10876/art/1551843117","parentThumb":"/library/metadata/10877/thumb/1551843117","grandparentThumb":"/library/metadata/10876/thumb/1551843117","grandparentArt":"/library/metadata/10876/art/1551843117","originallyAvailableAt":"2018-11-13","addedAt":1542173951,"updatedAt":1551843117,"Director":[{"id":24051,"filter":"director=24051","tag":"Toa Fraser"}],"Writer":[{"id":21974,"filter":"writer=21974","tag":"Vincent Angell"}]}
             
             */

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
            public string Event { get; set; }

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
            public string Thumb { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }
        }

        public class PlexServer
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("uuid")]
            public string Uuid { get; set; }
        }

        public class PlexPlayer
        {
            [JsonProperty("local")]
            public bool IsLocal { get; set; }

            [JsonProperty("publicAddress")]
            public string PublicAddress { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("uuid")]
            public string Uuid { get; set; }
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
            public string Guid { get; set; }

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
            public string Thumb { get; set; }

            [JsonProperty("art")]
            public string Art { get; set; }

            [JsonProperty("parentThumb")]
            public string ParentThumb { get; set; }

            [JsonProperty("grandparentThumb")]
            public string GrandparentThumb { get; set; }

            [JsonProperty("grandparentArt")]
            public string GrandparentArt { get; set; }

            [JsonProperty("addedAt")]
            public int AddedAt { get; set; }

            [JsonProperty("updatedAt")]
            public int UpdatedAt { get; set; }
        }
    }
}