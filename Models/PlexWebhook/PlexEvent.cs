namespace Webhook.Models.PlexWebhook
{
    using System.Runtime.Serialization;

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