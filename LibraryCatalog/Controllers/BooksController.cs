using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using LibraryCatalog.Models;
using System.Collections.Generic;
using System.Linq;

namespace LibraryCatalog.Controllers
{
  public class BooksController : Controller
  {
    private readonly LibraryCatalogContext _db;

    public BooksController(LibraryCatalogContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Book> model = _db.Book.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.AuthorId = new SelectList(_db.Author, "AuthorId", "Name");
      ViewBag.GenreId = new SelectList(_db.Genre, "GenreId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Book book, int AuthorId, int GenreId)
    {
      _db.Book.Add(book);
      _db.SaveChanges();
      if (AuthorId != 0)
      {
          _db.AuthorBook.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId });
          _db.SaveChanges();
      }
      if (GenreId != 0)
      {
          _db.BookGenre.Add(new BookGenre() { GenreId = GenreId, BookId = book.BookId });
          _db.SaveChanges();
      }
      return RedirectToAction("Index");
      }

    public ActionResult Details(int id)
    {
      var thisBook = _db.Book
        .Include(book => book.JoinEntities2)
        .ThenInclude(join => join.Author)
        .Include(book => book.JoinEntities3)
        .ThenInclude(join => join.Genre)
        .FirstOrDefault(book => book.BookId == id);
      return View(thisBook);
    }
    public ActionResult Edit(int id)
    {
      Book thisBook = _db.Book.FirstOrDefault(book => book.BookId == id);
      ViewBag.AuthorId = new SelectList(_db.Author, "AuthorId", "Name");
      ViewBag.GenreId = new SelectList(_db.Genre, "GenreId", "Name");
      return View(thisBook);
    }

    [HttpPost]
    public ActionResult Edit(Book book, int AuthorId, int GenreId, int JoinId, int JoinId2)
    {
      bool duplicate = _db.AuthorBook.Any(join => join.AuthorId == AuthorId && join.BookId == book.BookId);
      bool duplicate2 = _db.BookGenre.Any(join => join.GenreId == GenreId && join.BookId == book.BookId);
      if (AuthorId != 0 && !duplicate)
      {
          _db.AuthorBook.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId });
          _db.SaveChanges();
      }
      if (AuthorId != 0 && !duplicate2)
      {
          _db.BookGenre.Add(new BookGenre() { GenreId = GenreId, BookId = book.BookId });
          _db.SaveChanges();
      }

      _db.Entry(book).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisBook = _db.Book.FirstOrDefault(book => book.BookId == id);
      return View(thisBook);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisBook = _db.Book.FirstOrDefault(book => book.BookId == id);
      _db.Book.Remove(thisBook);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    //   [HttpPost, ActionName("Delete")]
    // public ActionResult DeleteConfirmed(int authorId)
    // {
    //   var thisAuthor = _db.Author.FirstOrDefault(book => book.AuthorId == authorId);
    //   _db.Author.Remove(thisAuthor);
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }
    //   [HttpPost, ActionName("Delete")]
    // public ActionResult DeleteConfirmed(int id)
    // {
    //   var thisBook = _db.Book.FirstOrDefault(book => book.BookId == id);
    //   _db.Book.Remove(thisBook);
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }
        [HttpPost]
        public ActionResult DeleteAuthor(int joinId)
    {
        var joinEntry = _db.AuthorBook.FirstOrDefault(entry => entry.AuthorBookId == joinId);
        _db.AuthorBook.Remove(joinEntry);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
        [HttpPost]
        public ActionResult DeleteGenre(int joinId)
    {
        var joinEntry = _db.BookGenre.FirstOrDefault(entry => entry.BookGenreId == joinId);
        _db.BookGenre.Remove(joinEntry);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
  }
}



// The following could be used to update an existing AuthorBook or BookGenre row (to be used in Edit())
      // foreach (var join in _db.AuthorBook)
      // {
      //   if (join.BookId == book.BookId) 
      //   {
      //     join.AuthorId = AuthorId;
      //     _db.Entry(join).State = EntityState.Modified;
      //   }
      // }
      // foreach (var join in _db.BookGenre)
      // {
      //   if (join.BookId == book.BookId) 
      //   {
      //     join.GenreId = GenreId;
      //     _db.Entry(join).State = EntityState.Modified;
      //   }
      // }