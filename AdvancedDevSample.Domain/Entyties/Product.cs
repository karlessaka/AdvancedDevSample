using AdvancedDevSample.Domain.Exceptions;


namespace AdvancedDevSample.Domain.Entyties
{
    public class Product
    {
        public Guid Id { get; private set; }
        public decimal Price { get; private set; }
        public bool IsActive { get; private set; }
        public string Name { get; private set; }
        public int StockQuantity { get; private set; }

        public Product()
        {
            IsActive = true;
        }

        public Product(Guid id, decimal price, bool isActive)
        {
            Id = id;
            Price = price;
            IsActive = isActive;
        }

        public Product(Guid id, string name, decimal price, int stockQuantity, bool isActive)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new DomaineException("Le nom du produit est obligatoire.");

            Id = id;
            Name = name;
            IsActive = isActive;
            ChangePrice(price);
            StockQuantity = stockQuantity;
        }

        public void ChangePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new DomaineException("Le prix doit etre positif");

            if (!IsActive)
                throw new DomaineException("Produit Incatif");

            Price = newPrice;
        }


        public void UpdateStock(int quantity)
        {
            // Si quantity est négatif, on retire du stock. Si positif, on ajoute.
            if (StockQuantity + quantity < 0)
                throw new DomaineException("Stock insuffisant pour effectuer cette opération.");

            StockQuantity += quantity;
        }

        public void ApplyDiscount(decimal discount)
        {
            ChangePrice(Price - discount);
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
