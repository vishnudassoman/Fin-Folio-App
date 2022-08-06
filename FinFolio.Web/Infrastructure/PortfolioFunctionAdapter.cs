using FinFolio.Web.Infrastructure.Models;
using Newtonsoft.Json;
using System.Text;

namespace FinFolio.Web.Infrastructure

{
    public class PortfolioFunctionAdapter : IPortfolioFunctionAdapter
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PortfolioFunctionAdapter> _logger;

        public PortfolioFunctionAdapter(IConfiguration configuration, ILogger<PortfolioFunctionAdapter> loggerService)
        {
            _configuration = configuration;
            _logger = loggerService;
        }

        public async Task<OperationResult<TResponseDto>> ExecuteFunction<TRequestDto, TResponseDto>(string functionName, TRequestDto request)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("x-functions-key", _configuration.GetValue("PortFolioWebApi:FunctionKey", string.Empty));

                string url = $"{_configuration.GetValue("PortFolioWebApi:FunctionBaseUrl", string.Empty)}/{functionName}";
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var outputJson = JsonConvert.SerializeObject(request);

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new OperationResult<TResponseDto>(await response.Content.ReadAsStringAsync());
                }

                string serialized = await response.Content.ReadAsStringAsync();
                Type responseType = typeof(TResponseDto);

                if (responseType == typeof(string))
                {
                    return new OperationResult<TResponseDto>()
                    {
                        IsSuccess = true,
                        Data = (TResponseDto)Convert.ChangeType(serialized, typeof(TResponseDto))
                    };
                }
                TResponseDto result = JsonConvert.DeserializeObject<TResponseDto>(serialized);
                return new OperationResult<TResponseDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: "PortfolioFunction Adapter has thrown an exception", new Object());
                return new OperationResult<TResponseDto>($"Error connecting to service");
            }
        }
    }
}
