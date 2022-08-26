using FinFolio.PortFolioRepository.Entities;

namespace FinFolio.PortFolioRepository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(int id);
        Task<User> GetUserAsync(Guid oid);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
    }
}
