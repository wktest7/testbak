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
        Task<IEnumerable<ProductDto>> GetAllAsync(bool includeHiddenProducts);
        Task<IEnumerable<ProductDto>> GetByCategoryAsync(string category);
        Task AddAsync(ProductAddDto productAddDto);//to add new dto
        Task UpdateAsync(ProductUpdateDto productUpdateDto);
        //Task RemoveAsync(int id);

    }
}
