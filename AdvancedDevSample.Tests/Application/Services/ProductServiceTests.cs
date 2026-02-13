using AdvancedDevSample.Domain.Entyties;
using AdvancedDevSample.Tests.Application.Fakes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedDevSample.Tests.Application.Services
{
    public class ProductServiceTests
    {
        [Fact]
        public void ChangeProductPrice_Should_Save_Product_When_Price_Is_Valid()
        {
            var product = new Product();
            product.ChangePrice(10);

            var repo = new FakeProductRepository(product);
        }
    }
}
