using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApiBakery.Data;
using TestApiBakery.Data.Repositories;
using TestApiBakery.Models;

namespace TestApiBakery.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly BakeryDbContext _context;

        public ProductRepository(BakeryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetByCategory(string category)
        {
            return await _context.Products
                .Include(x => x.Category)
                .Where(x => x.Category.Name.Equals(category))
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(x => x.Category)
                .Where(x => x.ProductId == id)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Product product)
        {
            _context.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (category != null)
            {
                return true;
            }
            return false;
        }
    }
}
