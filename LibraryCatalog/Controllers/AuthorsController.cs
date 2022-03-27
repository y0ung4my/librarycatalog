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
  public class AuthorsController : Controller
  {
    private readonly LibraryCatalogContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthorsController(UserManager<ApplicationUser> userManager, LibraryCatalogContext db)
    {
      _db = db;
      _userManager = userManager;
    }

    [AllowAnonymous]
    public ActionResult Index()
   {
    return View(_db.Author.ToList());
   }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Author author)
    {
      _db.Author.Add(author);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      var thisAuthor = _db.Author
        .Include(author => author.JoinEntities2)
        .ThenInclude(join => join.Book)
        .FirstOrDefault(author => author.AuthorId == id);
      return View(thisAuthor);
    }
    public ActionResult Edit(int id)
    {
      var thisAuthor = _db.Author.FirstOrDefault(author => author.AuthorId == id);
      return View(thisAuthor);
    }

    [HttpPost]
    public ActionResult Edit(Author author)
    {
      _db.Entry(author).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisAuthor = _db.Author.FirstOrDefault(author => author.AuthorId == id);
      return View(thisAuthor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisAuthor = _db.Author.FirstOrDefault(author => author.AuthorId == id);
      _db.Author.Remove(thisAuthor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}