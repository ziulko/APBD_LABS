using System.ComponentModel.DataAnnotations;
namespace WebApplicationzad10.Models;

public class AddProductRequest
{
    [Required]
    public string ProductName { get; set; }
    
    [Required]
    public decimal ProductWeight { get; set; }

    [Required]
    public decimal ProductWidth { get; set; }

    [Required]
    public decimal ProductHeight { get; set; }

    [Required]
    public decimal ProductDepth { get; set; }

    [Required]
    public List<int> ProductCategories { get; set; }
}