using Cinema.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.DataProcessor.ImportDto
{
    public class ImportMovieDto
    {
        [StringLength(20, MinimumLength = 3), Required]
        public string Title { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Range(1, 10), Required]
        public double Rating { get; set; }

        [StringLength(20, MinimumLength = 3), Required]
        public string Director { get; set; }
    }
}
