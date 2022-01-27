using Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenWeatherMap.Interface;
using OpenWeatherMap.Models;
using Spotify.Interfaces;
using Spotify.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.AccessModels;
using WebAPI.ViewModels.UserViewModels;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SpotifyTracksSuggestionController : ControllerBase
    {
        #region Dependency Injection

        private readonly IWeatherInfoService<WeatherInfo> _weatherInfo;
        private readonly IGetIdPlaylist<string, double> _playlistGetId;
        private readonly IGetResponseApiExternal<string> _spotifyRequest;
        private readonly IGetPlaylist<string, double> _playlist;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private Page _page;
        private UserBackViewModel _response;

        public SpotifyTracksSuggestionController(
           UserManager<User> userManager,
           IWeatherInfoService<WeatherInfo> weatherInfo,
           IGetResponseApiExternal<string> spotifyRequest,
           IGetIdPlaylist<string, double> playlistGetId,
           IGetPlaylist<string, double> playlist,
           UserBackViewModel response,
           Page page)
        {
            _userManager = userManager;
            _weatherInfo = weatherInfo;
            _spotifyRequest = spotifyRequest;
            _playlistGetId = playlistGetId;
            _playlist = playlist;
            _page = page;
            _response = response;
        }

        #endregion

        /// <summary>
        /// User Get, retorna usuário e playlist
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        [HttpGet("{userEmail}")]
        public async Task<ActionResult> Get(string userEmail)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values);

            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);

                WeatherInfo watherInfo = null;

                if (!string.IsNullOrEmpty(user.City))
                {
                    watherInfo = _weatherInfo.GetWeatherInfo(user.City);

                    _page = await _spotifyRequest.GetResponseSpotify(_playlistGetId.GetIdPlaylist(watherInfo.main.temp));

                    _response.Name = user.Name;
                    _response.City = user.City;
                    _response.Email = user.Email;
                    _response.tracks = _page?.items?.Select(i => i.track).ToList();
                    _response.url = _playlist.GetPlaylist(watherInfo.main.temp);
                }

                return Ok(_response);
            }
            catch (CustomErrorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
