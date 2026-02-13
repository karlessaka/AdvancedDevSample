
using AdvancedDevSample.Domain.Entyties;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdvancedDevSample.Domain.Interfaces.Products
{
    // Interface Synchrone (ancienne, on la garde juste pour pas casser le vieux code si besoin)
    public interface IProductRepository
    {
        Product GetById(Guid id);
        void Save(Product product);
    }

    // Interface Asynchrone (Celle qu'on utilise maintenant)
    public interface IProductRepositoryAsync
    {
        Task<Product> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync(); 

        Task AddAsync(Product product);           
        Task UpdateAsync(Product product);        

        Task DeleteAsync(Guid id);                
    }
}