namespace Webhook.Models
{
    public class PlexConfig
    {
        public string AuthToken { get; set; }
    }

    public class ElasticSearchConfig
    {
        public string Uri { get; set; }

        public string Index { get; set; }
    }
}