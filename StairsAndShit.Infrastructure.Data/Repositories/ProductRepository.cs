using System.Collections.Generic;
using StairsAndShit.Core.DomainService;
using StairsAndShit.Core.Entity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
			_stairsAppContext.Attach(newProduct).State = EntityState.Added;
			_stairsAppContext.SaveChanges();
			return newProduct;
		}

		public Product RemoveProduct(int id)
		{
			var removed = _stairsAppContext.Remove(new Product {Id = id}).Entity;
			_stairsAppContext.SaveChanges();
            return removed;
		}
		
		
		public Product UpdateProduct(Product updatedProduct)
		{
			_stairsAppContext.Attach(updatedProduct).State = EntityState.Modified;
			//_stairsAppContext.Entry(updatedProduct).Reference(p => p.Owner).IsModified = true;
			_stairsAppContext.SaveChanges();
			return updatedProduct;
		}

		
		// returns Product with id specified in API
		public Product GetProductById(int id)
		{
			foreach (var Product in _stairsAppContext.Products)
			{
				if (Product.Id == id)
				{
					return Product;
				}
			}		
			return null;		
		}
		

		// counts how many products in DbSet we have
		public int Count()
		{
			return _stairsAppContext.Products.Count();
		}
		
		/*
			Read all products and filter (set how many per page)
			Order products by name
		*/
		public IEnumerable<Product> ReadAllProducts(Filter filter)
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
	}
}
