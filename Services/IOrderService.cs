using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApiBakery.Models;

namespace TestApiBakery.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetByUserIdAsync();
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task AddAsync(OrderAddDto orderAddDto);
        Task UpdateAsync(OrderUpdateDto orderUpdateDto);

    }
}
