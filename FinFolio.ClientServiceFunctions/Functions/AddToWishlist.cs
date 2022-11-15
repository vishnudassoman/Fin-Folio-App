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
        [OpenApiOperation(operationId: "Run", tags: new[] { "Wishlist" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(WishlistDto), Description = "A WishlistDto object instance with UserId and SchemeDto.Id", Example = typeof(WishlistDto), Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WishlistDto), Description = "Adds the scheme to the user's wishlist and returns the added item as wishlistdto")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function AddToWishList - start");
            WishlistDto wishlist = null;
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                wishlist = JsonConvert.DeserializeObject<WishlistDto>(requestBody);

                List<WishlistDto> existingList = await _wishlistService.GetWishlistByUserIdAsync(wishlist.UserID);
                if (existingList != null)
                {
                    WishlistDto existingSchemeDto = existingList.Find(w => w.Scheme?.Id == wishlist.Scheme?.Id);
                    if (existingSchemeDto != null)
                    {
                        return new ObjectResult($"Scheme {existingSchemeDto.Scheme.NAVName} already exists in your wishlist") { StatusCode = (int)HttpStatusCode.Conflict };
                    }
                }
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

