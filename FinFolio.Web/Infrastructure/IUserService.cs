using FinFolio.Web.Models;

namespace FinFolio.Web.Infrastructure
{
    public interface IUserService
    {
        Task<UserViewModel> SaveUserDetails(UserViewModel userViewModel);
    }
}
