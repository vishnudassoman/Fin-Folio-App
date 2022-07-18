using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FinFolio.PortFolioRepository
{
    public class PortFolioDBContextFactory : IDesignTimeDbContextFactory<PortFolioDBContext>
    {
        public PortFolioDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PortFolioDBContext>();
            optionsBuilder.UseSqlServer<PortFolioDBContext>("Data Source=.;Initial Catalog=Portfolio;Integrated Security=True");
            return new PortFolioDBContext(optionsBuilder.Options);
        }
    }
}
