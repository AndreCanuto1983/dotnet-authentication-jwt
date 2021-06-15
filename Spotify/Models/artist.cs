using Newtonsoft.Json;

namespace Spotify.Models
{
    public class Artist
    {
        public ExternalUrls external_urls { get; set; }
        public string[] genres { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public Image[] images { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }
}
