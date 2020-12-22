using EfCoreMigrationInHostedService.EfCore.Configurations;
using EfCoreMigrationInHostedService.EfCore.EntityModels;
using EfCoreMigrationInHostedService.Parameters;
using Microsoft.EntityFrameworkCore;

namespace EfCoreMigrationInHostedService.EfCore
{
    public class TestDataBaseContext : DbContext
    {
        public TestDataBaseContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<TestA> TestAs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation(DbParameter.Collation);
            modelBuilder.HasDefaultSchema(DbParameter.DefaultSchema);

            modelBuilder.ApplyConfiguration(new TestAConfiguration());
        }
    }
}
