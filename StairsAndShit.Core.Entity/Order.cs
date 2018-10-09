using System;
using System.Collections.Generic;

namespace StairsAndShit.Core.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public double FinalPrice { get; set; }
        
        public List<OrderLine> OrderLines { get; set; }
    }
}