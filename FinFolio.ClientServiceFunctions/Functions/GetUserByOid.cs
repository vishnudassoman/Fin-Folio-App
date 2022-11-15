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
    public class GetUserByOid
    {
        private readonly ILogger<GetUserByOid> _logger;
        private readonly IUserService _userService;
        public GetUserByOid(ILogger<GetUserByOid> log, IUserService userService)
        {
            _logger = log;
            _userService = userService;
        }

        [FunctionName("GetUserByOid")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "User" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "Oid", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Object Identifier(GUID) ** parameter")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UserDto), Description = "A UserDto object instance with value in Object Identifier property", Example = typeof(UserDto), Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(UserDto), Description = "Returns the user details as UserDto for the given object identifier of the user")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function GetUserByOid - start.");

            string userId = req.Query["Oid"];
            Guid userObjectIdentifier;
            UserDto user = null;
            try
            {
                if (userId == null || userId.Length == 0)
                {
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    UserDto userRequest = JsonConvert.DeserializeObject<UserDto>(requestBody);
                    userObjectIdentifier = userRequest.ObjectIdentifier;
                }
                else
                {
                    userObjectIdentifier = new Guid(userId);
                }
                user = await _userService.GetUserAsync(userObjectIdentifier);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);
            }
            _logger.LogInformation("C# HTTP trigger function GetUserByOid - end.");
            return new OkObjectResult(user);
        }
    }
}

