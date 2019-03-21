namespace Webhook.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Webhook.Eventing;
    using Webhook.Messages;

    //using Nest;

    using Webhook.Models.PlexWebhook;

    [Route("api/[controller]")]
    [ApiController]
    public class PlexController : ControllerBase
    {
        private readonly IEventAggregator eventAggregator;

        private static readonly List<PlexWebHook> Hooks;

        static PlexController()
        {
            Hooks = new List<PlexWebHook>();
        }

        public PlexController(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        [HttpPost]
        public async Task<ActionResult<PlexWebHook>> Post(PlexWebHook hook)
        {
            Hooks.Add(hook);

            await this.eventAggregator.PublishAsync(new PlexWebHookReceived(hook));

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