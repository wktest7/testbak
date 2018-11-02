using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApiBakery.Models;

namespace TestApiBakery.Services
{
    public interface IProductService
    {
        Task<ProductDto> GetByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetByCategoryAsync(string category);
        Task AddAsync(ProductDto productDto);
        Task UpdateAsync(int id, ProductDto productDto);
        Task RemoveAsync(int id);

    }
}
