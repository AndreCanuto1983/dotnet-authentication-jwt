namespace Spotify.Models
{
    public class Playlist
    {
        public bool collaborative { get; set; }
        public string description { get; set; }
        public ExternalUrls external_urls { get; set; }
        public Followers followers { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public Image[] images { get; set; }
        public string name { get; set; }
        public SpotfyUser owner { get; set; }
        public bool? @public { get; set; }
        public Playlisttrack tracks { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }
}
