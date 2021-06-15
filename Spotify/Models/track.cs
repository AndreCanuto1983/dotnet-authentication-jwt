using Newtonsoft.Json;

namespace Spotify.Models
{
    public class Track
    {
        public Album album { get; set; }

        public Artist[] artists { get; set; }

        public int disc_number { get; set; }

        public int duration_ms { get; set; }

        public bool @explicit { get; set; }

        public ExternalIds external_ids { get; set; }

        public ExternalUrls external_urls { get; set; }

        public string href { get; set; }

        public string id { get; set; }

        public string name { get; set; }

        public int popularity { get; set; }

        public string preview_url { get; set; }

        public int track_number { get; set; }

        public string type { get; set; }

        public string uri { get; set; }
    }
}
