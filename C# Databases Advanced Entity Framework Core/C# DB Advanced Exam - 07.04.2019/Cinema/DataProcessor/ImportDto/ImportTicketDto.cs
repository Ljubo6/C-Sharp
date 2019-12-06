using Cinema.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Ticket")]
    public class ImportTicketDto
    {

        [Required]
        public int ProjectionId { get; set; }


        [Range(typeof(decimal), "0.01", "79228162514264337593543950335"), Required]
        public decimal Price { get; set; }


    }
}