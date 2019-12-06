using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Customer")]
    public class ImportCustomerTicketDto
    {

        [StringLength(20, MinimumLength = 3), Required]
        public string FirstName { get; set; }

        [StringLength(20, MinimumLength = 3), Required]
        public string LastName { get; set; }


        [Range(12, 110), Required]
        public int Age { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335"), Required]
        public decimal Balance { get; set; }

        public ImportTicketDto[] Tickets { get; set; }

    }

}
