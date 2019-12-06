using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dtos.Export
{
    [XmlType("Game")]
    public class ExportGameDto
    {
        [XmlAttribute("title")]
        [Required]
        public string Title { get; set; }
        
        public string Genre { get; set; }

        public decimal Price { get; set; }
    }
}
