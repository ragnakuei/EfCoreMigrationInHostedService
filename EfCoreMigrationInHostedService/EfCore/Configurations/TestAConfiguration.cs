using EfCoreMigrationInHostedService.EfCore.EntityModels;
using EfCoreMigrationInHostedService.Infras;
using EfCoreMigrationInHostedService.Parameters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreMigrationInHostedService.EfCore.Configurations
{
    internal class TestAConfiguration : IEntityTypeConfiguration<TestA>
    {
        public void Configure(EntityTypeBuilder<TestA> builder)
        {
            builder.ToTable(nameof(TestA), DbParameter.DefaultSchema);

            builder.HasKey(x => new
                                {
                                    x.Id,
                                })
                   .IsClustered();

            builder.HasIndex(u => u.Guid)
                   .HasName($"IX_{nameof(TestA)}_{nameof(TestA.Guid)}")
                   .IsUnique();

            builder.Property(x => x.Id)
                   .IsRequired()
                   .ValueGeneratedNever()
                   .HasColumnType("bigint")
                   .HasComment("ID");

            builder.Property(x => x.Guid)
                   .IsRequired()
                   .HasColumnType("uniqueidentifier")
                   .HasComment("Guid");

            builder.Property(x => x.Age)
                   .IsRequired()
                   .HasColumnType("int")
                   .HasComment("年齡");

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasColumnType("nvarchar(50)")
                   .HasComment("名稱");
        }
    }
}
