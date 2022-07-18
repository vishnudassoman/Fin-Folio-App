using FinFolio.PortFolioRepository.Entities;
using FinFolio.PortFolioRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace FinFolio.PortFolioRepository.Repository
{
    public class WishlistRepository : IWishlistRepository
    {
        PortFolioDBContext _dbContext;
        public WishlistRepository(PortFolioDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Creates a wishlist item in the db.
        /// </summary>
        /// <param name="wishlist"></param>
        /// <returns></returns>
        public async Task<Wishlist> CreateWishListAsync(Wishlist wishlist)
        {
            return await this.UpsertWishListAsync(wishlist);
        }

        /// <summary>
        /// Returns all the wishlist items for the given userid.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Wishlist>> GetWishlistByUserIdAsync(int userId)
        {
            using (_dbContext)
            {
                return await _dbContext.Wishlist.Where(wl => wl.UserID == userId).ToListAsync();
            }
        }

        /// <summary>
        /// Updates the wishlist item in the db.
        /// </summary>
        /// <param name="wishlist"></param>
        /// <returns></returns>
        public async Task<Wishlist> UpdateWishlistAsync(Wishlist wishlist)
        {
            return await this.UpsertWishListAsync(wishlist);
        }

        /// <summary>
        /// Saves the wishlist item to the database.
        /// If the id=0, then creates a new entry in the db.
        /// If the id has the identity value from the db, then the wishlist item with that id will be updated in the db.
        /// </summary>
        /// <param name="wishlist"></param>
        /// <returns></returns>
        private async Task<Wishlist> UpsertWishListAsync(Wishlist wishlist)
        {
            using (_dbContext)
            {
                _dbContext.Wishlist.Update(wishlist);
                await _dbContext.SaveChangesAsync();
                return wishlist;
            }
        }
    }
}
