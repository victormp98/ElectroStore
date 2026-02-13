using System;
using System.Threading.Tasks;
using ElectroStore.Core.Entities;

namespace ElectroStore.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IGenericRepository<Category> Categories { get; }
        Task<int> CompleteAsync();
    }
}
