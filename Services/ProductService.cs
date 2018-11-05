using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApiBakery.Data;
using TestApiBakery.Data.Repositories;
using TestApiBakery.Models;

namespace TestApiBakery.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(ProductEditDto productEditDto)
        {
            if (!await _categoryRepository.CategoryExistsAsync(productEditDto.CategoryId))
            {
                throw new Exception($"Category with id: '{productEditDto.CategoryId}' not exists.");
            }

            var product = _mapper.Map<ProductEditDto, Product>(productEditDto);
            await _productRepository.AddAsync(product);
        }

        public async Task<IEnumerable<ProductDto>> GetByCategoryAsync(string category)
        {
            var products = await _productRepository.GetByCategoryAsync(category);
            if (products.Count() == 0)
            {
                throw new Exception($"Category with name: '{category}' not exists.");
            }
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new Exception($"Product with id: '{id}' not exists.");
            }
            return _mapper.Map<Product, ProductDto>(product);
        }

        //public async Task RemoveAsync(int id)
        //{
        //    var product = await _productRepository.GetByIdAsync(id);
        //    if (product == null)
        //    {
        //        throw new Exception($"Product with id: '{id}' not exists.");
        //    }

        //    await _productRepository.RemoveAsync(product);
        //}

        public async Task UpdateAsync(int id, ProductEditDto productEditDto)
        {
            var product = await _productRepository.GetByIdAsync(id, false);
            if (product == null)
            {
                throw new Exception($"Product with id: '{id}' not exists.");
            }

            if (!await _categoryRepository.CategoryExistsAsync(productEditDto.CategoryId))
            {
                throw new Exception($"Category with id: '{productEditDto.CategoryId}' not exists.");
            }

            product = _mapper.Map<ProductEditDto, Product>(productEditDto, product);
            product.ProductId = id;
            await _productRepository.UpdateAsync(product);
        }
    }
}
