using Nanoservices.Entities;
using Nanoservices.Infrastructure.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanoservices.Infrastructure.Repositories
{
    public class CheckOutRepository : ICheckOutRepository
    {
        private readonly List<Customer> customers = new List<Customer>();
        private readonly List<Product> products = new List<Product>();
        private readonly List<CartItem> cartItems = new List<CartItem>();
        public CheckOutRepository()
        {
            customers.Add(new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = "Joydip",
                LastName = "Kanjilal",
                EmailAddress = "joydipkanjilal@yahoo.com"
            });

            customers.Add(new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = "Steve",
                LastName = "Smith",
                EmailAddress = "stevesmith@yahoo.com"
            });

            products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Code = "P0001",
                Name = "Lenovo Laptop",
                Quantity_In_Stock = 15,
                Unit_Price = 125000
            });

            products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Code = "P0002",
                Name = "DELL Laptop",
                Quantity_In_Stock = 25,
                Unit_Price = 135000
            });

            products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Code = "P0003",
                Name = "HP Laptop",
                Quantity_In_Stock = 20,
                Unit_Price = 115000
            });

            cartItems.Add(new CartItem()
            {
                Id = Guid.NewGuid(),
                Product_Id = products.Where(p => p.Code.Equals("P0001")).FirstOrDefault().Id,
                Customer_Id = customers.Where(c => c.FirstName == "Joydip").FirstOrDefault().Id,
                Number_Of_Items = 1,
                Item_Added_On = DateTime.Now
            });

            cartItems.Add(new CartItem()
            {
                Id = Guid.NewGuid(),
                Product_Id = products.Where(p => p.Code.Equals("P0003")).FirstOrDefault().Id,
                Customer_Id = customers.Where(c => c.FirstName == "Steve").FirstOrDefault().Id,
                Number_Of_Items = 10,
                Item_Added_On = DateTime.Now
            });

            cartItems.Add(new CartItem()
            {
                Id = Guid.NewGuid(),
                Product_Id = products.Where(p => p.Code.Equals("P0002")).FirstOrDefault().Id,
                Customer_Id = customers.Where(c => c.FirstName == "Joydip").FirstOrDefault().Id,
                Number_Of_Items = 5,
                Item_Added_On = DateTime.Now
            });
        }
        public Task<List<CartItem>> GetAllCartItems()
        {
            return Task.FromResult(cartItems);
        }
        public Task<List<Product>> GetAllProducts()
        {
            return Task.FromResult(products);
        }
        public Task<List<Customer>> GetAllCustomers()
        {
            return Task.FromResult(customers);
        }
        public Task<bool> AddItemToCart(CartItem cartItem)
        {
            cartItems.Add(cartItem);
            return Task.FromResult(true);
        }
        public Task<bool> IsItemAvailable(string productCode)
        {
            var p = products.Find(p => p.Code.Trim().Equals(productCode));

            if (p != null)
                return Task.FromResult(true);
            return Task.FromResult(false);
        }
        public Task<bool> ProcessPayment(CartItem cartItem)
        {
            var product = products.Where(c => c.Id == cartItem.Product_Id).FirstOrDefault();
            var totalPrice = product.Unit_Price * cartItem.Number_Of_Items;
            //Write code here to process payment for the purchase
            return Task.FromResult(true);
        }
        public Task<bool> SendConfirmation(CartItem cartItem)
        {
            var customer = customers.Where(c => c.Id == cartItem.Customer_Id).FirstOrDefault();
            //Write code here to send an email to the customer confirming the purchase
            return Task.FromResult(true);
        }
        public Task<bool> UpdateStock(CartItem cartItem)
        {
            var p = products.Find(p => p.Id == cartItem.Product_Id);
            p.Quantity_In_Stock--;
            return Task.FromResult(true);
        }
    }
}
