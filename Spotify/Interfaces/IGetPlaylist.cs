namespace Spotify.Interfaces
{
    public interface IGetPlaylist<T,A>
    {
        T GetPlaylist(A value);
    }
}
