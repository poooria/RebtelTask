namespace Book.API.Infrastructure.EntityConfigurations;

public class BookLanguageEntityConfiguration : IEntityTypeConfiguration<BookLanguage>
{
    public void Configure(EntityTypeBuilder<BookLanguage> builder)
    {
        builder.ToTable("Language", "Book");
    }
}