namespace Book.API.Infrastructure.EntityConfigurations
{
    public class BookEntityConfiguration: IEntityTypeConfiguration<Model.Book>
    {
        public void Configure(EntityTypeBuilder<Model.Book> builder)
        {
            builder.Property(ci => ci.Title)
                .IsRequired(true);
            builder.HasOne(ci => ci.BookAuthor)
                .WithMany()
                .HasForeignKey(ci => ci.BookAuthorId);
            builder.HasOne(ci => ci.BookPublisher)
                .WithMany()
                .HasForeignKey(ci => ci.BookPublisherId);
        }
    }
}
