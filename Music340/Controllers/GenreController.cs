using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Music340.Data;
using Music340.Models;
using NuGet.Protocol.Plugins;

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
            return View(_context.Genres.Where(x => x.IsActive));
        }
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            Genre genre = _context.Genres.FirstOrDefault(x => x.Id == id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View(genre);
            }
            genre.IsActive = true;
            _context.Genres.Add(genre);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            Genre genre = _context.Genres.FirstOrDefault(x => x.Id == id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }
        [HttpPost]
        public IActionResult Edit(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View(genre);
            }
            _context.Genres.Update(genre);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            Genre genre = _context.Genres.FirstOrDefault(x => x.Id == id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }
        [HttpPost]
        public IActionResult Delete(Genre genre)
        {
            if (genre.Id == 0)
            {
                return NotFound();
            }
            Genre g = _context.Genres.FirstOrDefault(x => x.Id == genre.Id);
            if (g == null)
            {
                return NotFound();
            }
            g.IsActive = false;
            _context.Genres.Update(g);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
