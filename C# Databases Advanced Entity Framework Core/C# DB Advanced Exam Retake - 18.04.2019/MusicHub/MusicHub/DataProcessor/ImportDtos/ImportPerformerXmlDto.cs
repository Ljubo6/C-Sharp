using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace MusicHub.DataProcessor.ImportDtos
{
    [XmlType("Performer")]
    public class ImportPerformerXmlDto
    {
        [StringLength(20, MinimumLength = 3), Required]
        public string FirstName { get; set; }

        [StringLength(20, MinimumLength = 3), Required]
        public string LastName { get; set; }

        [Range(18, 70), Required]
        public int Age { get; set; }

        [Range(typeof(decimal), "0.0", "79228162514264337593543950335"), Required]
        public decimal NetWorth { get; set; }

        [XmlArray("PerformersSongs")]
        public ImportPerformerSongXmlDto[] PerformersSongs { get; set; }

    }
}
