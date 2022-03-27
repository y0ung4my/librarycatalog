using System.Collections.Generic;

namespace LibraryCatalog.Models
{

  public class Genre
  {
    public Genre()
        {
            this.JoinEntities = new HashSet<AuthorGenre>();
            this.JoinEntities3 = new HashSet<BookGenre>();
        }
    public int GenreId { get; set; }
    public string Name { get; set; }
    
    public virtual ICollection<AuthorGenre> JoinEntities { get; set; }
    public virtual ICollection<BookGenre> JoinEntities3 { get; set; }
  }
}