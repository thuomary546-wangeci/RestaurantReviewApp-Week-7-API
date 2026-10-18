using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using RestaurantReviewApi.Models;

namespace RestaurantReviewApi.Data;

public class RestaurantDbContext : DbContext
{
    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options) { }

    public DbSet<Restaurant> Restaurants => Set<Restaurant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Store the list of review strings as JSON in a SQLite text column.
        var reviewsConverter = new ValueConverter<List<string>, string>(
            reviews => JsonSerializer.Serialize(reviews, (JsonSerializerOptions?)null),
            json => JsonSerializer.Deserialize<List<string>>(json, (JsonSerializerOptions?)null) ?? new List<string>());

        modelBuilder.Entity<Restaurant>()
            .Property(restaurant => restaurant.Reviews)
            .HasConversion(reviewsConverter);
    }
}
