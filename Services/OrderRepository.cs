using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApiBakery.Data;
using TestApiBakery.Data.Repositories;

namespace TestApiBakery.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BakeryDbContext _context;

        public OrderRepository(BakeryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order)
        {


            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(a => a.Product)
                .FirstOrDefaultAsync(x => x.OrderId == id);
        }

        public async Task<IEnumerable<Order>> GetByNipAsync(string nip)
        {
            return await _context.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(a => a.Product)
                .Where(x => x.AppUser.Nip == nip)
                .ToListAsync(); 
        }

        public async Task<IEnumerable<Order>> GetByUserIdAsync(string id)
        {
            return await _context.Orders
               .Where(x => x.AppUserId == id)
               .ToListAsync();
        }

        public async Task<bool> OrderExistsAsync(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
            if (order != null)
            {
                return true;
            }
            return false;
        }

        public async Task RemoveAsync(Order order)
        {
            _context.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
