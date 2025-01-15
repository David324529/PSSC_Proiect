using Data;
using Data.Repositories;
using Domain.Workflows;
using Domain.Operations;         
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Api.Clients;
using Example.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurare conexiune la baza de date
builder.Services.AddDbContext<ShoppingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repozitorii
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();       // pentru facturi
builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();     // pentru livrari

// Operatii
builder.Services.AddScoped<ValidateOrderOperation>();
builder.Services.AddScoped<InvoiceOrderOperation>();
builder.Services.AddScoped<ShipOrderOperation>();
builder.Services.AddScoped<PublishOrderOperation>();
builder.Services.AddScoped<CalculateOrderOperation>();

// Inregistrare PaymentService
builder.Services.AddScoped<PaymentService>();  

// Workflow-uri
builder.Services.AddScoped<PlasareComandaWorkflow>();
builder.Services.AddScoped<FacturareWorkflow>();
builder.Services.AddScoped<LivrareWorkflow>();

// Configurare Controller JSON
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

//  Configurare Swagger pentru API Documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MagazinOnline.Api", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
