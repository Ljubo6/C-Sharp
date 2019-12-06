using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Dto.ExportDto
{
    [XmlType("AnimalAid")]
    public class ExportAnimalAidXmlDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
