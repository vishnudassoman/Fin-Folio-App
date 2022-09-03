using FinFolio.PortFolio.DTO;
using FinFolio.Web.Infrastructure;
using FinFolio.Web.Infrastructure.Models;
using FinFolio.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinFolio.Web.Controllers
{
    [AllowAnonymous]
    public class SchemeController : Controller
    {
        private readonly ILogger<SchemeController> _logger;
        private readonly IPortfolioFunctionAdapter _portfolioFunctionAdapter;
        public SchemeController(ILogger<SchemeController> logger, IPortfolioFunctionAdapter portfolioFunctionAdapter)
        {
            _logger = logger;
            _portfolioFunctionAdapter = portfolioFunctionAdapter;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> SchemeDetails(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            SchemeViewModel schemeViewModel = await this.GetSchemeDetails(id);
            return View(schemeViewModel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Search([FromBody] Data searchData)
        {
            if (searchData != null && searchData.where != null && searchData.where.Count > 0)
            {
                List<SchemeViewModel> schemes = await this.GetSchemesAsync(searchData.where[0].value);
                return new JsonResult(schemes);
            }
            return new NotFoundObjectResult(searchData);
        }
        #region private methods
        private async Task<List<SchemeViewModel>> GetSchemesAsync(string schemeName)
        {
            try
            {
                OperationResult<List<SchemeDto>> result = await _portfolioFunctionAdapter.ExecuteFunction<SchemeRequestDto, List<SchemeDto>>("GetSchemes", new SchemeRequestDto { NAVName = schemeName });
                if (result.IsSuccess)
                {
                    List<SchemeViewModel> schemes = (result.Data != null) ? result.Data.Select(s => new SchemeViewModel
                    {
                        Id = s.Id,
                        AMC = s.AMC,
                        Code = s.Code,
                        IsActive = s.IsActive,
                        LaunchDate = s.LaunchDate,
                        Name = s.NAVName
                    }).ToList() : new List<SchemeViewModel>();
                    return schemes;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty, schemeName);
            }
            return new List<SchemeViewModel>();
        }
        private async Task<SchemeViewModel> GetSchemeDetails(int id)
        {
            try
            {
                OperationResult<SchemeDto> result = await _portfolioFunctionAdapter.ExecuteFunction<SchemeRequestDto, SchemeDto>("GetSchemeDetails", new SchemeRequestDto { Id = id });
                if (result.IsSuccess)
                {
                    SchemeViewModel schemeVM = (result.Data != null) ? new SchemeViewModel
                    {
                        Id = result.Data.Id,
                        AMC = result.Data.AMC,
                        Code = result.Data.Code,
                        IsActive = result.Data.IsActive,
                        LaunchDate = result.Data.LaunchDate,
                        Name = result.Data.NAVName,
                        NAVHistory = result.Data.NAV
                            .Select<NavDto, SchemeNavViewModel>(navDto =>
                            new SchemeNavViewModel
                            {
                                Date = navDto.Date,
                                Value = navDto.Value
                            })
                        .ToList()
                    } : new SchemeViewModel();
                    return schemeVM;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while fetching scheme with id", id);
            }
            return new SchemeViewModel();
        }
        #endregion Private Methods
    }
}
