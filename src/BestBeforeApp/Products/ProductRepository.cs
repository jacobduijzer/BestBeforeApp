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

        public async Task<int> Add(Product entity)
        {
            await _appDbContext.Products.AddAsync(entity).ConfigureAwait(false);
            await _appDbContext.SaveChangesAsync().ConfigureAwait(false);

            return entity.Id;
        }

        public async Task<IEnumerable<Product>> Get(Expression<Func<Product, bool>> expression) =>
            await _appDbContext.Products
                .Where(expression)
                .OrderBy(x => x.BestBefore) // TODO: parameterize!
                .ToListAsync()
                .ConfigureAwait(false);

        public async Task<Product> GetById(int id) =>
            await _appDbContext.Products
                .FirstOrDefaultAsync(x => x.Id.Equals(id))
                .ConfigureAwait(false);

        public async Task Delete(Product product)
        {
            _appDbContext.Products.Remove(product);
            await _appDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

    }
}
