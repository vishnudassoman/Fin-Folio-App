using FinFolio.PortFolio.DTO;
namespace FinFolio.PortFolioCore.Interfaces
{
    public interface IWishlistService
    {
        Task<List<WishlistDto>> GetWishlistByUserIdAsync(int userId);
        Task<WishlistDto> CreateWishListAsync(WishlistDto wishlist);
        Task<WishlistDto> UpdateWishlistAsync(WishlistDto wishlist);
    }
}
