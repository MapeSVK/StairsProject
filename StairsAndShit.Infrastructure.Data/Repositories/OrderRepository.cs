using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StairsAndShit.Core.DomainService;
using StairsAndShit.Core.Entity;


namespace StairsAndShit.Infrastructure.Data
{
	public class OrderRepository : IOrderRepository
	{
		readonly StairsAppContext _stairsAppContext;

		public OrderRepository(StairsAppContext sac)
		{
			_stairsAppContext = sac;
		}
		
		public IEnumerable<Order> ReadAllPets()
		{
			return _stairsAppContext.Orders;
		}

		/*public Order GetOrderById(int id)
		{
			// return _stairsAppContext.Orders.Include(o => o.Product).FirstOrDefault(o => o.);
			throw new System.NotImplementedException();
		}*/

		public Order RemoveOrder(int id)
		{
			throw new System.NotImplementedException();
		}

		public Order CreateOrder(Order newOrder)
		{
			throw new System.NotImplementedException();
		}
	}
}
