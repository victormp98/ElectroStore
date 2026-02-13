using System.Threading.Tasks;
using ElectroStore.Core.Entities;
using ElectroStore.Core.Interfaces;
using ElectroStore.Infrastructure.Data;

namespace ElectroStore.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Products = new Repositories.ProductRepository(_context);
            Categories = new Repositories.GenericRepository<Category>(_context);
        }

        public IProductRepository Products { get; private set; }
        public IGenericRepository<Category> Categories { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
