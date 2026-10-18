using System.ComponentModel.DataAnnotations;

namespace RestaurantReviewApi.Models;

public class Restaurant
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Restaurant name is required.")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Location is required.")]
    public string Location { get; set; } = "";

    [Required(ErrorMessage = "Cuisine type is required.")]
    public string CuisineType { get; set; } = "";

    [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
    public double Rating { get; set; }

    public List<string> Reviews { get; set; } = new();
}
