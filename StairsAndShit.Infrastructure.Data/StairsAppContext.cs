using Microsoft.EntityFrameworkCore;
using StairsAndShit.Core.Entity;

namespace StairsAndShit.Infrastructure.Data
{
	public class StairsAppContext : DbContext
	{
		public StairsAppContext(DbContextOptions<StairsAppContext> opt)
			: base(opt)
		{		
		}
		
		public DbSet<Product> Products { get; set; }
		public DbSet<User> Users { get; set; }
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}

		
	}
}
