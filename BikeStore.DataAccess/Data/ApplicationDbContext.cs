using BikeStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>().ToTable("categories", schema: "production");
			modelBuilder.Entity<Product>().ToTable("products", schema: "production");
			// Other configurations
		}

	}
}
