using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApiBakery.Data;
using TestApiBakery.Models;
using TestApiBakery.Services;

namespace TestApiBakery.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "user")]
        [HttpGet("foruser")]
        public async Task<IActionResult> ProductsForUser()
        {
            var products = await _productService.GetAllAsync(false);
            return Ok(products);
        }

        [Authorize(Roles = "employee")]
        [HttpGet("foremployee")]
        public async Task<IActionResult> ProductsForEmployee()
        {
            var products = await _productService.GetAllAsync(true);
            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(product);
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> Get(string category)
        {
            var products = await _productService.GetByCategoryAsync(category);
            return Ok(products);
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductEditDto productEditDto)
        {
            if (productEditDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productService.AddAsync(productEditDto);
            return StatusCode(201);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductEditDto productEditDto)
        {
            if (productEditDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            await _productService.UpdateAsync(id, productEditDto);

            return NoContent();
        }

       
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _productService.RemoveAsync(id);
            
        //    return NoContent();
        //}
    }
}
