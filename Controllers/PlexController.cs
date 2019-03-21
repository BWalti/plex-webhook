namespace Webhook.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    //using Nest;

    using Webhook.Models.PlexWebhook;

    [Route("api/[controller]")]
    [ApiController]
    public class PlexController : ControllerBase
    {
        private static readonly List<PlexWebHook> Hooks;

        //private readonly ElasticClient elasticClient;

        static PlexController()
        {
            Hooks = new List<PlexWebHook>();
        }

        //public PlexController(ElasticClient elasticClient)
        //{
        //    this.elasticClient = elasticClient;
        //}

        [HttpPost]
        public ActionResult<PlexWebHook> Post(PlexWebHook hook)
        {
            Hooks.Add(hook);

            //var doc = JsonConvert.SerializeObject(hook);
            //var response = this.elasticClient.Index(doc, idx => idx.Index("plex"));

            return this.CreatedAtAction(nameof(this.Get), new { id = Hooks.Count - 1 }, hook);
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlexWebHook>> Get()
        {
            return this.Ok(Hooks);
        }

        [HttpGet("{id}")]
        public ActionResult<PlexWebHook> Get(int id)
        {
            return this.Ok(Hooks[id]);
        }
    }
}