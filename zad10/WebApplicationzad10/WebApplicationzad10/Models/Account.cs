namespace WebApplicationzad10.Models;


public class Account
{
    public int AccountId { get; set; }
    public int RoleId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public Role Role { get; set; }
    public ICollection<ShoppingCart> ShoppingCarts { get; set; }
}