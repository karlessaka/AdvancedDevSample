using AdvancedDevSample.Api.Middlewares;
using AdvancedDevSample.Application.Services;
using AdvancedDevSample.Domain.Interfaces;
using AdvancedDevSample.Domain.Interfaces.Products;
using AdvancedDevSample.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var basePath = AppContext.BaseDirectory;
    // On garde ta gestion des commentaires XML, c'est très bien !
    foreach (var xmlFile in Directory.GetFiles(basePath, "*.xml"))
    {
        options.IncludeXmlComments(xmlFile);
    }
});


builder.Services.AddScoped<ProductService>();

// ...
builder.Services.AddScoped<IProductRepositoryAsync, EfProductRepository>();

// NOUVEAU : Enregistrement pour les commandes
builder.Services.AddScoped<IOrderRepositoryAsync, EfOrderRepository>();
builder.Services.AddScoped<OrderService>();
// ...


builder.Services.AddScoped<IProductRepositoryAsync, EfProductRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseMiddleware<ExceptionHandLingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();