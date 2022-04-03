using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using LibraryCatalog.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace LibraryCatalog.Controllers
{
  [Authorize]
  public class GenresController : Controller
  {
    private readonly LibraryCatalogContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public GenresController(UserManager<ApplicationUser> userManager, LibraryCatalogContext db)
    {
      _userManager = userManager;
      _db = db;
    }
    [AllowAnonymous]
    // public ActionResult Index()
    // {
    //   return View(_db.Genre.ToList());
    // }

    public ActionResult Index(string searchBy, string search)
      {
        if (searchBy == "Name")
        {
          return View(_db.Genre.Where(x => x.Name.Contains(search) || search == null).ToList());
        }
        else
        {
          List<Genre> model = _db.Genre.ToList();
          return View(model);
        }
      }

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
    
    [AllowAnonymous]
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
  }
}