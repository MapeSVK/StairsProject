using System.Collections.Generic;
using StairsAndShit.Core.Entity;

namespace StairsAndShit.Core.ApplicationService
{
    public interface IProductService
    {
        //Create //POST
        Product CreateProduct(Product newProduct);
        
        //Read //GET
        Product GetProductById(int id);

        //Update //PUT
        Product UpdateProduct(Product productUpdate);
        
        //Delete //DELETE
        Product DeleteProduct(int id);
	    
	    // get all products after filter 
	    List<Product> ReadAllProducts(Filter filter);

    }
}