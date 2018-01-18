using Books.Migrations;
using System.Data.Entity;

public class BookContext : DbContext
{
    public BookContext()
    {
        Database.SetInitializer<BookContext>(new MigrateDatabaseToLatestVersion<BookContext, Configuration>());
    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
}