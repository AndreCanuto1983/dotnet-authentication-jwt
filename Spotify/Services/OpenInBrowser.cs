using Spotify.Interfaces;

namespace Spotify.Services
{
    public class OpenInBrowser : IOpenInBrowser
    {
        public void OpenUrl(string url)
        {
            System.Diagnostics.Process.Start("explorer.exe", url);
        }
    }
}
