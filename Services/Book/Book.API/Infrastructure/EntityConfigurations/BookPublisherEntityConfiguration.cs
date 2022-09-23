namespace Book.API.Infrastructure.EntityConfigurations;

public class BookPublisherEntityConfiguration : IEntityTypeConfiguration<BookPublisher>
{
    public void Configure(EntityTypeBuilder<BookPublisher> builder)
    {
        builder.ToTable("Publisher", "Book");
    }
}