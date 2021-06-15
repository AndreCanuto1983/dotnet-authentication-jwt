using Spotify.Enums;
using Spotify.Interfaces;
using Spotify.Models;
using System.Collections.Generic;

namespace Spotify.Services
{
    public class MountPlaylistSpotifyForOpenInBrowser : IMountPlayListSpotify<Playlists>
    {
        public List<Playlists> MountPlaylist()
        {
            var playlist = new List<Playlists>()
            {
                new Playlists { playlist=(int)PlaylistSpotify.Party, id="1BVPSd4dynzdlIWehjvkPj", url="https://open.spotify.com/playlist/1BVPSd4dynzdlIWehjvkPj"},
                new Playlists { playlist=(int)PlaylistSpotify.Pop, id="5sTHqyG2DAwmTCopHXHRdz", url="https://open.spotify.com/playlist/5sTHqyG2DAwmTCopHXHRdz"},
                new Playlists { playlist=(int)PlaylistSpotify.Rock, id="3rEtasq7NeGLOrLRxwBCUb", url="https://open.spotify.com/playlist/3rEtasq7NeGLOrLRxwBCUb"},
                new Playlists { playlist=(int)PlaylistSpotify.Classic, id="1h0CEZCm6IbFTbxThn6Xcs", url="https://open.spotify.com/playlist/1h0CEZCm6IbFTbxThn6Xcs"},
            };

            return playlist;
        }
    }
}
