using FinFolio.Web.Infrastructure.Models;

namespace FinFolio.Web.Infrastructure
{
    public interface IPortfolioFunctionAdapter
    {
        Task<OperationResult<TResponseDto>> ExecuteFunction<TRequestDto, TResponseDto>(string functionName, TRequestDto request);

    }
}
