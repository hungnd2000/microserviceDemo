using Manonero.MessageBus.Kafka.Extensions;
using Product.API.Application.Repositories;
using Product.API.Application.Services;
using Product.API.BackgroundTasks;
using Product.API.Data;
using Product.API.Extensions;
using Product.API.Repositories;
using Product.API.Settings;

var builder = WebApplication.CreateBuilder(args);
var ConsumerId = builder.Configuration.GetSection("ConsumerSettings:0:Id").Value;
var appSetting = AppSetting.MapValue(builder.Configuration);

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
builder.Services.AddKafkaConsumers(ConsumerBuilder =>
{
    ConsumerBuilder.AddConsumer<ConsumerBackgroundTask>(appSetting.GetConsumerSetting(ConsumerId));
});

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

app.UseKafkaMessageBus(messageBus =>
{
    messageBus.RunConsumer(ConsumerId);
}
);

app.Run();
