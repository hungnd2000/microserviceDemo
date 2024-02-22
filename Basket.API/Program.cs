using Basket.API.Application.Repositories;
using Basket.API.Application.Services;
using Basket.API.BackgroundTasks;
using Basket.API.Database;
using Basket.API.Repositories;
using Basket.API.Services;
using Basket.API.Settings;
using Manonero.MessageBus.Kafka.Extensions;

var builder = WebApplication.CreateBuilder(args);
var ConsumerId = builder.Configuration.GetSection("ConsumerSettings:0:Id").Value;
var appSetting = AppSetting.MapValue(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOracle<BasketDbContext>(builder.Configuration.GetConnectionString("OracleConnection"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICustomerBasketRepository, CustomerBasketRepository>();
builder.Services.AddScoped<ICustomerBasketService, CustomerBasketService>();
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
app.UseCors(builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
    );
app.MapControllers(); 
app.UseKafkaMessageBus(messageBus =>
{
    messageBus.RunConsumer(ConsumerId);
});

app.Run();
