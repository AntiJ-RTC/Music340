using Microsoft.AspNetCore.Mvc;

namespace Music340.Controllers
{
    public class GenreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
