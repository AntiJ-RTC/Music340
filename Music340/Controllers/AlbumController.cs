using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            IEnumerable<Album> albums = _context.Albums.Include(x => x.Genre).Where(x => x.IsActive);
            return View(albums);
        }
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            Album alb = _context.Albums.SingleOrDefault(x => x.Id == id);
            if (alb == null)
            {
                return NotFound();
            }
            return View(alb);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Create(int genreId)
        {
            if (genreId == 0)
            {
                return NotFound();
            }
            Genre genre = _context.Genres.SingleOrDefault(x => x.Id == genreId);
            if (genre == null)
            {
                return NotFound();
            }
            AlbumCreateVM albVM = new AlbumCreateVM
            {
                GenreId = genre.Id,
                Genre = genre
            };
            return View(albVM);
        }
        [HttpPost]
        public IActionResult Create(AlbumCreateVM albVM)
        {
            if (!ModelState.IsValid)
            {
                Genre genre = _context.Genres.SingleOrDefault(x => x.Id == albVM.GenreId);
                if(genre == null)
                {
                    return NotFound();
                }
                return View(albVM);
            }
            string img = SaveUploadedFile(albVM.ItemImageFile);
            Album alb = new Album
            {
                GenreId = albVM.GenreId,
                Title = albVM.Title,
                Artist = albVM.Artist,
                Year = albVM.Year,
                ItemImage = img,
                IsActive = true
            };
            _context.Albums.Add(alb);
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
            Album album = _context.Albums.SingleOrDefault(a => a.Id == id);
            if(album == null)
            {
                return NotFound();
            }
            AlbumCreateVM albVM = new AlbumCreateVM
            {
                GenreId = album.GenreId,
                Title = album.Title,
                Artist = album.Artist,
                Year = album.Year,
                ItemImage = album.ItemImage,
                IsActive = album.IsActive
            };
            return View(albVM);
        }
        [HttpPost]
        public IActionResult Edit(Album alb) 
        {
            if (!ModelState.IsValid)
            {
                return View(alb);
            }
            _context.Albums.Update(alb);
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
            Album alb = _context.Albums.SingleOrDefault(x => x.Id == id);
            if(alb == null)
            {
                return NotFound();
            }
            return View(alb);
        }
        [HttpPost]
        public IActionResult Delete(Album album)
        {
            if(album.Id == 0)
            {
                return NotFound();
            }
            Album alb = _context.Albums.SingleOrDefault(x => x.Id == album.Id);
            if (alb == null)
            {
                return NotFound();
            }
            alb.IsActive = false;
            _context.Albums.Update(alb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        private string SaveUploadedFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = Path.Combine(_environment.WebRootPath, "images");
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string fullFilePath = Path.Combine(folder, fileName);
                using (FileStream fs = new FileStream(fullFilePath, FileMode.Create))
                {
                    file.CopyTo(fs);
                }
                return fileName;
            }
            return "";
        }
    }
}
