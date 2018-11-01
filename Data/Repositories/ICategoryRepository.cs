using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiBakery.Data.Repositories
{
    public interface ICategoryRepository
    {
        //Task<bool> CategoryExists(string category);
        Task<IEnumerable<string>> GetAllNamesAnync();
    }
}
