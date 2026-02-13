using AdvancedDevSample.Application.DTOs;
using AdvancedDevSample.Domain.Entyties;
using AdvancedDevSample.Domain.Exceptions;
using AdvancedDevSample.Domain.Interfaces;
using AdvancedDevSample.Domain.Interfaces.Products;
using System;
using System.Threading.Tasks;

namespace AdvancedDevSample.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepositoryAsync _orderRepository;
        private readonly IProductRepositoryAsync _productRepository; // On a besoin des produits !

        public OrderService(IOrderRepositoryAsync orderRepository, IProductRepositoryAsync productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<Guid> PlaceOrderAsync(CreateOrderRequest request)
        {
            // 1. Récupérer le produit
            var product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product == null)
                throw new DomaineException("Produit introuvable.");

            if (!product.IsActive)
                throw new DomaineException("Ce produit n'est plus disponible à la vente.");

            // 2. Créer la commande
            var order = new Order(Guid.NewGuid(), request.ClientId);

            // 3. Tenter d'ajouter l'article (C'est ici que le Domain vérifie le stock !)
            // Si le stock est insuffisant, la méthode AddItem va lancer une exception (voir Order.cs et Product.cs)
            // ATTENTION : Il faut modifier un peu Order.cs pour qu'il appelle product.UpdateStock(-qte)
            // Pour l'instant, faisons simple :

            if (product.StockQuantity < request.Quantity)
                throw new DomaineException($"Stock insuffisant. Reste : {product.StockQuantity}");

            // 4. Décrémenter le stock (Logique métier)
            product.UpdateStock(-request.Quantity);

            // 5. Ajouter la ligne à la commande
            order.AddItem(product, request.Quantity);

            // 6. Sauvegarder TOUT
            await _productRepository.UpdateAsync(product); // Sauvegarder le nouveau stock
            await _orderRepository.AddAsync(order);        // Sauvegarder la commande

            return order.Id;
        }
    }
}