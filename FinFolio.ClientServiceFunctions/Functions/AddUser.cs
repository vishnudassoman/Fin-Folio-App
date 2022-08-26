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
    public class AddUser
    {
        private readonly ILogger<AddUser> _logger;
        private readonly IUserService _userService;
        public AddUser(ILogger<AddUser> log, IUserService userService)
        {
            _logger = log;
            _userService = userService;
        }

        [FunctionName("AddUser")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(UserDto), Description = "The UserDto object response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function AddUser - start");
            UserDto newUser = null;
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                newUser = JsonConvert.DeserializeObject<UserDto>(requestBody);

                //TODO: validate the input.
                newUser = await _userService.AddUserAsync(newUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);
            }

            _logger.LogInformation("C# HTTP trigger function AddUser - end");
            return new OkObjectResult(newUser);
        }
    }
}

