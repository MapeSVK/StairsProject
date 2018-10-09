using System.Collections.Generic;
using System.IO;
using System.Linq;
using StairsAndShit.Core.DomainService;
using StairsAndShit.Core.Entity;

namespace StairsAndShit.Core.ApplicationService.Impl
{
    public class ProductService
    {
	    readonly IProductRepository _productRepository;

	    public ProductService(IProductRepository productRepository)
	    {
		    _productRepository = productRepository;
	    }
	    
	    
	    // get products after filter applied
	    public List<Product> GetFilteredProducts(Filter filter)
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