namespace Spotify.Interfaces
{
    public interface IGetExternalToken
    {
        string GetAccessToken(string clientid, string clientsecret);
    }
}
