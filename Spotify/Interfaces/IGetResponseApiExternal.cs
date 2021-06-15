using System.Threading.Tasks;

namespace Spotify.Interfaces
{
    public interface IGetResponseApiExternal<T>
    {
        Task<dynamic> GetResponseSpotify(T id);
    }
}
