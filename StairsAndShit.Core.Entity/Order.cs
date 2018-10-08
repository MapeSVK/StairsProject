using System;

namespace StairsAndShit.Core.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public double FinalPrice { get; set; }
    }
}