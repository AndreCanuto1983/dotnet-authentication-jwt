using Spotify.Enums;
using Spotify.Interfaces;
using Spotify.Models;
using System.Linq;

namespace Spotify.Services
{
    public class GetIdPlaylistSpotfy : IGetIdPlaylist<string, double>
    {
        private readonly IMountPlayListSpotify<Playlists> _playlist;

        public GetIdPlaylistSpotfy()
        {
            _playlist = new MountPlaylistSpotifyForOpenInBrowser();
        }

        public string GetIdPlaylist(double temperature)
        {
            if (temperature > 30)
            {
                //playlist party
                return SelectIdPlaylist((int)PlaylistSpotify.Party);
            }
            if (temperature >= 15 && temperature <= 30)
            {
                //playlist Pop
                return SelectIdPlaylist((int)PlaylistSpotify.Pop);
            }
            if (temperature >= 10 && temperature <= 14)
            {
                //playlist Rock
                return SelectIdPlaylist((int)PlaylistSpotify.Rock);
            }
            if (temperature < 10)
            {
                //playlist Classic
                return SelectIdPlaylist((int)PlaylistSpotify.Classic);
            }
            return null;
        }

        private string SelectIdPlaylist(int playlistSpotify)
        {
            var result = _playlist.MountPlaylist();
            return result.Where(p => p.playlist == playlistSpotify).Select(p => p.id).FirstOrDefault();
        }
    }
}
