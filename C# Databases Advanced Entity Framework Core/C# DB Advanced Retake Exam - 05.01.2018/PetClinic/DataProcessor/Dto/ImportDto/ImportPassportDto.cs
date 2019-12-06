using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetClinic.DataProcessor.Dto.ImportDto
{
    public class ImportPassportDto
    {

        [RegularExpression(@"^[A-z]{7}[0-9]{3}$")]
        public string SerialNumber { get; set; }

        [StringLength(30, MinimumLength = 3), Required]
        public string OwnerName { get; set; }

        [RegularExpression(@"^\+359[0-9]{9}|0[0-9]{9}$"), Required]
        public string OwnerPhoneNumber { get; set; }

        [Required]
        public string RegistrationDate { get; set; }
    }
}
