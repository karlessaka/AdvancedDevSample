using AdvancedDevSample.Domain.Entyties;
using AdvancedDevSample.Domain.Interfaces.Products;
using System;
using AdvancedDevSample.Application.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace AdvancedDevSample.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepositoryAsync _repository;

        public ProductService(IProductRepositoryAsync repository)
        {
            _repository = repository;
        }


        // 1. GET ALL
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _repository.GetAllAsync();
        }

        // 2. GET BY ID
        public async Task<Product> GetProductAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) throw new Exception("Product not found"); // Idéalement, utilise une exception personnalisée NotFoundException
            return product;
        }

        // 3. CREATE
        public async Task<Guid> CreateProductAsync(CreateProductRequest request)
        {
            // Création de l'entité Domain
            var product = new Product(
                Guid.NewGuid(),
                request.Name,
                request.Price,
                request.StockQuantity,
                true // Actif par défaut
            );

            await _repository.AddAsync(product);
            return product.Id;
        }

        // 4. UPDATE PRICE (Exemple de modification spécifique)
        public async Task ChangeProductPriceAsync(Guid productId, decimal newPrice)
        {
            var product = await GetProductAsync(productId); // Récupère et vérifie l'existence
            product.ChangePrice(newPrice);
            await _repository.UpdateAsync(product);
        }

        // 5. DELETE
        public async Task DeleteProductAsync(Guid productId)
        {
            // On vérifie juste si le produit existe avant de supprimer
            var product = await GetProductAsync(productId);
            await _repository.DeleteAsync(product.Id);
        }



       /* public void ChangeProductPrice(Guid productId, decimal newPrice)
        {
            var product = GetProduct(productId);
            product.ChangePrice(newPrice);
            _repository.Save(product);
        }

        public void ApplyProductDiscount(Guid productId, decimal discount) 
        {
            var product = GetProduct(productId);
            product.Activate();
            product.ApplyDiscount(discount);
            _repository.Save(product);
        }

        public void ActivateProduct(Guid produtId)
        {
            var product = GetProduct(produtId);
            product.Activate();
            _repository.Save(product);
        }

        public void DesactivateProduct(Guid productId)
        {
            var product = GetProduct(productId);
            product.Deactivate();
            _repository.Save(product);
        }

        public Product GetProduct(Guid productId)
        {
            return _repository.GetById(productId)
                ?? throw new Exception("Product not found");
        }*/
            
    }
}
