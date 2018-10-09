using Microsoft.EntityFrameworkCore;
using StairsAndShit.Core.Entity;

namespace StairsAndShit.Infrastructure.Data
{
	public class StairsAppContext : DbContext
	{
		public StairsAppContext(DbContextOptions<StairsAppContext> opt) 
			: base(opt) { }

		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			
			/* MANY TO MANY RELATIONSHIP */
			
			modelBuilder.Entity<OrderLine>().HasKey(ol => new { ol.ProductId, ol.OrderId });

			modelBuilder.Entity<OrderLine>()
				.HasOne<Order>(ol => ol.Order)
				.WithMany(o => o.OrderLines)
				.HasForeignKey(sc => sc.OrderId);


			modelBuilder.Entity<OrderLine>()
				.HasOne<Product>(ol => ol.Product)
				.WithMany(o => o.OrderLines)
				.HasForeignKey(sc => sc.ProductId);
	
		}

		public DbSet<Order> Orders { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<OrderLine> OrderLines { get; set; }
	}
}
