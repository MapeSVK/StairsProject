using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StairsAndShit.Core.ApplicationService;
using StairsAndShit.Core.Entity;

namespace StairsAndShit.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
	    private readonly IProductService _productService;

	    public ProductsController(IProductService productService)
	    {
		    _productService = productService;
	    }

	    // GET api/values
	    // get all filtered products with paging and ordered by name
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get([FromQuery] Filter filter)
        {
	        try
	        {
				return Ok(_productService.ReadAllProducts(filter));
	        }
	        catch (Exception e)
	        {
		        return BadRequest(e.Message);
	        }
        }

        // GET api/values/5
	    // get specific pet by id
	    [HttpGet("{id}")]
	    public ActionResult<Product> Get(int id)
	    {
		    if (id < 1) return BadRequest("Id must be greater then 0");
		    return _productService.GetProductById(id);
	    }

        // POST api/values
	    public ActionResult<Product> Post([FromBody] Product newProduct)
	    {
		    if (string.IsNullOrEmpty(newProduct.Name))
		    {
			    return BadRequest("Set the product name");
		    }

		    if (char.ToUpper(newProduct.Type) != 'N' || char.ToUpper(newProduct.Type) != 'O' || 
		        char.ToUpper(newProduct.Type) != 'S')
		    {
			    return BadRequest("You need to set type of product which can be 'N' (New)," +
			                      "'O' (Normal), or 'S' (Sale) ");
		    }
		    if (string.IsNullOrEmpty(newProduct.Desc))
		    {
			    return BadRequest("Write description of the product");
		    }
		    if (!double.IsNegative(newProduct.Price) == true)
		    {
			    return BadRequest("set price of the product. Price cannot be negative");
		    }

		    return _productService.CreateProduct(newProduct);
	    }

        // PUT api/values/5
	    [HttpPut("{id}")]
	    public ActionResult<Product> Put(int id, [FromBody] Product product)
	    {
	        if (id < 1 || id != product.Id)
	        {
		        return BadRequest("The ID of the pet is not correct!");
	        }
		    if (string.IsNullOrEmpty(product.Name))
		    {
			    return BadRequest("Set the product name");
		    }

		    if (char.ToUpper(product.Type) != 'N' || char.ToUpper(product.Type) != 'O' || 
		        char.ToUpper(product.Type) != 'S')
		    {
			    return BadRequest("You need to set type of product which can be 'N' (New)," +
			                      "'O' (Normal), or 'S' (Sale) ");
		    }
		    if (string.IsNullOrEmpty(product.Desc))
		    {
			    return BadRequest("Write description of the product");
		    }
		    if (!double.IsNegative(product.Price) == true)
		    {
			    return BadRequest("set price of the product. Price cannot be negative");
		    }
	        return Ok(_productService.UpdateProduct(product));
        }

        // DELETE api/values/5
	    [HttpDelete("{id}")]
	    public ActionResult<Product> Delete(int id)
	    {
		    var product = _productService.DeleteProduct(id);
		
		    if (product == null)
		    {
			    return StatusCode(404, "Could not find a product with this ID: " + id);
		    }

		    return Ok($"Product with Id: {id} is deleted");
	    }
    }
}