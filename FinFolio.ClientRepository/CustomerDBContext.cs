using FinFolio.ClientRepository.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinFolio.ClientRepository
{
    internal class CustomerDBContext : DbContext
    {
        public CustomerDBContext(DbContextOptions<CustomerDBContext> options) : base(options)
        {

        }

        public DbSet<PortFolio> PortFolios { get; set; }
        public DbSet<PortFolioItem> PortFolioItems { get; set; }
    }
}
