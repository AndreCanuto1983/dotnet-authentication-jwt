using Newtonsoft.Json;
using System;

namespace Spotify.Models
{
    public class Playlisttrack
    {
        public string added_at { get; set; }
        public SpotfyUser added_by { get; set; }
        public Track track { get; set; }
    }
}
