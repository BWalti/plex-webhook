namespace Webhook.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class PlexController : ControllerBase
    {
        private readonly List<PlexWebHook> hooks = new List<PlexWebHook>();

        [HttpPost]
        public void Post(PlexWebHook hook)
        {
            this.hooks.Add(hook);
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlexWebHook>> Get()
        {
            return this.hooks;
        }

        public class PlexWebHook
        {
            public string Event { get; set; }

            public bool User { get; set; }

            public bool Owner { get; set; }

            public PlexAccount Account { get; set; }

            public PlexServer Server { get; set; }

            public PlexPlayer Player { get; set; }

            public PlexMetadata Metadata { get; set; }
        }

        public class PlexAccount
        {
            public int Id { get; set; }

            public string Thumb { get; set; }

            public string Title { get; set; }
        }

        public class PlexServer
        {
            public string Title { get; set; }

            public string Uuid { get; set; }
        }

        public class PlexPlayer
        {
            public bool Local { get; set; }

            public string PublicAddress { get; set; }

            public string Title { get; set; }

            public string Uuid { get; set; }
        }

        public class PlexMetadata
        {
            public string LibrarySectionType { get; set; }

            public string RatingKey { get; set; }

            public string Key { get; set; }

            public string ParentRatingKey { get; set; }

            public string GrandparentRatingKey { get; set; }

            public string Guid { get; set; }

            public int LibrarySectionID { get; set; }

            public string Type { get; set; }

            public string Title { get; set; }

            public string GrandparentKey { get; set; }

            public string ParentKey { get; set; }

            public string GrandparentTitle { get; set; }

            public string ParentTitle { get; set; }

            public string Summary { get; set; }

            public int Index { get; set; }

            public int ParentIndex { get; set; }

            public int RatingCount { get; set; }

            public string Thumb { get; set; }

            public string Art { get; set; }

            public string ParentThumb { get; set; }

            public string GrandparentThumb { get; set; }

            public string GrandparentArt { get; set; }

            public int AddedAt { get; set; }

            public int UpdatedAt { get; set; }
        }
    }
}