namespace Webhook.Models.ElasticSearch
{
    using System;

    using Webhook.Models.PlexWebhook;

    public class SimplePlexDocument
    {
        public DateTime EventAt { get; set; }
        
        public string PlayerId { get; set; }

        public string PlayerTitle { get; set; }

        public bool PlayerIsLocal { get; set; }

        public string PlayerPublicAddress { get; set; }

        public string ServerTitle { get; set; }

        public string ServerId { get; set; }

        public string Title { get; set; }

        public DateTime AddedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string FullUri { get; set; }

        public PlexEvent Event { get; set; }

        public bool IsOwner { get; set; }

    }
}