﻿using System;
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
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}