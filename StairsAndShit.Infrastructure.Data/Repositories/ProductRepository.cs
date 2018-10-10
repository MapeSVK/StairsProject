using System;
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
			_stairsAppContext.SaveChanges();
			return updatedProduct;
		}
		
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
		
		public int Count()
		{
			return _stairsAppContext.Products.Count();
		}

		public IEnumerable<Product> ReadAllProducts(Filter filter)
		{
			if (filter.ItemsPrPage > 0&& filter.CurrentPage>0)
			{
				return _stairsAppContext.Products
					.Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
					.Take(filter.ItemsPrPage)
					.OrderBy(p => p.Name);				
			}
			return _stairsAppContext.Products;					
		}
	}
}
