using BikeStoreWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStoreWeb.Data
{
	public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>().ToTable("categories", schema: "production");
			// Other configurations
		}

	}
}
