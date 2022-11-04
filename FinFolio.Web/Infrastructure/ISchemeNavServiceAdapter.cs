using FinFolio.Web.Models;

namespace FinFolio.Web.Infrastructure
{
    public interface ISchemeNavServiceAdapter
    {
        Task<SchemeNavViewModel> GetSchemeNavAsync(string schemeCode);
    }
}
