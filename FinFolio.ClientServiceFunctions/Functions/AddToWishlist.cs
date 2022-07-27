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
    public class AddToWishlist
    {
        private readonly ILogger<AddToWishlist> _logger;
        private readonly IWishlistService _wishlistService;

        public AddToWishlist(ILogger<AddToWishlist> log, IWishlistService wishlistService)
        {
            _logger = log;
            _wishlistService = wishlistService;
        }

        [FunctionName("AddToWishlist")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WishlistDto), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function AddToWishList - start");
            WishlistDto wishlist = null;
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                wishlist = JsonConvert.DeserializeObject<WishlistDto>(requestBody);

                //TODO: validate the input.
                wishlist = await _wishlistService.CreateWishListAsync(wishlist);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);
            }

            _logger.LogInformation("C# HTTP trigger function AddToWishList - end");
            return new OkObjectResult(wishlist);
        }
    }
}

