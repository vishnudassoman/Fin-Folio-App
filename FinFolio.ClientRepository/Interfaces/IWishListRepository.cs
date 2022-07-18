using FinFolio.PortFolioRepository.Entities;

namespace FinFolio.PortFolioRepository.Interfaces
{
    public interface IWishlistRepository
    {
        Task<List<Wishlist>> GetWishlistByUserIdAsync(int userId);
        Task<Wishlist> CreateWishListAsync(Wishlist wishlist);
        Task<Wishlist> UpdateWishlistAsync(Wishlist wishlist);
    }
}
