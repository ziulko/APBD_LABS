using Microsoft.AspNetCore.Mvc;
using WebApplicationzad10.Data;
using WebApplicationzad10.Models;

namespace WebApplicationzad10.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var product = new Product
        {
            Name = request.ProductName,
            Weight = request.ProductWeight,
            Width = request.ProductWidth,
            Height = request.ProductHeight,
            Depth = request.ProductDepth
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        foreach (var categoryId in request.ProductCategories)
        {
            _context.ProductCategories.Add(new ProductCategory
            {
                ProductId = product.ProductId,
                CategoryId = categoryId
            });
        }

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { productId = product.ProductId }, product);
    }

    [HttpGet("{productId:int}")]
    public async Task<IActionResult> GetProduct(int productId)
    {
        var product = await _context.Products.FindAsync(productId);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }
}