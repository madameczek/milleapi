using Domain.Entities;
using FluentValidation;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApi.Behaviors;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddXmlDataContractSerializerFormatters();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MVC WebAPI", Version = "v1" });
    var filePath = Path.Combine(AppContext.BaseDirectory, "WebApi.xml");
    if (File.Exists(filePath))
        c.IncludeXmlComments(filePath);
});

builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=app.db"));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    context.Database.EnsureDeleted();
    context.Database.Migrate();

    if (!context.Customers.Any())
    {
        var customers = new[]
        {
            new Customer
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            },
            new Customer
            {
                FirstName = "Adam",
                LastName = "Adamski",
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            },
            new Customer
            {
                FirstName = "Marcin",
                LastName = "Nowak",
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            }
        };

        context.Customers.AddRange(customers);
        context.SaveChanges();

        var orders = new[]
        {
            new Order
            {
                OrderNumber = "Order-001",
                TotalAmount = 150.50m,
                OrderDate = DateTime.UtcNow.AddDays(-5),
                CustomerId = 1
            },
            new Order
            {
                OrderNumber = "Order-002",
                TotalAmount = 299.99m,
                OrderDate = DateTime.UtcNow.AddDays(-3),
                CustomerId = 1
            },
            new Order
            {
                OrderNumber = "Order-003",
                TotalAmount = 75.25m,
                OrderDate = DateTime.UtcNow.AddDays(-2),
                CustomerId = 2
            },
            new Order
            {
                OrderNumber = "Order-004",
                TotalAmount = 425.00m,
                OrderDate = DateTime.UtcNow.AddDays(-1),
                CustomerId = 3
            }
        };

        context.Orders.AddRange(orders);
        context.SaveChanges();
    }
}

app.Run();