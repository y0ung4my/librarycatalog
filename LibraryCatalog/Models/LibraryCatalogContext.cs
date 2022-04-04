using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryCatalog.Models
{
  public class LibraryCatalogContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Book> Book { get; set; }
    public DbSet<Author> Author { get; set; }
    public DbSet<Genre> Genre { get; set; }
    public DbSet<Patron> Patron { get; set; }
    public DbSet<Copy> Copy { get; set; }
    public DbSet<AuthorGenre> AuthorGenre {get; set; }
    public DbSet<AuthorBook> AuthorBook {get; set; }
    public DbSet<BookGenre> BookGenre {get; set; }

    public DbSet<BookCopyPatron> BookCopyPatron { get; set; }


    public LibraryCatalogContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}