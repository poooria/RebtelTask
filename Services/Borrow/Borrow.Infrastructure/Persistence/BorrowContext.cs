namespace Borrow.Infrastructure.Persistence;

public class BorrowContext : DbContext
{
    public BorrowContext(DbContextOptions<BorrowContext> options) : base(options)
    {
    }
    public DbSet<Core.Entities.BorrowedBook> BorrowedBooks { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}