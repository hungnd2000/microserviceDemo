using Manonero.MessageBus.Kafka.Extensions;
using Order.API.Application.Repositories;
using Order.API.Application.Services;
using Order.API.Database;
using Order.API.Repositories;
using Order.API.Repository;
using Order.API.Services;
using Order.API.Settings;
using static Confluent.Kafka.ConfigPropertyNames;

var builder = WebApplication.CreateBuilder(args);
var ProducerId = builder.Configuration.GetSection("ProducerSettings:0:Id").Value;
var appSetting = AppSetting.MapValue(builder.Configuration);

// Add services to the container.
builder.Services.AddOracle<OrderDbContext>(builder.Configuration.GetConnectionString("OracleConnection"));
builder.Services.AddControllers();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddHttpClient();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddKafkaProducers(producerBuilder =>
{
    producerBuilder.AddProducer(appSetting.GetProducerSetting(ProducerId));
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

app.UseKafkaMessageBus(messageBus =>
{
    messageBus.RunConsumer(ProducerId);
});

app.Run();
