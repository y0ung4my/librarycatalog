namespace LibraryCatalog.Models
{
    public class BookGenre
    {
        public int BookGenreId {get; set;}
        public int GenreId {get; set;}
        public int BookId {get; set;}
        public virtual Genre Genre {get; set;}
        public virtual Book Book {get; set;}
    }
}