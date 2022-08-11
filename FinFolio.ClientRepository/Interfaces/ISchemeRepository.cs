using FinFolio.PortFolioRepository.Entities;

namespace FinFolio.PortFolioRepository.Interfaces
{
    public interface ISchemeRepository
    {
        Task<List<Scheme>> GetSchemesAsync(string name);
        Task<Scheme> GetSchemeDetailsAsync(int id);
    }
}
