using Microsoft.EntityFrameworkCore;

namespace LibraryCatalog.Models
{
  public class LibraryCatalogContext : DbContext
  {
    public DbSet<Book> Book { get; set; }
    public DbSet<Author> Author { get; set; }
    public DbSet<Genre> Genre { get; set; }
    public DbSet<AuthorGenre> AuthorGenre {get; set;}


    public LibraryCatalogContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}