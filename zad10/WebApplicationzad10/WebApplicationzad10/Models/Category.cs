namespace WebApplicationzad10.Models;


public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; }
}