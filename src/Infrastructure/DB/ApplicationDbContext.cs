using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DB
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext()
		{

		}
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<Product> Products { get; set; }
		public DbSet<ShoppingCart> ShoppingCarts { get; set; }
		public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder
					.UseSqlServer("Data Source=.;initial catalog=tot;User ID=sa;Password=P@$$w0rd;Connect Timeout=30;TrustServerCertificate=true");
			}

			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
