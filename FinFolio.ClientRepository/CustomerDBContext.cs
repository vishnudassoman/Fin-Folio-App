using FinFolio.ClientRepository.Configuration;
using FinFolio.ClientRepository.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinFolio.ClientRepository
{
    public class CustomerDBContext : DbContext
    {
        public CustomerDBContext(DbContextOptions<CustomerDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PortFolioEntityTypeConfiguration).Assembly);
        }
        public DbSet<PortFolio> PortFolios { get; set; }
        public DbSet<PortFolioItem> PortFolioItems { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
    }
}
