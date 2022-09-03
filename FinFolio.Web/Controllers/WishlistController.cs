using FinFolio.PortFolio.DTO;
using FinFolio.Web.Infrastructure;
using FinFolio.Web.Infrastructure.Models;
using FinFolio.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinFolio.Web.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly ILogger<WishlistController> _logger;
        private readonly IPortfolioFunctionAdapter _portfolioFunctionAdapter;
        SaveAndListViewModel<WishlistViewModel> resultViewModel;

        public WishlistController(ILogger<WishlistController> logger, IPortfolioFunctionAdapter portfolioFunctionAdapter)
        {
            _logger = logger;
            _portfolioFunctionAdapter = portfolioFunctionAdapter;

            resultViewModel = new SaveAndListViewModel<WishlistViewModel>();
            resultViewModel.Result = new OperationResultViewModel();
            resultViewModel.Result.Message = $"Failed to add the scheme to your wishlist. Please try again!";
            resultViewModel.Result.ShowAlert = true;
            resultViewModel.Result.Type = AlertType.Error;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string id)
        {
            int userId = 1;
            int schemeId;
            if (int.TryParse(id, out schemeId) && schemeId != 0)
            {

                return View(await AddToWishListAsync(userId, schemeId));

            }
            return View(resultViewModel);
        }

        #region Private Methods
        private async Task<SaveAndListViewModel<WishlistViewModel>> AddToWishListAsync(int userId, int schemeId)
        {
            try
            {
                OperationResult<WishlistDto> result = await _portfolioFunctionAdapter.ExecuteFunction<WishlistDto, WishlistDto>("AddToWishlist", new WishlistDto { UserID = userId, Scheme = new SchemeDto { Id = schemeId } });
                List<WishlistViewModel> wishlist = await this.GetWishlistByUserAsync(userId);
                resultViewModel.ListItems = wishlist;
                if (result.IsSuccess && result.Data != null && result.Data.Id > 0)
                {
                    resultViewModel.Result.IsSuccess = true;
                    resultViewModel.Result.Message = $"Scheme {result.Data.Scheme.NAVName} has been added to your wishlist!";
                    resultViewModel.Result.ShowAlert = true;
                    resultViewModel.Result.Type = AlertType.Success;
                }
                else
                {
                    resultViewModel.Result.IsSuccess = false;
                    resultViewModel.Result.Message = result.ErrorMessage;
                    resultViewModel.Result.ShowAlert = true;
                    resultViewModel.Result.Type = AlertType.Warning;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"For input parameters  UserId ={userId} and SchemeId-{schemeId}", schemeId);
            }
            return resultViewModel;
        }
        private async Task<List<WishlistViewModel>> GetWishlistByUserAsync(int userId)
        {
            try
            {
                OperationResult<List<WishlistDto>> result = await _portfolioFunctionAdapter.ExecuteFunction<UserDto, List<WishlistDto>>("GetWishlistByUser", new UserDto { Id = userId });
                if (result.IsSuccess)
                {
                    List<WishlistViewModel> wishlistViewModels = (result.Data != null) ? result.Data.Select(wl => new WishlistViewModel
                    {
                        Id = wl.Id,
                        Code = wl.Scheme.Code,
                        NAVName = wl.Scheme.NAVName,
                    }).ToList() : new List<WishlistViewModel>();
                    return wishlistViewModels;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"For input parameters  UserId ={userId}", userId);
            }
            return new List<WishlistViewModel>();
        }
        #endregion Private Methods
    }
}
