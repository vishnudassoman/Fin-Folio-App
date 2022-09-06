using FinFolio.PortFolio.DTO;
using FinFolio.Web.Infrastructure.Models;
using FinFolio.Web.Models;

namespace FinFolio.Web.Infrastructure
{
    public class UserService : IUserService
    {
        private readonly IPortfolioFunctionAdapter _portfolioFunctionAdapter;
        private readonly ILogger<UserService> _logger;

        public UserService(IPortfolioFunctionAdapter portfolioFunctionAdapter, ILogger<UserService> logger)
        {
            _portfolioFunctionAdapter = portfolioFunctionAdapter;
            _logger = logger;
        }

        public async Task<UserViewModel> SaveUserDetails(UserViewModel userViewModel)
        {
            try
            {
                OperationResult<UserDto> result = await _portfolioFunctionAdapter.ExecuteFunction<UserDto, UserDto>("GetUserByOid", new UserDto { ObjectIdentifier = new Guid(userViewModel.ObjectIdentifier) });
                if (result.IsSuccess)
                {
                    if (result.Data != null && result.Data.Id > 0)
                    {//User exists in portfolio database
                        return GetUserViewModel(result);
                    }
                    else
                    {//No user exists in db, hence adding the user to the portfolio database.
                        OperationResult<UserDto> operationResult = await _portfolioFunctionAdapter.ExecuteFunction<UserDto, UserDto>("AddUser", new UserDto { ObjectIdentifier = new Guid(userViewModel.ObjectIdentifier) });
                        if (operationResult.IsSuccess && operationResult.Data != null)
                        {
                            return GetUserViewModel(operationResult);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while adding user with Oid", userViewModel.ObjectIdentifier);
            }
            return new UserViewModel();
        }

        private UserViewModel GetUserViewModel(OperationResult<UserDto> operationResult)
        {
            return new UserViewModel
            {
                Id = operationResult.Data.Id,
                ObjectIdentifier = Convert.ToString(operationResult.Data.ObjectIdentifier)
            };
        }
    }
}