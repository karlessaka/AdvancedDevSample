using AdvancedDevSample.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdvancedDevSample.Domain.Entyties
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid ClientId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public decimal TotalAmount { get; private set; }

        // Liste des lignes de commande
        private readonly List<OrderItem> _items = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        public Order(Guid id, Guid clientId)
        {
            Id = id;
            ClientId = clientId;
            OrderDate = DateTime.UtcNow;
            TotalAmount = 0;
        }

        // Ajouter un produit à la commande
        public void AddItem(Product product, int quantity)
        {
            if (quantity <= 0) throw new DomaineException("La quantité doit être positive.");
            if (product.StockQuantity < quantity) throw new DomaineException($"Stock insuffisant pour le produit {product.Name}");

            var item = new OrderItem(product.Id, product.Name, product.Price, quantity);
            _items.Add(item);

            // Recalculer le total
            RecalculateTotal();
        }

        private void RecalculateTotal()
        {
            TotalAmount = _items.Sum(i => i.TotalPrice);
        }
    }

    // Objet valeur (Value Object) pour représenter une ligne de commande
    public class OrderItem
    {
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalPrice => UnitPrice * Quantity;

        public OrderItem(Guid productId, string productName, decimal unitPrice, int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
}
