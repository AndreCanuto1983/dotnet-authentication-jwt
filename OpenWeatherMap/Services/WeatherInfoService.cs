using Newtonsoft.Json;
using OpenWeatherMap.Interface;
using OpenWeatherMap.Models;
using System.Net;

namespace OpenWeatherMap.Services
{
    public class WeatherInfoService : IGetWeatherInfo<WeatherInfo>
    {
        public WeatherInfo GetWeatherInfo(string city, string appId = "27b5237d944993ebfe0a90cb000f38f1")
        {   
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&appid={1}", city, appId);
            WeatherInfo weatherInfo = null;

            using (WebClient client = new WebClient())
            {
                string response = client.DownloadString(url);

                weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(response);
            }

            return weatherInfo;
        }
    }
}
