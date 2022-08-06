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
        #endregion Private Methods
    }
}
