using System.ComponentModel.DataAnnotations;

namespace RestaurantReviewApi.Models;

public class ReviewRequest
{
    [Required]
    [MinLength(1)]
    public string Text { get; set; } = "";
}
