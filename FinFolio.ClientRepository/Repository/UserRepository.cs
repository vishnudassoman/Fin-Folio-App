using FinFolio.PortFolioRepository.Entities;
using FinFolio.PortFolioRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinFolio.PortFolioRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        PortFolioDBContext _dbContext;
        public UserRepository(PortFolioDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AddUserAsync(User user)
        {
            return await this.UpsertUserAsync(user);
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<User> GetUserAsync(Guid oid)
        {
            return await _dbContext.Users
                         .FirstOrDefaultAsync(u => u.ObjectIdentifier == oid);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            return await this.UpsertUserAsync(user);
        }

        private async Task<User> UpsertUserAsync(User user)
        {
            using (_dbContext)
            {
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
                return user;
            }
        }
    }
}
