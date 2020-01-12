using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestBeforeApp.Products
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetProducts();
    }
}
