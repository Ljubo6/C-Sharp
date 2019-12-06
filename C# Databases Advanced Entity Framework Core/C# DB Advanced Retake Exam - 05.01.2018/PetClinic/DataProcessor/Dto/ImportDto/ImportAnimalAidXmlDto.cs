using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Dto.ImportDto
{
    [XmlType("AnimalAid")]
    public class ImportAnimalAidXmlDto
    {
        [XmlElement("Name")]
        public string Name { get; set; }
    }
}
