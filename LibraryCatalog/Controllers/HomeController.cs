using Microsoft.AspNetCore.Mvc;

namespace LibraryCatalog.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }
    }
}