using System.Collections.Generic;
using StairsAndShit.Core.Entity;

namespace StairsAndShit.Core.DomainService
{
    public interface IProductRepository
    {
	    // create new product
	    Product Create(Product newProduct);
	    
	    // remove product from the list
	    Product RemoveProduct(int id);
	    
	    // update product
	    Product UpdateProduct(Product updatedProduct);

	    // get specific product by id
	    Product GetProductById(int id);
	    
	    // count how many products are in DbSet<Product>
	    // you can find DbSet in StairsAppContext.cs
	    int Count();
	    
	    // read all products
	    IEnumerable<Product> ReadAllProducts(Filter filter= null);
	    
	 


    }
}