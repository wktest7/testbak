using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApiBakery.Data;
using TestApiBakery.Data.Repositories;
using TestApiBakery.Models;

namespace TestApiBakery.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        //GET: api/Products
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("ss");
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<Product, ProductDto>(product));
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> Get(string category)
        {
            var products = await _productRepository.GetByCategory(category);
            if (products.Count() == 0)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products));
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _productRepository.CategoryExistsAsync(productDto.CategoryId))
            {
                return NotFound();
            }

            var product = Mapper.Map<ProductDto, Product>(productDto);
            await _productRepository.AddAsync(product);
            return StatusCode(201);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var x = await _productRepository.GetByIdAsync(id);
           

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            if (!await _productRepository.CategoryExistsAsync(productDto.CategoryId))
            {
                return BadRequest();
            }

            product = Mapper.Map<ProductDto, Product>(productDto, product);
            product.ProductId = id;
            
            
            await _productRepository.UpdateAsync(product);

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productRepository.RemoveAsync(product);
            
            return NoContent();
        }
    }
}
