using Product.API.Application.Repositories;
using Product.API.Application.Services;
using Product.API.Data;
using Product.API.Extensions;
using Product.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddOracle<ProductDbContext>(builder.Configuration.GetConnectionString("OracleConnection"));
builder.Services.AddControllers();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, Product.API.Services.ProductService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ProductInMemoryContext>();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization(); 


app.MapControllers();

app.LoadDataToMemory<ProductInMemoryContext, ProductDbContext>((inMemoryContext, dbContext) =>
{
    new ProductInMemoryContextSeed().SeedAsync(inMemoryContext, dbContext).Wait();
});


app.Run();
