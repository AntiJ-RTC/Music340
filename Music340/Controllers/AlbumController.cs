using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Music340.Data;
using Music340.Models;

namespace Music340.Controllers
{
    public class AlbumController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _environment;
        public AlbumController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            var data = _context.Albums.ToList();
            return View(data);
        }
        public async Task<IActionResult> Details(int? id)
        {
            var getDetails = await _context.Albums.FindAsync(id);
            return View(getDetails);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Create(string genre)
        {
            IEnumerable<string> genres = _context.Genres.Select(a  => a.Name).Distinct();
            IEnumerable<SelectListItem> selectGenre = genres.Select(a => new SelectListItem
            {
                Text = a.ToString(),
                Value = a.ToString(),
                Selected = a == genre
            });
            ViewBag.Genre = selectGenre;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Album alb)
        {
            if (ModelState.IsValid)
            {
                _context.Albums.Add(alb);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(alb);
        }
    }
}
