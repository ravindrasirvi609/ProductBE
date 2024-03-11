using Microsoft.EntityFrameworkCore;

namespace Products.Models
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Lbs;User Id=SA;Password=Popill786@;TrustServerCertificate=True");
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
