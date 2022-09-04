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
        int _userId = 1;

        public WishlistController(ILogger<WishlistController> logger, IPortfolioFunctionAdapter portfolioFunctionAdapter)
        {
            _logger = logger;
            _portfolioFunctionAdapter = portfolioFunctionAdapter;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<WishlistViewModel> wishlist = await this.GetWishlistByUserAsync(_userId);
            SaveAndListViewModel<WishlistViewModel> resultViewModel = new SaveAndListViewModel<WishlistViewModel>();
            resultViewModel.ListItems = wishlist;
            return View(resultViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int Id, [Bind("Id")] SchemeViewModel schemeVM)
        {
            OperationResultViewModel resultViewModel = await AddToWishListAsync(_userId, schemeVM.Id);
            TempData.Put<OperationResultViewModel>(Constants.ADDTOWISHLISTRESULT, resultViewModel);
            return RedirectToAction("SchemeDetails", "Scheme", new { id = Id });
        }

        #region Private Methods
        private async Task<OperationResultViewModel> AddToWishListAsync(int userId, int schemeId)
        {
            OperationResultViewModel resultViewModel = new OperationResultViewModel();
            try
            {
                OperationResult<WishlistDto> result = await _portfolioFunctionAdapter.ExecuteFunction<WishlistDto, WishlistDto>("AddToWishlist", new WishlistDto { UserID = userId, Scheme = new SchemeDto { Id = schemeId } });
                resultViewModel.ShowAlert = true;
                if (result.IsSuccess && result.Data != null && result.Data.Id > 0)
                {
                    resultViewModel.IsSuccess = true;
                    resultViewModel.Message = $"Scheme {result.Data.Scheme.NAVName} has been added to your wishlist!";
                    resultViewModel.Type = AlertType.Success;
                }
                else
                {
                    resultViewModel.Message = result.ErrorMessage;
                    resultViewModel.Type = AlertType.Warning;
                }
            }
            catch (Exception ex)
            {
                resultViewModel.Message = $"Failed to add the scheme to your wishlist. Please try again!";
                resultViewModel.Type = AlertType.Error;
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
