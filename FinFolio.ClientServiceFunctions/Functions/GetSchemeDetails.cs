using FinFolio.PortFolio.DTO;
using FinFolio.PortFolioCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace FinFolio.PortFolio.WebAPI.Functions
{
    public class GetSchemeDetails
    {
        private readonly ILogger<GetSchemeDetails> _logger;
        private readonly ISchemeService _schemeService;
        public GetSchemeDetails(ILogger<GetSchemeDetails> log, ISchemeService schemeService)
        {
            _logger = log;
            _schemeService = schemeService;
        }

        [FunctionName("GetSchemeDetails")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SchemeDto), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function GetSchemeDetails - start.");
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                SchemeRequestDto schemeRequest = JsonConvert.DeserializeObject<SchemeRequestDto>(requestBody);
                SchemeDto scheme = await _schemeService.GetSchemeDetailsAsync(schemeRequest.Id);
                if (scheme != null)
                {
                    return new OkObjectResult(scheme);
                }
                else
                {
                    return new StatusCodeResult(StatusCodes.Status404NotFound); ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            _logger.LogInformation("C# HTTP trigger function GetSchemeDetails - end");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}

