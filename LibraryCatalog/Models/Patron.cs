using System.Collections.Generic;

namespace LibraryCatalog.Models
{

  public class Patron
  {
    public Patron()
    {
      this.JoinEntities4 = new HashSet<BookCopyPatron>();
    }
    public int PatronId { get; set; }
    public int Name { get; set; }
    public virtual ICollection<BookCopyPatron> JoinEntities4 { get; set; }
  }
}