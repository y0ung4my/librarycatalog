using System.Collections.Generic;

namespace LibraryCatalog.Models
{

  public class Book
  {
    public Book()
    {
          this.JoinEntities2 = new HashSet<AuthorBook>();
          this.JoinEntities3 = new HashSet<BookGenre>();
          this.JoinEntities4 = new HashSet<BookCopyPatron>();
    }
    public int BookId { get; set; }
    public string Name { get; set; }
    public string PublishDate { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }

    public virtual ICollection<Copy> Copies { get; set; }
    public virtual ICollection<AuthorBook> JoinEntities2 { get; set; }
    public virtual ICollection<BookGenre> JoinEntities3 { get; set; }
    public virtual ICollection<BookCopyPatron> JoinEntities4 { get; set; }
  }
}