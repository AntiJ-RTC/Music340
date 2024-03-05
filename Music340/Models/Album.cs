﻿using System.ComponentModel.DataAnnotations;

namespace Music340.Models
{
    public class Album
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Album Title cannot be blank")]
        [Display(Name = "Album Title")]
        public string Title { get; set; }
        [StringLength(50, ErrorMessage = "Artist cannot be more than 50 characters")]
        public string Artist { get; set; }
        [Required(ErrorMessage = "Year cannot be blank")]
        [Range(1900,2024)]
        public int Year { get; set; }
        public string Genre { get; set; }
    }
}
