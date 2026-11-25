using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WarehouseInventory.API.Data;
using WarehouseInventory.API.DTOs;
using WarehouseInventory.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(o=>
{
    o.CreateMap<Category,CategoryDTO>().ReverseMap();
    o.CreateMap<CreateCategoryDTO,CategoryDTO>().ReverseMap();
    o.CreateMap<UpdateCategoryDTO,CategoryDTO>().ReverseMap();
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

//Apply database migrations
await ApplyDatabaseMigrationsAsync(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static async Task ApplyDatabaseMigrationsAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();
}