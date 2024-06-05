namespace WebApplicationzad10.Models;

public class Role
{
    public int RoleId { get; set; }
    public string Name { get; set; }
    public ICollection<Account> Accounts { get; set; }
}