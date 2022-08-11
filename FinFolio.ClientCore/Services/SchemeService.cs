using FinFolio.PortFolio.DTO;
using FinFolio.PortFolioCore.ExternalModels;
using FinFolio.PortFolioCore.Interfaces;
using FinFolio.PortFolioRepository.Entities;
using FinFolio.PortFolioRepository.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FinFolio.PortFolioCore.Services
{
    public class SchemeService : ISchemeService
    {
        private readonly ISchemeRepository _schemeRepository;
        private readonly ILogger<SchemeService> _logger;
        public SchemeService(ISchemeRepository schemeRepository, ILogger<SchemeService> logger)
        {
            _schemeRepository = schemeRepository;
            _logger = logger;
        }
        public async Task<List<SchemeDto>> GetSchemesAsync(string name)
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

        public async Task<SchemeDto> GetSchemeDetailsAsync(int id)
        {
            Scheme schemeDetails = await _schemeRepository.GetSchemeDetailsAsync(id);
            if (schemeDetails != null)
            {
                return new SchemeDto
                {
                    Id = schemeDetails.Id,
                    AMC = schemeDetails.AMC,
                    Code = schemeDetails.Code,
                    IsActive = schemeDetails.IsActive,
                    LaunchDate = schemeDetails.LaunchDate,
                    NAVName = schemeDetails.NAVName,
                    NAV = await this.GetSchemeNAVAsync(schemeDetails.Code)
                };
            }
            return null;
        }

        private async Task<List<NavDto>> GetSchemeNAVAsync(long code)
        {
            try
            {
                HttpClient client = new HttpClient();
                using (client)
                {
                    HttpResponseMessage responseMessage = await client.GetAsync($"https://api.mfapi.in/mf/{code.ToString()}");
                    if (responseMessage != null && responseMessage.IsSuccessStatusCode)
                    {

                        string serialized = await responseMessage.Content.ReadAsStringAsync();
                        MfApiData navData = JsonConvert.DeserializeObject<MfApiData>(serialized);
                        if (navData != null && navData.status == "SUCCESS")
                        {
                            return navData.data.Select<Nav, NavDto>(nav =>
                                 new NavDto
                                 {
                                     Date = DateTime.Parse(nav.date, null, System.Globalization.DateTimeStyles.AssumeLocal),
                                     Value = Convert.ToDecimal(nav.nav),
                                 }
                                 ).ToList();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting NAV values", code);
            }
            return null;
        }
    }
}
