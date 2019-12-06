using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Dto.ImportDto
{
    [XmlType("Vet")]
    public class ImportVetXmlDto
    {
        //        <?xml version = "1.0" encoding="UTF-8"?>
        //<Vets>
        //    <Vet>
        //        <Name>Michael Jordan</Name>
        //        <Profession>Emergency and Critical Care</Profession>
        //        <Age>45</Age>
        //        <PhoneNumber>0897665544</PhoneNumber>
        //    </Vet>

        [StringLength(40, MinimumLength = 3), Required]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 3), Required]
        public string Profession { get; set; }

        [Range(22, 65)]
        public int Age { get; set; }

        [RegularExpression(@"^\+359[0-9]{9}|0[0-9]{9}$"), Required]
        public string PhoneNumber { get; set; }

    }
}
