using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApiBakery.Models;

namespace TestApiBakery.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task AddAsync(CategoryDto productDto);
        Task UpdateAsync(CategoryDto categoryDto);
        Task RemoveAsync(int id);
    }
}
