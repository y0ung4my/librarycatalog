using System.Collections.Generic;

namespace LibraryCatalog.Models
{

  public class Author
  {
    public Author()
        {
            this.JoinEntities = new HashSet<AuthorGenre>();
            this.JoinEntities2 = new HashSet<AuthorBook>();
        }
    public int AuthorId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<AuthorGenre> JoinEntities { get; set; }
    public virtual ICollection<AuthorBook> JoinEntities2 { get; set; }
  }
}