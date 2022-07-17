using FinFolio.ClientRepository.Configuration;
using FinFolio.ClientRepository.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinFolio.ClientRepository
{
    public class CustomerDBContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CustomerDBContext(DbContextOptions<CustomerDBContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PortFolioEntityTypeConfiguration).Assembly);
            modelBuilder.Entity<PortFolioItem>()
                .Navigation(pf => pf.Scheme)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
            modelBuilder.Entity<Wishlist>()
                .Navigation(wl => wl.Scheme)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
            modelBuilder.Entity<PortFolioItem>()
               .Navigation(pf => pf.PortFolio)
               .UsePropertyAccessMode(PropertyAccessMode.Property);
            modelBuilder.Entity<PortFolio>()
              .Navigation(pf => pf.Items)
              .UsePropertyAccessMode(PropertyAccessMode.Property);
        }
        public DbSet<PortFolio> PortFolios { get; set; }
        public DbSet<PortFolioItem> PortFolioItems { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
        public DbSet<Scheme> Schemes { get; set; }
    }
}
