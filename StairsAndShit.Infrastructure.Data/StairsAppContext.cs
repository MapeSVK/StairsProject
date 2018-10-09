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
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}

		public DbSet<Product> Products { get; set; }
		
	}
}
