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
		    if (newProduct.Price <0.1)
		    {
			    throw new InvalidDataException("Price cannot be smaller than 0.1");
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

	    public Product UpdateProduct(Product productUpdate)
	    {
		    if (productUpdate.Id<1)
		    {
			    throw new InvalidDataException("Id cannot be smaller than 1");
		    }
		    if (productUpdate.Name == null)
		    {
			    throw new InvalidDataException("You need to specify products name");
		    }
		    if (productUpdate.Desc == null)
		    {
			    throw new InvalidDataException("You need to specify products description");
		    }
		    if (productUpdate.Price <0.1)
		    {
			    throw new InvalidDataException("Price cannot be smaller than 0.1");
		    }

		    var updatedProduct = _productRepository.UpdateProduct(productUpdate);

		    return updatedProduct;
	    }

	    public Product DeleteProduct(int id)
	    {
		    if (id<1)
		    {
			    throw new InvalidDataException("Id cannot be smaller than 1");
		    }
		    
		    var deletedProuct = _productRepository.RemoveProduct(id);
		    
		    return deletedProuct;
	    }
	    
	    public List<Product> ReadAllProducts(Filter filter)
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
	    }
    }
}