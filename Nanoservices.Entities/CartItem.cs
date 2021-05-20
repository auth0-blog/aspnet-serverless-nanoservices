using System;

namespace Nanoservices.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid Product_Id { get; set; }
        public Guid Customer_Id { get; set; }
        public int Number_Of_Items { get; set; }
        public DateTime Item_Added_On { get; set; }
    }
}
