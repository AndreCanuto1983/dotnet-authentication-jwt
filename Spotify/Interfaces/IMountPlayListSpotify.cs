using System.Collections.Generic;

namespace Spotify.Interfaces
{
    public interface IMountPlayListSpotify<T>
    {
        List<T> MountPlaylist();
    }
}
