using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BestBeforeApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace BestBeforeApp.Products
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;

        public async Task Add(Product entity)
        {
            await _appDbContext.Products.AddAsync(entity).ConfigureAwait(false);
            await _appDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Product>> Get(Expression<Func<Product, bool>> expression) =>
            await _appDbContext.Products
                .Where(expression)
                .OrderBy(x => x.BestBefore) // TODO: parameterize!
                .ToListAsync()
                .ConfigureAwait(false);
    }
}
