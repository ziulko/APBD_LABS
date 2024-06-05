namespace WebApplicationzad10.Models;

public class ShoppingCart
{
    public int AccountId { get; set; }
    public Account Account { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Amount { get; set; }
}