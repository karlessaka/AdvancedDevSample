using AdvancedDevSample.Domain.Interfaces.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedDevSample.Tests.API.Integration
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                //supprimer le crai repository si necessaire
                services.RemoveAll(typeof(IProductRepositoryAsync));

                //Ajouter un repository inMemory
                services.AddSingleton<IProductRepositoryAsync, InMemoryProductRepositoryAsync>();
            });
        }
    }
}
