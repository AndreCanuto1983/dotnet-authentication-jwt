using System.ComponentModel.DataAnnotations;
using WebAPI.ViewModels.UserViewModels;

namespace WebAPI.FronViewModels.UserViewModels
{
    public class UserResetPasswordViewModel : UserRegisterViewModel
    {
        [Required(ErrorMessage = "O Token é obrigatório!")]
        public string Token { get; set; }
    }
}
