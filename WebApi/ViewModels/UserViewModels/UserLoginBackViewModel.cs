using System;

namespace WebAPI.ViewModels.UserViewModels
{
    public class UserLoginBackViewModel
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
        public DateTime token_expires { get; set; }
    }
}
