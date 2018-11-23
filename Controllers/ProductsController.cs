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

        [Authorize(Roles = "employee")]
        public async Task<IActionResult> Post([FromBody] ProductAddDto productAddDto)
        {
            if (productAddDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productService.AddAsync(productAddDto);
            return StatusCode(201);
        }
        
        [HttpPut]
        [Authorize(Roles = "employee")]
        public async Task<IActionResult> Put([FromBody] ProductUpdateDto productUpdateDto)
        {
            if (productUpdateDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            await _productService.UpdateAsync(productUpdateDto);

            return NoContent();
        }
    }
}
