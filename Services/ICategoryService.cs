using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApiBakery.Models;

namespace TestApiBakery.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<string>> GetNamesAsync();
        Task AddAsync(CategoryDto productDto);
        Task UpdateAsync(int id, CategoryDto categoryDto);
        Task RemoveAsync(int id);
    }
}
