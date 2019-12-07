using MusicHub.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace MusicHub.DataProcessor.ImportDtos
{
    [XmlType("Song")]
    public class ImportSongXmlDto
    {
        [StringLength(20, MinimumLength = 3), Required]
        public string Name { get; set; }

        [Required]
        public string Duration { get; set; }

        [Required]
        public string CreatedOn { get; set; }

        [Required]
        public string Genre { get; set; }

        public int? AlbumId { get; set; }

        [Required]
        public int WriterId { get; set; }

        [Range(typeof(decimal), "0.0", "79228162514264337593543950335"), Required]
        public decimal Price { get; set; }
    }
}
