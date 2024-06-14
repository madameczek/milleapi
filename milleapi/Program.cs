using Microsoft.EntityFrameworkCore;
using milleapi;
using milleapi.App.DataSource;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CustomerDbContext>(o => o.UseSqlite(
    builder.Configuration["ConnectionStrings:DishesDBConnectionString"]));
builder.Services.AddScoped(typeof(ICustomerService), typeof(CustomerService));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

// Recreate and migrate DB on each run
// TODO remove on production
using (var scope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
    context.Database.EnsureDeleted();
    context.Database.Migrate();
}

app.Run();