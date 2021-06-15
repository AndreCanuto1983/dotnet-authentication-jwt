using Core.Models.Base;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebAPI.Models.AccessModels;

namespace WebAPI.Interfaces
{
    public interface IGenerateToken
    {
        Task<string> GenerateJwt(string email, UserManager<User> userManager, AppSettings appSettings);
    }
}
