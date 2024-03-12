using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Music340.Data;
using Music340.Models;
using Music340.ViewModels;

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
        public IActionResult Create(int genreId)
        {
            if (genreId == 0)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Album alb)
        {

            return View(alb);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            Album album = _context.Albums.SingleOrDefault(a => a.Id == id);
            if(album == null)
            {
                return NotFound();
            }
            AlbumCreateVM albVM = new AlbumCreateVM
            {

            };
            return View(albVM);
        }
    }
}
