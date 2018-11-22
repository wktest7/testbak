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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<CategoryDto, Category>(categoryDto);
            await _categoryRepository.AddAsync(category);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(categories);
        }

        public async Task RemoveAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new Exception($"Category with id: '{id}' not exists.");
            }

            await _categoryRepository.RemoveAsync(category);
        }

        public async Task UpdateAsync(CategoryDto categoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryDto.CategoryId);
            if (category == null)
            {
                throw new Exception($"Category  with id: '{categoryDto.CategoryId}' not exists.");
            }
            category = _mapper.Map<CategoryDto, Category>(categoryDto, category);
            await _categoryRepository.UpdateAsync(category);
        }
    }
}
