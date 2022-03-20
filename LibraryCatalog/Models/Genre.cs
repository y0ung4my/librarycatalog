using System.Collections.Generic;

namespace LibraryCatalog.Models
{

  public class Genre
  {
    public Genre()
        {
            this.JoinEntities = new HashSet<AuthorGenre>();
        }
    public int GenreId { get; set; }
    public string Name { get; set; }

    public virtual ICollection<AuthorGenre> JoinEntities { get; set; }
  }
}