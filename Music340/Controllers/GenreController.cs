using Microsoft.AspNetCore.Mvc;
using Music340.Data;

namespace Music340.Controllers
{
    public class GenreController : Controller
    {
        ApplicationDbContext _context;
        public GenreController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var data = _context.Genres.ToList();
            return View(data);
        }
    }
}
