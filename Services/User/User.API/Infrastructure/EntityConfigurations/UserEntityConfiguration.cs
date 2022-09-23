using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.API.Model;

namespace User.API.Infrastructure.EntityConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User.API.Model.User>
{
    public void Configure(EntityTypeBuilder<User.API.Model.User> builder)
    {
        builder.OwnsOne(x => x.Address);
    }
}