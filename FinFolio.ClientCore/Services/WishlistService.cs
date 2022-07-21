using FinFolio.PortFolio.DTO;
using FinFolio.PortFolioCore.Interfaces;
using FinFolio.PortFolioRepository.Entities;
using FinFolio.PortFolioRepository.Interfaces;

namespace FinFolio.PortFolioCore.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;

        public WishlistService(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }
        public async Task<WishlistDto> CreateWishListAsync(WishlistDto wishlist)
        {
            WishlistDto resultWishlistDto = null;
            try
            {
                Wishlist wishListToSave = new Wishlist
                {//TODO: Automapper Integration
                    SchemeId = wishlist.Scheme.Id,
                    UserID = wishlist.UserID
                };

                Wishlist createdWishlist = await _wishlistRepository.CreateWishListAsync(wishListToSave);

                resultWishlistDto = new WishlistDto
                {//TODO: Automapper Integration
                    Id = createdWishlist.Id,
                    UserID = createdWishlist.UserID,
                    Scheme = new SchemeDto
                    {
                        Id = createdWishlist.SchemeId,
                        Code = createdWishlist.Scheme.Code,
                        IsActive = createdWishlist.Scheme.IsActive,
                        LaunchDate = createdWishlist.Scheme.LaunchDate,
                        NAVName = createdWishlist.Scheme.NAVName
                    }
                };
            }
            catch (Exception)
            {//TODO: Implement Logging

                throw;
            }
            return resultWishlistDto;
        }

        public async Task<List<WishlistDto>> GetWishlistByUserIdAsync(int userId)
        {
            List<WishlistDto> wishListDto = null;
            try
            {
                wishListDto = new List<WishlistDto>();
                List<Wishlist> createdWishlist = await _wishlistRepository.GetWishlistByUserIdAsync(userId);

                if (createdWishlist != null && createdWishlist.Count > 0)
                {//TODO: Automapper Integration
                    wishListDto.AddRange(
                                     createdWishlist.Select<Wishlist, WishlistDto>((w) => new WishlistDto
                                     {
                                         Id = w.Id,
                                         UserID = w.UserID,
                                         Scheme = new SchemeDto { Id = w.SchemeId, Code = w.Scheme.Code, IsActive = w.Scheme.IsActive, LaunchDate = w.Scheme.LaunchDate }
                                     }).ToList());
                }
            }
            catch (Exception)
            {//TODO: Implement Logging

                throw;
            }
            return wishListDto;
        }

        public async Task<WishlistDto> UpdateWishlistAsync(WishlistDto wishlist)
        {
            WishlistDto resultWishlistDto = null;
            try
            {
                Wishlist wishListToSave = new Wishlist
                {//TODO: Automapper Integration
                    Id = wishlist.Id,
                    SchemeId = wishlist.Scheme.Id,
                    UserID = wishlist.UserID
                };

                Wishlist createdWishlist = await _wishlistRepository.UpdateWishlistAsync(wishListToSave);

                resultWishlistDto = new WishlistDto
                {//TODO: Automapper Integration
                    Id = createdWishlist.Id,
                    UserID = createdWishlist.UserID,
                    Scheme = new SchemeDto
                    {
                        Id = createdWishlist.SchemeId,
                        Code = createdWishlist.Scheme.Code,
                        IsActive = createdWishlist.Scheme.IsActive,
                        LaunchDate = createdWishlist.Scheme.LaunchDate,
                        NAVName = createdWishlist.Scheme.NAVName
                    }
                };
            }
            catch (Exception)
            {//TODO: Implement Logging

                throw;
            }
            return resultWishlistDto;
        }
    }
}
