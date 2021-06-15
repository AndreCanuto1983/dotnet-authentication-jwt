using System;

namespace Spotify.Models
{
    public class Items
    {
        public DateTime added_at { get; set; }
        public Added_by added_by { get; set; }
        public bool is_local { get; set; }
        public string primary_color { get; set; }
        public Track track { get; set; }
        public VideoThumbnail video_thumbnail { get; set; }
    }
}
