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
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace FinFolio.PortFolio.WebAPI.Functions
{
    public class List
    {
        private readonly ILogger<List> _logger;
        private readonly IWishlistService _wishlistService;
        public List(ILogger<List> log, IWishlistService wishlistService)
        {
            _logger = log;
            _wishlistService = wishlistService;
        }

        [FunctionName("GetWishListByUser")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "userid", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The id of the user")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(System.Collections.Generic.List<WishlistDto>), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function GetWishListByUser - start");

            int userId = 0;

            List<WishlistDto> wishlist = null;
            try
            {
                if (!int.TryParse(req.Query["userid"], out userId))
                {
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    UserDto user = JsonConvert.DeserializeObject<UserDto>(requestBody);
                    userId = user.Id;
                }
                if (userId != 0)
                {
                    wishlist = await _wishlistService.GetWishlistByUserIdAsync(userId);
                }
                else
                    return new BadRequestObjectResult(wishlist);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);
            }

            _logger.LogInformation("C# HTTP trigger function GetWishListByUser - end");

            return new OkObjectResult(wishlist);
        }
    }
}

