using FinFolio.PortFolioRepository.Entities;

namespace FinFolio.PortFolioRepository.Interfaces
{
    public interface IWishlistRepository
    {
        public Wishlist GetWishlistByUserId(int userId);
        public Wishlist GetWishlistById(int id);
        public Wishlist CreateWishList(Wishlist wishlist);
        public Wishlist UpdateWishlist(Wishlist wishlist);
    }
}
