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
        public DbSet<Products> Products { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>().ToTable("categories", schema: "production");
			modelBuilder.Entity<Products>().ToTable("products", schema: "production");
			// Other configurations
		}

	}
}
