using Spotify.Enums;
using Spotify.Interfaces;
using Spotify.Models;
using System.Linq;

namespace Spotify.Services
{
    public class GetPlaylistSpotifyForOpenInBrowser : IGetPlaylist<string, double>
    {
        #region Dependency Injection

        private readonly IOpenInBrowser _open;
        private readonly IMountPlayListSpotify<Playlists> _playlist;

        public GetPlaylistSpotifyForOpenInBrowser()
        {
            _playlist = new MountPlaylistSpotifyForOpenInBrowser();
            _open = new OpenInBrowser();
        }

        #endregion

        public string GetPlaylist(double temperature)
        {
            if (temperature > 30)
            {
                //playlist party
                return SelectPlaylistToOpen((int)PlaylistSpotify.Party);
            }
            if (temperature >= 15 && temperature <= 30)
            {
                //playlist Pop
                return SelectPlaylistToOpen((int)PlaylistSpotify.Pop);
            }
            if (temperature >= 10 && temperature <= 14)
            {
                //playlist Rock
                return SelectPlaylistToOpen((int)PlaylistSpotify.Rock);
            }
            if (temperature < 10)
            {
                //playlist Classic
                return SelectPlaylistToOpen((int)PlaylistSpotify.Classic);
            }
            else
            {
                return "Nenhuma playlist de sugestão";
            }
        }

        private string SelectPlaylistToOpen(int playlistSpotify)
        {
            var result = _playlist.MountPlaylist();
            var url = result.Where(p => p.playlist == playlistSpotify).Select(p => p.url).FirstOrDefault();

            _open.OpenUrl(url);

            return url;
        }
    }
}
