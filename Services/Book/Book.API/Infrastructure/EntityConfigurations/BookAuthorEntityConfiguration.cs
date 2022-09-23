namespace Book.API.Infrastructure.EntityConfigurations;

public class BookAuthorEntityConfiguration : IEntityTypeConfiguration<BookAuthor>
{
    public void Configure(EntityTypeBuilder<BookAuthor> builder)
    {
        builder.ToTable("Author","Book");
    }
}