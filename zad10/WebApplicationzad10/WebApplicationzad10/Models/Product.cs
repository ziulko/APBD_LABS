namespace WebApplicationzad10.Models;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Weight { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Depth { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; }
    public ICollection<ShoppingCart> ShoppingCarts { get; set; }
}