using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedDevSample.Infrastructure.Entities
{
    public class ProductEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
    }
}
