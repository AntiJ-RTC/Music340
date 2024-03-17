using Music340.Validation;
using Music340.Models;
using System.ComponentModel.DataAnnotations;

namespace Music340.ViewModels
{
    public class AlbumCreateVM
    {
        public int Id { get; set; }
        public int GenreId { get; set; }
        [Required(ErrorMessage = "Album Title cannot be blank")]
        [Display(Name = "Album Title")]
        public string Title { get; set; }
        [StringLength(50, ErrorMessage = "Artist cannot be more than 50 characters")]
        public string Artist { get; set; }
        [Required(ErrorMessage = "Year cannot be blank")]
        [Range(1900, 2024)]
        public int Year { get; set; }
        [Display(Name = "Upload Item Photo")]
        [ItemImgValidation]
        public IFormFile ItemImageFile { get; set; }
        public bool IsActive { get; set; }
        public Genre Genre { get; set; }
    }
}
