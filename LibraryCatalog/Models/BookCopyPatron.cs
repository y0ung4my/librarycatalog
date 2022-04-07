namespace LibraryCatalog.Models
{
    public class BookCopyPatron
    {
        public int BookCopyPatronId {get; set;}
        public int BookId {get; set;}
        public int? CopyId {get; set;}

        public int PatronId { get; set; }
        public virtual Book Book {get; set;}
        public virtual Copy Copy {get; set;}

        public virtual Patron Patron { get; set; }
    }
}