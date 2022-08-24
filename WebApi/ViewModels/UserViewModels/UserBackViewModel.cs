using Spotify.Models;
using System.Collections.Generic;

namespace WebAPI.ViewModels.UserViewModels
{
    public class UserBackViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string url;
        public virtual ICollection<Track> tracks { get; set; }
    }
}
