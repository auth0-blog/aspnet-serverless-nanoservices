using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Nanoservices.Entities;
using Nanoservices.Infrastructure.Definitions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace NanoservicesFunctionApp
{
    public class CartManager
    {
        private readonly ICheckOutService _checkoutService;
        public CartManager(ICheckOutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [FunctionName("IsItemAvailable")]
        public async Task<IActionResult> IsItemAvailable(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            HttpRequest req, ILogger log)
        {
            string productCode = req.Query["productcode"];
            return new OkObjectResult(await _checkoutService.IsItemAvailable(productCode));
        }

        [FunctionName("GetAllCartItems")]
        public async Task<IActionResult> GetAllCartItems(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            HttpRequest req, ILogger log)
        {
            return new OkObjectResult(await _checkoutService.GetAllCartItems());
        }

        [FunctionName("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            HttpRequest req, ILogger log)
        {
            return new OkObjectResult(await _checkoutService.GetAllCustomers());
        }

        [FunctionName("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            HttpRequest req, ILogger log)
        {
            return new OkObjectResult(await _checkoutService.GetAllProducts());
        }

        [FunctionName("UpdateStock")]
        public async Task<IActionResult> UpdateStock(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", "put", Route = null)]
            HttpRequestMessage req, ILogger log)
        {
            string jsonContent = await req.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(jsonContent))
            {
                return new BadRequestErrorMessageResult("Invalid input.");
            }

            CartItem cartItem = JsonConvert.DeserializeObject<CartItem>(jsonContent);
            return new OkObjectResult(await _checkoutService.UpdateStock(cartItem));
        }

        [FunctionName("AddItemToCart")]
        public async Task<IActionResult> AddItemToCart(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            HttpRequestMessage req, ILogger log)
        {
            string jsonContent = await req.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(jsonContent))
            {
                return new BadRequestErrorMessageResult("Invalid input.");
            }

            CartItem cartItem = JsonConvert.DeserializeObject<CartItem>(jsonContent);
            return new OkObjectResult(await _checkoutService.AddItemToCart(cartItem));
        }
    }
}