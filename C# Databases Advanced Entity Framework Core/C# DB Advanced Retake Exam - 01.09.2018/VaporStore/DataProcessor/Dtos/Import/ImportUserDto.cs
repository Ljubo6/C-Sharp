using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporStore.DataProcessor.Dtos.Import
{
    public class ImportUserDto
    {
        [RegularExpression("^[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+$"), Required]
        public string FullName { get; set; }

        [MinLength(3), MaxLength(20), Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Range(3, 103), Required]
        public int Age { get; set; }
        public CardsDto[] Cards { get; set; }
    }
}
