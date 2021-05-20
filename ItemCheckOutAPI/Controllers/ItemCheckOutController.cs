using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nanoservices.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ItemCheckOutAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCheckOutController : ControllerBase
    {
        private readonly string baseURL;
        private readonly IConfiguration _configuration;
        public ItemCheckOutController(IConfiguration configuration)
        {
            _configuration = configuration;
            baseURL = _configuration.GetSection("MyAppSettings").GetSection("AzureFunctionURL").Value;
        }

        [HttpGet("IsItemAvailable")]
        public async Task<bool> IsItemAvailable(string productCode)
        {
            return await IsItemAvailableInternal(productCode);
        }

        [HttpGet("GetAllCartItems")]
        public async Task<List<CartItem>> GetAllCartItems()
        {
            return await GetAllCartItemsInternal();
        }

        [HttpGet("GetAllProducts")]
        public async Task<List<Product>> GetAllProducts()
        {
            return await GetAllProductsInternal();
        }

        [HttpGet("GetAllCustomers")]
        public async Task<List<Customer>> GetAllCustomers()
        {
            return await GetAllCustomersInternal();
        }

        [HttpPost("AddItemToCart")]
        public async Task<bool> AddItemToCart([FromBody] CartItem cartItem)
        {
            return await AddItemToCartInternal(cartItem);
        }

        [HttpPost("ProcessPayment")]
        public async Task<bool> ProcessPayment([FromBody] CartItem cart)
        {
            return await Task.FromResult(true);
        }

        [HttpPost("SendConfirmation")]
        public async Task<bool> SendConfirmation([FromBody] CartItem cartItem)
        {
            return await Task.FromResult(true);
        }

        [HttpPut("UpdateStock")]
        public async Task<bool> UpdateStock([FromBody] CartItem cartItem)
        {
            return await UpdateStockInternal(cartItem);
        }
        private async Task<bool> IsItemAvailableInternal(string productCode)
        {
            string azureFunctionBaseUrl = baseURL + "IsItemAvailable";
            string queryStringParams = $"?productCode={productCode}";
            string url = azureFunctionBaseUrl + queryStringParams;
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage responseMessage = await client.GetAsync(url))
                {
                    using (HttpContent content = responseMessage.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                            return bool.Parse(data);
                        return false;
                    }
                }
            }
        }
        private async Task<bool> AddItemToCartInternal(CartItem cartItem)
        {
            string url = baseURL + "AddItemToCart";
            var json = JsonConvert.SerializeObject(cartItem);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsync(url, data);
            }

            return true;
        }
        private async Task<bool> UpdateStockInternal(CartItem cartItem)
        {
            string azureFunctionBaseUrl = baseURL + "UpdateStock";
            string url = azureFunctionBaseUrl;

            var json = JsonConvert.SerializeObject(cartItem);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                var response = await client.PutAsync(url, data);
            }

            return true;
        }
        private async Task<List<CartItem>> GetAllCartItemsInternal()
        {
            string url = baseURL + "GetAllCartItems";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage responseMessage = await client.GetAsync(url))
                {
                    using (HttpContent content = responseMessage.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<CartItem>>(data);
                    }
                }
            }
        }
        private async Task<List<Product>> GetAllProductsInternal()
        {
            string url = baseURL + "GetAllProducts";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage responseMessage = await client.GetAsync(url))
                {
                    using (HttpContent content = responseMessage.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<Product>>(data);
                    }
                }
            }
        }
        private async Task<List<Customer>> GetAllCustomersInternal()
        {
            string url = baseURL + "GetAllCustomers";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage responseMessage = await client.GetAsync(url))
                {
                    using (HttpContent content = responseMessage.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<Customer>>(data);
                    }
                }
            }
        }
    }
}
