using FinFolio.PortFolio.DTO;

namespace FinFolio.PortFolioCore.Interfaces
{
    public interface ISchemeService
    {
        Task<List<SchemeDto>> GetSchemesAsync(string name);
        Task<SchemeDto> GetSchemeDetailsAsync(int id);
    }
}
