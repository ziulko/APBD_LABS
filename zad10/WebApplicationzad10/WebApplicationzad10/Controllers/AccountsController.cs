using WebApplicationzad10.Data;

namespace WebApplicationzad10.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AccountsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{accountId:int}")]
    public async Task<IActionResult> GetAccount(int accountId)
    {
        var account = await _context.Accounts
            .Include(a => a.Role)
            .Include(a => a.ShoppingCarts)
            .ThenInclude(sc => sc.Product)
            .FirstOrDefaultAsync(a => a.AccountId == accountId);

        if (account == null)
        {
            return NotFound();
        }

        var response = new
        {
            firstName = account.FirstName,
            lastName = account.LastName,
            email = account.Email,
            phone = account.Phone,
            role = account.Role.Name,
            cart = account.ShoppingCarts.Select(sc => new
            {
                productId = sc.ProductId,
                productName = sc.Product.Name,
                amount = sc.Amount
            }).ToList()
        };

        return Ok(response);
    }
}