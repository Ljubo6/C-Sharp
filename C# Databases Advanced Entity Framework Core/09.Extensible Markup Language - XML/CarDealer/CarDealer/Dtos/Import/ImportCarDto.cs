using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.Dtos.Import
{
    [XmlType("Car")]
    public class ImportCarDto
    {
        [XmlElement(ElementName = "make")]
        public string Make { get; set; }

        [XmlElement(ElementName = "model")]
        public string Model { get; set; }

        [XmlElement(ElementName = "TraveledDistance")]
        public long TraveledDistance { get; set; }

        [XmlArray(ElementName = "parts")]
        public ImportCarPartDto[] Parts { get; set; }

        [XmlType("partId")]
        public class ImportCarPartDto
        {
            [XmlAttribute("id")]
            public int Id { get; set; }
        }
    }
}
