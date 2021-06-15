using Newtonsoft.Json;
using Spotify.Interfaces;
using Spotify.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Spotify.Services
{
    public class SpotifyRequest : IGetResponseApiExternal<string>
    {
        private IGetExternalToken _getToken;
        private HttpClient client;

        public SpotifyRequest(IGetExternalToken getToken)
        {
            _getToken = getToken;
            client = new HttpClient();
        }

        public async Task<dynamic> GetResponseSpotify(string idPlaylist)
        {
            try
            {
                var accessToken = _getToken.GetAccessToken("991cad9bdfc746a2907d31a8fd35c0c8", "d01b18b300c34ab5a4845c2fb4310541");

                TokenSpotify token = JsonConvert.DeserializeObject<TokenSpotify>(accessToken);

                var url = "https://api.spotify.com/v1/playlists/" + idPlaylist + "/tracks";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);

                string json = await client.GetStringAsync(url);

                var obj = JsonConvert.DeserializeObject<Page>(json);

                return obj;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
