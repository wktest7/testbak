using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApiBakery.Models;

namespace TestApiBakery.Data.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id, bool includeCategory = true);
        Task<IEnumerable<Product>> GetByCategoryAsync(string category);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        //Task RemoveAsync(Product product);
        //Task HideAsync(Product product);

    }
}
