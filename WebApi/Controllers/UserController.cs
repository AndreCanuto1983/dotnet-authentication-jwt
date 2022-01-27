using Core.Exceptions;
using Core.Interfaces;
using Core.Models.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenWeatherMap.Interface;
using OpenWeatherMap.Models;
using Spotify.Interfaces;
using Spotify.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.FronViewModels.UserViewModels;
using WebAPI.Interfaces;
using WebAPI.Models.AccessModels;
using WebAPI.Models.NoteModel;
using WebAPI.ViewModels.UserViewModels;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        #region Dependency Injection

        private readonly IGenerateToken _token;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly AppSettings _appSettings;
        private UserLoginBackViewModel _responseLogin;

        public UserController(
           SignInManager<User> signInManager,
           UserManager<User> userManager,
           IOptions<AppSettings> appSettings,
           IGenerateToken token,
           UserLoginBackViewModel responseLogin
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _token = token;
            _responseLogin = responseLogin;
        }

        #endregion

        /// <summary>
        /// User Register
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegisterViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            try
            {
                var user = new User()
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true,
                    City = model.City
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded) return BadRequest(result.Errors);

                var userConsult = await _userManager.FindByEmailAsync(model.Email);

                await _signInManager.SignInAsync(user, false);

                _responseLogin.token_type = "Bearer";
                _responseLogin.access_token = await _token.GenerateJwt(model.Email, _userManager, _appSettings);
                _responseLogin.token_expires = DateTime.Now.AddHours(_appSettings.Expiration);

                return Ok(_responseLogin);
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

        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

                _responseLogin.token_type = "Bearer";
                _responseLogin.access_token = await _token.GenerateJwt(model.Email, _userManager, _appSettings);
                _responseLogin.token_expires = DateTime.Now.AddHours(_appSettings.Expiration);

                if (result.Succeeded) return Ok(_responseLogin);

                return BadRequest();
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

        /// <summary>
        /// User Logout
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok();
            }
            catch (CustomErrorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("resetpassword")]
        public async Task<ActionResult> ResetPassword([FromBody] UserResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                }

                return Ok();
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

        /// <summary>
        /// forgot password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("forgotpassword")]
        public async Task<ActionResult> ForgotPassword([FromBody] UserForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null && await _userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);

                    return base.Ok();
                }

                return Ok();
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