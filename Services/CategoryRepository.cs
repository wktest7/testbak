using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApiBakery.Data;
using TestApiBakery.Data.Repositories;

namespace TestApiBakery.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BakeryDbContext _context;

        public CategoryRepository(BakeryDbContext context)
        {
            _context = context;
        }

        //public async Task<bool> CategoryExists(string category)
        //{
        //    return await _context.Categories.AnyAsync(x => x.Name == category);
        //}

        public async Task<IEnumerable<string>> GetAllNamesAnync()
        {
            return await _context.Categories.Select(x => x.Name).ToListAsync();
        }
    }
}
