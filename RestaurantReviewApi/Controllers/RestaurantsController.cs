using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantReviewApi.Data;
using RestaurantReviewApi.Models;

namespace RestaurantReviewApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly RestaurantDbContext _db;

    public RestaurantsController(RestaurantDbContext db)
    {
        _db = db;
    }

    // GET: api/restaurants
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
    {
        var restaurants = await _db.Restaurants.AsNoTracking().OrderBy(r => r.Id).ToListAsync();
        return Ok(restaurants);
    }

    // GET: api/restaurants/1
    [HttpGet("{id:int}", Name = nameof(GetRestaurantById))]
    public async Task<ActionResult<Restaurant>> GetRestaurantById(int id)
    {
        var restaurant = await _db.Restaurants.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        if (restaurant is null) return NotFound();
        return Ok(restaurant);
    }

    // POST: api/restaurants
    [HttpPost]
    public async Task<ActionResult<Restaurant>> AddRestaurant([FromBody] Restaurant restaurant)
    {
        restaurant.Id = 0;
        _db.Restaurants.Add(restaurant);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurant.Id }, restaurant);
    }

    // PUT: api/restaurants/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] Restaurant input)
    {
        var restaurant = await _db.Restaurants.FirstOrDefaultAsync(r => r.Id == id);
        if (restaurant is null) return NotFound();

        restaurant.Name = input.Name;
        restaurant.Location = input.Location;
        restaurant.CuisineType = input.CuisineType;
        restaurant.Rating = input.Rating;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/restaurants/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRestaurant(int id)
    {
        var restaurant = await _db.Restaurants.FirstOrDefaultAsync(r => r.Id == id);
        if (restaurant is null) return NotFound();

        _db.Restaurants.Remove(restaurant);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    // POST: api/restaurants/1/reviews
    [HttpPost("{id:int}/reviews")]
    public async Task<ActionResult<Restaurant>> AddReview(int id, [FromBody] ReviewRequest request)
    {
        var restaurant = await _db.Restaurants.FirstOrDefaultAsync(r => r.Id == id);
        if (restaurant is null) return NotFound();
        if (string.IsNullOrWhiteSpace(request.Text))
            return BadRequest(new { message = "Review text is required." });

        // Assign a new list instance so EF Core detects the converted property change.
        restaurant.Reviews = restaurant.Reviews.Append(request.Text.Trim()).ToList();
        await _db.SaveChangesAsync();
        return Ok(restaurant);
    }
}
