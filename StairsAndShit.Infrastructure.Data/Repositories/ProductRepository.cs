using System.Collections.Generic;
using StairsAndShit.Core.DomainService;
using StairsAndShit.Core.Entity;
using System.Linq;

namespace StairsAndShit.Infrastructure.Data
{
	public class ProductRepository : IProductRepository
	{
		readonly StairsAppContext _stairsAppContext;

		public ProductRepository(StairsAppContext sac)
		{
			_stairsAppContext = sac;
		}
		
		public Product Create(Product newProduct)
		{
			throw new System.NotImplementedException();
		}

		public Product RemoveProduct(int id)
		{
			throw new System.NotImplementedException();
		}

		public Product UpdateProduct(Product updatedProduct)
		{
			throw new System.NotImplementedException();
		}

		public Product GetProductById(int id)
		{
			throw new System.NotImplementedException();
		}

		// counts how many products we have
		public int Count()
		{
			return _stairsAppContext.Products.Count();
		}

		/*
			Read all products and filter (set how many per page)
			Order products by name
		*/
		public IEnumerable<Product> ReadAllProducts(Filter filter = null)
		{
			if (filter == null)
			{
				return _stairsAppContext.Products;
			}

			return _stairsAppContext.Products
				.Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
				.Take(filter.ItemsPrPage)
				.OrderBy(p => p.Name);
		}

		public List<Product> GetFilteredProducts(Filter filter)
		{
			throw new System.NotImplementedException();
		}
	}
}
