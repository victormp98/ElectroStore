using ElectroStore.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectroStore.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsWithCategoryAsync();
        Task<Product> GetProductWithCategoryAsync(int id);
    }
}
