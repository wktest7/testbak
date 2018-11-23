using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiBakery.Data.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllAsync(); 
        Task<IEnumerable<Order>> GetByUserIdAsync(string id);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task RemoveAsync(Order order);
    }
}
