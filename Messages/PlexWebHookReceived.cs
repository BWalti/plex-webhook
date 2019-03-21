namespace Webhook.Messages
{
    using Webhook.Models.PlexWebhook;

    public class PlexWebHookReceived
    {
        public PlexWebHookReceived(PlexWebHook content)
        {
            this.Content = content;
        }

        public PlexWebHook Content { get; }
    }
}