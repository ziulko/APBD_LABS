using Microsoft.EntityFrameworkCore;


namespace WebAPI.Classes
{
    public class AnimalContext : DbContext
    {
        public AnimalContext(DbContextOptions<AnimalContext> options) : base(options)
        {
            
        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Visit> Visits { get; set; }
    }
}