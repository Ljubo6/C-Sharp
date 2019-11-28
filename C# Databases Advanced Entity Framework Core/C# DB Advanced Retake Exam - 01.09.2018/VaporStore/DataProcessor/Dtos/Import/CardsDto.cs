using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporStore.DataProcessor.Dtos.Import
{
    public class CardsDto
    {
        [RegularExpression("^[A-Z0-9]{4} [A-Z0-9]{4} [A-Z0-9]{4} [A-Z0-9]{4}$"), Required]
        public string Number { get; set; }

        [RegularExpression("[0-9]{3}"), Required]
        public string CVC { get; set; }

        [Required]
        public string Type { get; set; }

    }
}
