using System.Diagnostics;
using System.Transactions;

namespace StairsAndShit.Core.Entity
{
    public class OrderLine
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
        public int OrderId { get; set; }
        public Order Order { get; set; }
        
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}