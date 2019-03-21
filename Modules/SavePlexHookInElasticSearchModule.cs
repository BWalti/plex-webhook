namespace Webhook.Modules
{
    using System;
    using System.Threading.Tasks;

    using Nest;

    using Serilog;

    using Webhook.Eventing;
    using Webhook.Messages;
    using Webhook.Models.ElasticSearch;

    public class SavePlexHookInElasticSearchModule : IHandle<PlexWebHookReceived>
    {
        private readonly ElasticClient elasticClient;

        public SavePlexHookInElasticSearchModule(ElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        public async Task HandleAsync(PlexWebHookReceived message)
        {
            // 1. map web hook content to elastic search model
            // 2. save es model to elastic search

            //var doc = JsonConvert.SerializeObject(hook);
            //var response = this.elasticClient.Index(doc, idx => idx.Index("plex"));

            var mapped = new SimplePlexDocument
                {
                    EventAt = DateTime.UtcNow,

                    PlayerId = message.Content.Player.Id,
                    PlayerTitle = message.Content.Player.Title,
                    PlayerIsLocal = message.Content.Player.IsLocal,
                    PlayerPublicAddress = message.Content.Player.PublicAddress.ToString(),
                    
                    ServerTitle = message.Content.Server.Title,
                    ServerId = message.Content.Server.Id,
                    
                    Title = message.Content.Metadata.Title,
                    AddedAt = message.Content.Metadata.AddedAt,
                    UpdatedAt = message.Content.Metadata.UpdatedAt,
                    FullUri = message.Content.Metadata.FullUri,
                    
                    Event = message.Content.Event,
                    IsOwner = message.Content.IsOwner
                };

            var result = await this.elasticClient.IndexAsync(mapped, idx => idx.Index("simpleplexdoc"));

            Log.Information($"Submitting infos to elastic search: {result.IsValid}");
        }
    }
}