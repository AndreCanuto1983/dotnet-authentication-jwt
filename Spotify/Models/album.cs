using Newtonsoft.Json;

namespace Spotify.Models
{
    public class Album
    {
        public string album_type { get; set; }
        
        public Artist[] artists { get; set; }
        
        public string[] available_markets { get; set; }

        public ExternalUrls external_urls { get; set; }

        public ExternalIds external_ids { get; set; }        
        
        public string[] genres { get; set; }
        
        public string href { get; set; }
        
        public string id { get; set; }
        
        public Image[] images { get; set; }
        
        public string name { get; set; }
        
        public int popularity { get; set; }
        
        public string release_date { get; set; }

        public int total_tracks { get; set; }
        
        public string release_date_precision { get; set; }
        
        public Track tracks { get; set; }
        
        public string type { get; set; }
        
        public string uri { get; set; }
    }
}
