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

        public async Task<IEnumerable<string>> GetNamesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(x => x.Name);
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

        public async Task UpdateAsync(int id, CategoryDto categoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new Exception($"Category  with id: '{id}' not exists.");
            }

            category = _mapper.Map<CategoryDto, Category>(categoryDto, category);
            category.CategoryId = id;
            await _categoryRepository.UpdateAsync(category);
        }
    }
}
