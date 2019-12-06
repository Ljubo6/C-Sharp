using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetClinic.DataProcessor.Dto.ImportDto
{
    public class ImportAnimalAidDto
    {
        [StringLength(30, MinimumLength = 3), Required]
        public string Name { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335"), Required]
        public decimal Price { get; set; }
    }
}
