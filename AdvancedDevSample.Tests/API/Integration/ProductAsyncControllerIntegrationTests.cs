using AdvancedDevSample.Application.DTOs;
using AdvancedDevSample.Domain.Entyties;
using AdvancedDevSample.Domain.Interfaces.Products;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit; 

namespace AdvancedDevSample.Tests.API.Integration
{
    public class ProductAsyncControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly InMemoryProductRepositoryAsync _repo;

        public ProductAsyncControllerIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
            // On récupère le repository en mémoire pour vérifier les données directement
            _repo = (InMemoryProductRepositoryAsync)factory.Services.GetRequiredService<IProductRepositoryAsync>();
        }

        [Fact]
        public async Task CreateProduct_Should_Return_Created()
        {
            // Arrange
            var request = new CreateProductRequest
            {
                Name = "Souris Gamer",
                Price = 50,
                StockQuantity = 100
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/products", request);

            // Assert
            response.EnsureSuccessStatusCode(); // Vérifie que c'est 2xx
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task ChangePrice_Should_Update_Price_In_Database()
        {
            // Arrange - On prépare un produit existant
            var productId = Guid.NewGuid();
            var product = new Product(productId, "Clavier Mécanique", 100, 20, true);

            // On l'ajoute directement dans le "Faux" repository
            await _repo.AddAsync(product);

            var updateRequest = new ChangePriceRequest { NewPrice = 80 };

            // Act - On appelle l'API pour changer le prix
            var response = await _client.PutAsJsonAsync(
                $"/api/products/{productId}/price",
                updateRequest
            );

            // Assert - Vérification HTTP
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            // Assert - Vérification Persistance (Est-ce que le prix a vraiment changé ?)
            var updatedProduct = await _repo.GetByIdAsync(productId);
            Assert.Equal(80, updatedProduct!.Price);
        }
    }
}