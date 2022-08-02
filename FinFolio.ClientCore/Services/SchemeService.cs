using FinFolio.PortFolio.DTO;
using FinFolio.PortFolioCore.Interfaces;
using FinFolio.PortFolioRepository.Entities;
using FinFolio.PortFolioRepository.Interfaces;

namespace FinFolio.PortFolioCore.Services
{
    public class SchemeService : ISchemeService
    {
        private readonly ISchemeRepository _schemeRepository;
        public SchemeService(ISchemeRepository schemeRepository)
        {
            _schemeRepository = schemeRepository;
        }
        public async Task<List<SchemeDto>> GetSchemesAsync(string name)
        {
            try
            {
                List<Scheme> schemes = await _schemeRepository.GetSchemesAsync(name);
                return schemes.Select(s =>
                    new SchemeDto
                    {
                        Id = s.Id,
                        Code = s.Code,
                        AMC = s.AMC,
                        IsActive = s.IsActive,
                        LaunchDate = s.LaunchDate,
                        NAVName = s.NAVName
                    }).ToList();
            }
            catch (Exception ex)
            {//TODO: Implement logging
                throw;
            }

        }
    }
}
