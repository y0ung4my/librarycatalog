using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using LibraryCatalog.Models;
using System.Collections.Generic;
using System.Linq;

namespace LibraryCatalog.Controllers
{
  public class GenresController : Controller
  {
    private readonly LibraryCatalogContext _db;

    public GenresController(LibraryCatalogContext db)
    {
      _db = db;
    }

    public ActionResult Index()
   {
    return View(_db.Genre.ToList());
   }

//    [HttpPost]
//    public ActionResult Create(Genre genre, int BookId)
// {
//     _db.Genre.Add(genre);
//     _db.SaveChanges();
//     if (BookId != 0)
//     {
//         _db.BookGenre.Add(new BookGenre() { BookId = BookId, GenreId = genre.GenreId });
//         _db.SaveChanges();
//     }
//     return RedirectToAction("Index");
// }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Genre genre)
    {
        _db.Genre.Add(genre);
        _db.SaveChanges();
        return RedirectToAction("Index");    
    }
    

     public ActionResult Details(int id)
    {
        var thisGenre = _db.Genre
        .Include(item => item.JoinEntities3)
        .ThenInclude(join => join.Book)
        .FirstOrDefault(item => item.GenreId == id);
        return View(thisGenre);
    }
 

   public ActionResult Edit(int id)
{
    var thisItem = _db.Genre.FirstOrDefault(genre => genre.GenreId == id);
    ViewBag.BookId = new SelectList(_db.Book, "BookId", "Name");
    return View(thisItem);
}

      [HttpPost]
    public ActionResult Edit(Genre genre, int BookId)
    {
      if (BookId != 0)
      {
        _db.BookGenre.Add(new BookGenre() { BookId = BookId, GenreId = genre.GenreId });
      }
      _db.Entry(genre).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    
//     public ActionResult AddBook(int id)
// {
//     var thisGenre = _db.Genre.FirstOrDefault(genre => genre.GenreId == id);
//     ViewBag.BookId = new SelectList(_db.Book, "BookId", "Name");
//     return View(thisGenre);
// }
// [HttpPost]
// public ActionResult AddBook(Genre genre, int BookId)
// {
//     if (BookId != 0)
//     {
//       _db.BookGenre.Add(new BookGenre() { BookId = BookId, GenreId = genre.GenreId });
//       _db.SaveChanges();
//     }
//     return RedirectToAction("Index");
// }
    public ActionResult Delete(int id)
    {
      var thisGenre = _db.Genre.FirstOrDefault(genre => genre.GenreId == id);
      return View(thisGenre);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisGenre = _db.Genre.FirstOrDefault(genre => genre.GenreId == id);
      _db.Genre.Remove(thisGenre);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    // [HttpPost]
// public ActionResult DeleteCategory(int joinId)
// {
//     var joinEntry = _db.BookGenre.FirstOrDefault(entry => entry.BookGenreId == joinId);
//     _db.BookGenre.Remove(joinEntry);
//     _db.SaveChanges();
//     return RedirectToAction("Index");
// }
  }
}