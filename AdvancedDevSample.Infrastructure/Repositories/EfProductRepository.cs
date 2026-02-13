using AdvancedDevSample.Domain.Entyties;
using AdvancedDevSample.Domain.Interfaces.Products;
using AdvancedDevSample.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedDevSample.Infrastructure.Repositories
{
    public class EfProductRepository : IProductRepositoryAsync
    {
        public Product GetById(Guid id)
        {
            ProductEntity product = new() { Id = id, Price = 10, IsActive = true };
            var domainProduct = new Product (id: product.Id, price: product.Price, isActive: product.IsActive);

            return domainProduct;
        }

        public void Save(Product product) { }


        // Simulation d'une base de données (Liste statique pour garder les données tant que l'app tourne)
        private static readonly List<ProductEntity> _database = new List<ProductEntity>();

        public Task<Product> GetByIdAsync(Guid id)
        {
            var entity = _database.FirstOrDefault(p => p.Id == id);
            if (entity == null) return Task.FromResult<Product>(null);

            return Task.FromResult(MapToDomain(entity));
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = _database.Select(MapToDomain).ToList();
            return Task.FromResult<IEnumerable<Product>>(products);
        }

        public Task AddAsync(Product product)
        {
            var entity = MapToEntity(product);
            _database.Add(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Product product)
        {
            var existing = _database.FirstOrDefault(p => p.Id == product.Id);
            if (existing != null)
            {
                _database.Remove(existing); // On retire l'ancien
                _database.Add(MapToEntity(product)); // On met le nouveau
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var entity = _database.FirstOrDefault(p => p.Id == id);
            if (entity != null)
            {
                _database.Remove(entity);
            }
            return Task.CompletedTask;
        }

        // --- Mappers (Conversion Domain <-> Infrastructure) ---

        private Product MapToDomain(ProductEntity entity)
        {
            // On reconstruit l'objet Domain à partir de la BDD
            return new Product(entity.Id, entity.Name, entity.Price, entity.StockQuantity, entity.IsActive);
        }

        private ProductEntity MapToEntity(Product product)
        {
            // On transforme l'objet Domain en objet BDD
            return new ProductEntity
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                IsActive = product.IsActive
            };
        }
    }

}
