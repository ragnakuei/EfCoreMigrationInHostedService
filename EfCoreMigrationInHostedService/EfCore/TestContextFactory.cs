using EfCoreMigrationInHostedService.Parameters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EfCoreMigrationInHostedService.EfCore
{
    public class TestContextFactory : IDesignTimeDbContextFactory<TestDataBaseContext>
    {
        public TestContextFactory()
        {
        }

        public IConfiguration Configuration { get; set; }

        public TestDataBaseContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<TestDataBaseContext>()
                               .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                                             builder =>
                                             {
                                                 builder.MigrationsAssembly("EfCoreMigrationInHostedService");
                                             })
                               .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            return new TestDataBaseContext(optionBuilder.Options);
        }
    }
}
