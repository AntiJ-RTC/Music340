using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Album alb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alb);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(alb);
        }
    }
}
