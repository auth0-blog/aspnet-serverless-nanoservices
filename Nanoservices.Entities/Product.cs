using System;

namespace Nanoservices.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Quantity_In_Stock { get; set; }
        public decimal Unit_Price { get; set; }
    }
}
