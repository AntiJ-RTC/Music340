using System.ComponentModel.DataAnnotations;

namespace Music340.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public IEnumerable<Album> Albums { get; set; }
    }
}
