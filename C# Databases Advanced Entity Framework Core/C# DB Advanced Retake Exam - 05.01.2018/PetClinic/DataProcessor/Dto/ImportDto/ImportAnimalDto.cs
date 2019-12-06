using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetClinic.DataProcessor.Dto.ImportDto
{
    public class ImportAnimalDto
    {
        [StringLength(20, MinimumLength = 3), Required]
        public string Name { get; set; }

        [StringLength(20, MinimumLength = 3), Required]
        public string Type { get; set; }

        [Range(1, int.MaxValue), Required]
        public int Age { get; set; }

        [Required]
        public ImportPassportDto Passport { get; set; }
    }
}
