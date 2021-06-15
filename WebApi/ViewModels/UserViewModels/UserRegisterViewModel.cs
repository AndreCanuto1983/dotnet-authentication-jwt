using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.UserViewModels
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "O campo Email é obrigatório")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Password é obrigatório")]
        [MinLength(4, ErrorMessage = "O campo Password deve possuir no mínimo 4 caracteres")]
        [MaxLength(10, ErrorMessage = "O campo Password deve possuir no máximo 10 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo ConfirmPassword é obrigatório")]
        [MinLength(4, ErrorMessage = "O campo Password deve possuir no mínimo 4 caracteres")]
        [MaxLength(10, ErrorMessage = "O campo Password deve possuir no máximo 10 caracteres")]
        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "O campo Name é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo City é obrigatório")]
        public string City { get; set; }
    }
}
