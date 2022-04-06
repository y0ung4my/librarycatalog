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
  public class PatronsController : Controller
  {
    private readonly LibraryCatalogContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public PatronsController(UserManager<ApplicationUser> userManager, LibraryCatalogContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index(string searchBy, string search)
    {
        if (searchBy == "Name")
        {
          return View(_db.Patron.Where(x => x.Name.Contains(search) || search == null).ToList());
        }
        else
        {
          List<Patron> model = _db.Patron.ToList();
          return View(model);
        }
    }

    public ActionResult Create()
    {
      
      return View();
    }

    [HttpPost]
    public ActionResult Create(Patron patron)
    {
      
      _db.Patron.Add(patron);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisPatron = _db.Patron
        .Include(patron => patron.JoinEntities4)
        .ThenInclude(join => join.Book)
        .Include(patron => patron.JoinEntities4)
        .ThenInclude(join => join.Copy)
        .FirstOrDefault(patron => patron.PatronId == id);
      return View(thisPatron);
    }

    public ActionResult Edit(int id)
    {
      Patron thisPatron = _db.Patron.FirstOrDefault(patron => patron.PatronId == id);
      return View(thisPatron);
    }

    [HttpPost]
    public ActionResult Edit(Patron patron)
    {
      _db.Entry(patron).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
      var thisPatron = _db.Patron.FirstOrDefault(patron => patron.PatronId == id);
      return View(thisPatron);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisPatron = _db.Patron.FirstOrDefault(patron => patron.PatronId == id);
      _db.Patron.Remove(thisPatron);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}