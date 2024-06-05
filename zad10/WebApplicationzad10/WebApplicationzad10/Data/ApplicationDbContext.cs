using WebApplicationzad10.Models;

namespace WebApplicationzad10.Data;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Role> Roles { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProductCategory>()
            .HasKey(pc => new { pc.ProductId, pc.CategoryId });

        modelBuilder.Entity<ProductCategory>()
            .HasOne(pc => pc.Product)
            .WithMany(p => p.ProductCategories)
            .HasForeignKey(pc => pc.ProductId);

        modelBuilder.Entity<ProductCategory>()
            .HasOne(pc => pc.Category)
            .WithMany(c => c.ProductCategories)
            .HasForeignKey(pc => pc.CategoryId);

        modelBuilder.Entity<ShoppingCart>()
            .HasKey(sc => new { sc.AccountId, sc.ProductId });

        modelBuilder.Entity<ShoppingCart>()
            .HasOne(sc => sc.Account)
            .WithMany(a => a.ShoppingCarts)
            .HasForeignKey(sc => sc.AccountId);

        modelBuilder.Entity<ShoppingCart>()
            .HasOne(sc => sc.Product)
            .WithMany(p => p.ShoppingCarts)
            .HasForeignKey(sc => sc.ProductId);
    }
}