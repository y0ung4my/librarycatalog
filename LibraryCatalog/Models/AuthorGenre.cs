namespace LibraryCatalog.Models
{
    public class AuthorGenre
    {
        public int AuthorGenreId {get; set;}
        public int AuthorId {get; set;}
        public int GenreId {get; set;}
        public virtual Author Author {get; set;}
        public virtual Genre Genre {get; set;}
    }
}