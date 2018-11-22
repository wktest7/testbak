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

        public async Task AddAsync(ProductAddDto productAddDto)
        {
            if (!await _categoryRepository.CategoryExistsAsync(productAddDto.CategoryId))
            {
                throw new Exception($"Category with id: '{productAddDto.CategoryId}' not exists.");
            }

            var product = _mapper.Map<ProductAddDto, Product>(productAddDto);
            await _productRepository.AddAsync(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(bool includeHiddenProducts)
        {
            if (includeHiddenProducts)
            {
                var products = await _productRepository.GetAllAsync(true);
                return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            }
            else
            {
                var products = await _productRepository.GetAllAsync(false);
                return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            } 
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

        public async Task UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var product = await _productRepository.GetByIdAsync(productUpdateDto.ProductId, false);
            if (product == null)
            {
                throw new Exception($"Product with id: '{productUpdateDto.ProductId}' not exists.");
            }

            if (!await _categoryRepository.CategoryExistsAsync(productUpdateDto.CategoryId))
            {
                throw new Exception($"Category with id: '{productUpdateDto.CategoryId}' not exists.");
            }

            product = _mapper.Map<ProductUpdateDto, Product>(productUpdateDto, product);
            //product.ProductId = productEditDto.ProductId;
            await _productRepository.UpdateAsync(product);
        }
    }
}
