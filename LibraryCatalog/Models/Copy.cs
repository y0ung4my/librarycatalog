using System.Collections.Generic;

namespace LibraryCatalog.Models
{

  public class Copy
  {
    public Copy()
    {
      this.JoinEntities4 = new HashSet<BookCopyPatron>();
    }
    public int CopyId { get; set; }
    public int BookId { get; set; }
    public virtual ICollection<BookCopyPatron> JoinEntities4 { get; set; }
  }
}