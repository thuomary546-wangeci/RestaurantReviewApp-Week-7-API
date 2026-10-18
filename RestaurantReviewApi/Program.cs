using Microsoft.EntityFrameworkCore;
using RestaurantReviewApi.Data;
using RestaurantReviewApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("RestaurantDatabase")
        ?? "Data Source=restaurants.db"));

var app = builder.Build();

// Create the SQLite database and seed sample records on first startup.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RestaurantDbContext>();
    await db.Database.EnsureCreatedAsync();

    if (!await db.Restaurants.AnyAsync())
    {
        db.Restaurants.AddRange(
            new Restaurant { Name = "Savanna", Location = "San Diego, CA", CuisineType = "African", Rating = 4.5, Reviews = new() { "Friendly service", "Great food" } },
            new Restaurant { Name = "American Grill", Location = "San Diego, CA", CuisineType = "American", Rating = 4.0, Reviews = new() { "Good burgers" } },
            new Restaurant { Name = "Pasta House", Location = "San Diego, CA", CuisineType = "Italian", Rating = 4.3, Reviews = new() { "Tasty pasta" } }
        );
        await db.SaveChangesAsync();
    }
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
