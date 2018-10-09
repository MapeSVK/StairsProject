using System.Collections.Generic;
using StairsAndShit.Core.Entity;

namespace StairsAndShit.Core.ApplicationService
{
    public interface IOrderService
    {
        //Create //POST
        Order CreateOrder(Order newOrder);
        
        //Read //GET
        Order GetOrderById(int id);
        List<Order> GetAllOrders();
        
        //Update //PUT
        Order UpdateOrder(Order orderUpdate);
        
        //Delete //DELETE
        Order DeleteOrder(int id);
    }
}