using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.UserViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "O campo Email é obrigatório")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Password é obrigatório")]
        [MinLength(4, ErrorMessage = "O campo Password deve possuir no mínimo 4 caracteres")]
        [MaxLength(10, ErrorMessage = "O campo Password deve possuir no máximo 10 caracteres")]
        public string Password { get; set; }
    }
}
