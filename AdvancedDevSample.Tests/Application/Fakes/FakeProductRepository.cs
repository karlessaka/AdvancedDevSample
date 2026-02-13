using AdvancedDevSample.Domain.Entyties;
using AdvancedDevSample.Domain.Interfaces.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedDevSample.Tests.Application.Fakes
{
    public class FakeProductRepository : IProductRepository
    {
        public bool WasSaved { get; private set; }
        private readonly Product _product;

        public FakeProductRepository(Product product)
        {
            _product = product;
        }

        public Product GetById(Guid id) => _product;

        public void Save(Product product) 
        { 
            WasSaved = true;
        }
    }
}
