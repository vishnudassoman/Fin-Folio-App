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
    public class GetUserById
    {
        private readonly ILogger<GetUserById> _logger;
        private readonly IUserService _userService;
        public GetUserById(ILogger<GetUserById> log, IUserService userService)
        {
            _logger = log;
            _userService = userService;
        }

        [FunctionName("GetUserById")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "User" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Id** parameter")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UserDto), Description = "A UserDto object instance with value in Id property", Example = typeof(UserDto), Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(UserDto), Description = "Returns the user details as UserDto for the given user id.")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function GetUserById - start");

            int userId = 0;

            UserDto user = null;
            try
            {


                if (!int.TryParse(req.Query["id"], out userId))
                {
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    UserDto userRequest = JsonConvert.DeserializeObject<UserDto>(requestBody);
                    userId = userRequest.Id;

                }
                user = await _userService.GetUserAsync(userId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);
            }

            _logger.LogInformation("C# HTTP trigger function GetUserById - end");

            return new OkObjectResult(user);
        }
    }
}

