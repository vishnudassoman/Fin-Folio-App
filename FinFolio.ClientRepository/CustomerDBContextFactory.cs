using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FinFolio.ClientRepository
{
    public class CustomerDBContextFactory : IDesignTimeDbContextFactory<CustomerDBContext>
    {
        public CustomerDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CustomerDBContext>();
            optionsBuilder.UseSqlServer<CustomerDBContext>("Data Source=.;Initial Catalog=Portfolio;Integrated Security=True");
            return new CustomerDBContext(optionsBuilder.Options);
        }
    }
}
