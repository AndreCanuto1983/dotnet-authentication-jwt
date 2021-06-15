using WebAPI.Models.AccessModels;

namespace WebAPI.ViewModels.UserViewModels.Extensions
{
    public static class UserBackExtension
    {
        public static UserBackViewModel Entity2Front(this User entity)
        {
            return new UserBackViewModel()
            {
                Name = entity.Name,
                Email = entity.Email,
                City = entity.City                
            };
        }
    }
}
