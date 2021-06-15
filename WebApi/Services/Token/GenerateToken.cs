using Core.Models.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Models.AccessModels;

namespace WebAPI.Services.Token
{
    public class GenerateToken : IGenerateToken
    {
        public async Task<string> GenerateJwt(string email, UserManager<User> userManager, AppSettings appSettings)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var identityClaims = new ClaimsIdentity();

            var user = await userManager.FindByEmailAsync(email);

            identityClaims.AddClaims(await userManager.GetClaimsAsync(user));

            //outra maneira de fazer
            var _subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Email)                        
            });

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = _subject, //posso usar este modo também, que está recebendo as claims do user  -> identityClaims,
                Issuer = appSettings.Emissor,
                Audience = appSettings.OriginValidate,
                Expires = DateTime.UtcNow.AddHours(appSettings.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
