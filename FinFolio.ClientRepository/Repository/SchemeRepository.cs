using FinFolio.PortFolioRepository.Entities;
using FinFolio.PortFolioRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinFolio.PortFolioRepository.Repository
{
    public class SchemeRepository : ISchemeRepository
    {
        PortFolioDBContext _dbContext;
        public SchemeRepository(PortFolioDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Scheme>> GetSchemesAsync(string name)
        {
            return await _dbContext.Schemes
                                    .Where(x => x.NAVName.Contains(name))
                                    .OrderBy(s => s.NAVName)
                                    .ToListAsync();
        }

        public async Task<Scheme> GetSchemeDetailsAsync(int id)
        {
            return await _dbContext.Schemes.FindAsync(id);
        }
    }
}
