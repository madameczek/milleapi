using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using milleapi.App.Interfaces;
using milleapi.App.Persistence;
using milleapi.App.Persistence.DbContexts;
using milleapi.App.Services;
using milleapi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddXmlDataContractSerializerFormatters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MVC WebAPI", Version = "v1" });
});

builder.Services.AddDbContext<CustomerDbContext>(o => o.UseSqlite(
    builder.Configuration["ConnectionStrings:DishesDBConnectionString"]));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

// Recreate and migrate DB on each run
// TODO remove on production
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
    context.Database.EnsureDeleted();
    context.Database.Migrate();
}

app.Run();