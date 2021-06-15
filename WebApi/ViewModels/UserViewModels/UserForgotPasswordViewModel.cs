using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.UserViewModels
{
    public class UserForgotPasswordViewModel
    {
        [Required(ErrorMessage = "O campo Email é obrigatório")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
