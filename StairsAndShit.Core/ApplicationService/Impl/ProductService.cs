using System.Collections.Generic;
using System.IO;
using System.Linq;
using StairsAndShit.Core.DomainService;
using StairsAndShit.Core.Entity;

namespace StairsAndShit.Core.ApplicationService.Impl
{
    public class ProductService : IProductService
    {
	    readonly IProductRepository _productRepository;

	    public ProductService(IProductRepository productRepository)
	    {
		    _productRepository = productRepository;
	    }
	    
	    // get products after filter applied
	    public Product CreateProduct(Product newProduct)
	    {
		    if (newProduct.Name == null)
		    {
			    throw new InvalidDataException("You need to specify products name");
		    }
		    if (newProduct.Desc == null)
		    {
			    throw new InvalidDataException("You need to specify products description");
		    }
		    
		    var createdProduct =_productRepository.Create(newProduct);
		    
		    return createdProduct;
	    }

	    public Product GetProductById(int id)
	    {
		    if (id<1)
		    {
			    throw new InvalidDataException("Id cannot be smaller than 1"); 
		    }
		    return _productRepository.GetProductById(id);
	    }

	    public List<Product> GetAllProducts()
	    {
		    throw new System.NotImplementedException();
	    }

	    public Product UpdateProduct(Product productUpdate)
	    {
		    throw new System.NotImplementedException();
	    }

	    public Product DeleteProduct(int id)
	    {
		    throw new System.NotImplementedException();
	    }

	    /*public List<Product> GetFilteredProducts(Filter filter)
	    {
		    if (filter.CurrentPage < 0 || filter.ItemsPrPage < 0)
		    {
			    throw new InvalidDataException("CurrentPage and ItemsPage Must zero or more");
		    }
		    if((filter.CurrentPage -1 * filter.ItemsPrPage) >= _productRepository.Count())
		    {
			    throw new InvalidDataException("Index out bounds, CurrentPage is to high");
		    }
			
		    return _productRepository.ReadAllProducts(filter).ToList();
	    }*/
    }
}