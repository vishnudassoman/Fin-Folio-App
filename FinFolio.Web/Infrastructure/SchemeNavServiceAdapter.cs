using FinFolio.Web.Infrastructure.Models;
using FinFolio.Web.Models;
using Newtonsoft.Json;
using System.Globalization;

namespace FinFolio.Web.Infrastructure
{
    public class SchemeNavServiceAdapter : ISchemeNavServiceAdapter
    {
        private readonly ILogger<SchemeNavServiceAdapter> _logger;
        public SchemeNavServiceAdapter(ILogger<SchemeNavServiceAdapter> logger)
        {
            _logger = logger;
        }
        public async Task<SchemeNavViewModel> GetSchemeNavAsync(string schemeCode)
        {
            try
            {
                HttpClient client = new HttpClient();
                using (client)
                {
                    HttpResponseMessage responseMessage = await client.GetAsync($"https://api.mfapi.in/mf/{schemeCode}");
                    if (responseMessage != null && responseMessage.IsSuccessStatusCode)
                    {
                        string serialized = await responseMessage.Content.ReadAsStringAsync();
                        MfApiData navData = JsonConvert.DeserializeObject<MfApiData>(serialized);
                        if (navData != null && navData.status == "SUCCESS" && navData.data != null && navData.data.Count > 0)
                        {
                            SchemeNavViewModel navViewModel = new SchemeNavViewModel();
                            navViewModel.Date = DateTime.ParseExact(navData.data[0].date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                            navViewModel.LatestNAV = Convert.ToDecimal(navData.data[0].nav);
                            if (navData.data.Count > 1)
                            {
                                navViewModel.PreviousNAV = Convert.ToDecimal(navData.data[1].nav);
                            }
                            return navViewModel;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting NAV values", schemeCode);
            }
            return null;
        }
    }
}
