using AdvancedDevSample.Domain.Entyties;
using AdvancedDevSample.Domain.Interfaces.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedDevSample.Tests.API.Integration
{
    public class InMemoryProductRepositoryAsync : IProductRepositoryAsync
    {
        private readonly Dictionary<Guid, Product> _store = new();

        public Task<Product> GetByIdAsync(Guid id)
        {
            _store.TryGetValue(id, out var product);
            return Task.FromResult(product);
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_store.Values.AsEnumerable());
        }

        public Task AddAsync(Product product)
        {
            _store[product.Id] = product;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Product product)
        {
            // On écrase l'ancienne version par la nouvelle
            if (_store.ContainsKey(product.Id))
            {
                _store[product.Id] = product;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            _store.Remove(id);
            return Task.CompletedTask;
        }

        // Méthode utilitaire pour les tests (hors interface)
        public void Seed(Product product) => _store[product.Id] = product;
    }
}