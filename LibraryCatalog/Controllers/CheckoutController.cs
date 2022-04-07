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
using System.Web;


namespace LibraryCatalog.Controllers
{
  [Authorize]
  public class CheckoutController : Controller
  {
    private readonly LibraryCatalogContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public CheckoutController(UserManager<ApplicationUser> userManager, LibraryCatalogContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index(string searchBy, string search)
    {
        if (searchBy == "BookTitle")
        {
          return View(_db.BookCopyPatron.Where(x => x.Book.Name.Contains(search) || search == null).ToList());
        }
        else if (searchBy == "PatronName")
        {
          return View(_db.BookCopyPatron.Where(x => x.Patron.Name.Contains(search) || search == null).ToList());
        }
        {
          List<BookCopyPatron> model = _db.BookCopyPatron.ToList();
          return View(model);
        }
    }

    public ActionResult Create()
    {
      ViewBag.BookId = new SelectList(_db.Book, "BookId", "Name");
      ViewBag.PatronId = new SelectList(_db.Patron, "PatronId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(BookCopyPatron bookCopyPatron, int BookId, int PatronId)
    {
      
      _db.BookCopyPatron.Add(bookCopyPatron);
      _db.SaveChanges();
      return RedirectToAction("AddCopy", new { id = bookCopyPatron.BookId });;
    }

    // public ActionResult AddCopy(int id)
    // {
    //   var thisBook = _db.BookCopyPatron.FirstOrDefault(book => book.BookId == id);
    //   return View(thisBook);
    // }

    // [HttpPost]
    // public ActionResult AddCopy(BookCopyPatron bookCopyPatron, int CopyId)
    // {
    //   bookCopyPatron.CopyId = CopyId;
    //   _db.Entry(bookCopyPatron).State = EntityState.Modified;
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }
    

    public ActionResult Details(int id)
    {
      var thisBookCopyPatron = _db.BookCopyPatron
        .FirstOrDefault(checkout => checkout.BookCopyPatronId == id);
      return View(thisBookCopyPatron);
    }

    public ActionResult Edit(int id)
    {
      Patron thisPatron = _db.Patron.FirstOrDefault(patron => patron.PatronId == id);
      return View(thisPatron);
    }

    // [HttpPost]
    // public ActionResult Edit(Patron patron)
    // {
    //   _db.Entry(patron).State = EntityState.Modified;
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }
    public ActionResult Delete(int id)
    {
      var thisBookCopyPatron = _db.BookCopyPatron.FirstOrDefault(checkout => checkout.BookCopyPatronId == id);
      return View(thisBookCopyPatron);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisBookCopyPatron = _db.BookCopyPatron.FirstOrDefault(checkout => checkout.BookCopyPatronId == id);
      _db.BookCopyPatron.Remove(thisBookCopyPatron);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}

// return RedirectToAction("Details", new { id = stylist.StylistId });