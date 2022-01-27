using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using OpenWeatherMap.Interface;
using OpenWeatherMap.Models;
using OpenWeatherMap.Services;
using Spotify.Interfaces;
using Spotify.Services;
using WebAPI.Interfaces;
using WebAPI.Models.NoteModel;
using WebAPI.Repository;
using WebAPI.Services.Token;
using WebAPI.ViewModels.UserViewModels;

namespace WebAPI.Config
{
    public static class ServiceExtensions
    {
        public static void InterfaceConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IRepository<Notes>, NotesRepository>();
            services.AddScoped<IGenerateToken, GenerateToken>();
            services.AddScoped<IWeatherInfoService<WeatherInfo>, WeatherInfoService>();
            services.AddScoped<IGetResponseApiExternal<string>, SpotifyRequest>();
            services.AddScoped<IGetExternalToken, GetSpotifyToken>();
            services.AddScoped<IGetResponseApiExternal<string>, SpotifyRequest>();
            services.AddScoped<IGetIdPlaylist<string, double>, GetIdPlaylistSpotfy>();
            services.AddScoped<IGetPlaylist<string, double>, GetPlaylistSpotifyForOpenInBrowser>();
            services.AddScoped<UserBackViewModel, UserBackViewModel>();
            services.AddScoped<UserLoginBackViewModel, UserLoginBackViewModel>();
            services.AddScoped<Page, Page>();
        }
    }
}
