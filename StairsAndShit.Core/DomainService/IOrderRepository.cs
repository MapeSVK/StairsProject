using System.Collections.Generic;
using StairsAndShit.Core.Entity;


namespace StairsAndShit.Core.DomainService
{
    public interface IOrderRepository
    {
	    //Read all Orders made by user/users in admin page
	    IEnumerable<Order> ReadAllPets();

	    /*//get ID of the order
	    Order GetOrderById(int id);*/
	    
	    //Delete Order
	    Order RemoveOrder(int id);
	    
	    //Create new Order. This order is created by user
	    Order CreateOrder(Order newOrder);
        
    }
}