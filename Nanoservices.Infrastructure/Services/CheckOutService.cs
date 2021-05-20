using Nanoservices.Entities;
using Nanoservices.Infrastructure.Definitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nanoservices.Infrastructure.Services
{
    public class CheckOutService : ICheckOutService
    {
        private readonly ICheckOutRepository _repository;
        public CheckOutService(ICheckOutRepository repository)
        {
            _repository = repository;
        }
        public Task<List<CartItem>> GetAllCartItems()
        {
            return _repository.GetAllCartItems();
        }
        public Task<List<Product>> GetAllProducts()
        {
            return _repository.GetAllProducts();
        }
        public Task<List<Customer>> GetAllCustomers()
        {
            return _repository.GetAllCustomers();
        }
        public Task<bool> AddItemToCart(CartItem cartItem)
        {
            return _repository.AddItemToCart(cartItem);
        }
        public Task<bool> IsItemAvailable(string productCode)
        {
            return _repository.IsItemAvailable(productCode);
        }
        public Task<bool> ProcessPayment(CartItem cartItem)
        {
            return _repository.ProcessPayment(cartItem);
        }
        public Task<bool> SendConfirmation(CartItem cartItem)
        {
            return _repository.SendConfirmation(cartItem);
        }
        public Task<bool> UpdateStock(CartItem cartItem)
        {
            return _repository.UpdateStock(cartItem);
        }
    }
}
