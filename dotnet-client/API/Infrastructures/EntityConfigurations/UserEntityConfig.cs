using API.Domains.Users;
using API.Infrastructures.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Infrastructures.EntityConfigurations;

public class UserEntityConfig :  IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("TABLE_USER");

        ConfigColumns(builder);
    }

    private static void ConfigColumns(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(sample => sample.Id);

        builder.Property(sample => sample.Created)
            .HasDefaultValueSql(SqlFunctions.GetDate);
    }
}
