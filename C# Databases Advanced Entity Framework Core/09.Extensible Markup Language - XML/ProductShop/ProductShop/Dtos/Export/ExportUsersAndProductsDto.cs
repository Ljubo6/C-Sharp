using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using static ProductShop.Dtos.Export.ExportUserSoldProductDto;

namespace ProductShop.Dtos.Export
{
    [XmlType("User")]
    public class ExportUsersAndProductsDto
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }
        [XmlElement("lastName")]
        public string LastName { get; set; }
        [XmlElement("age")]
        public int? Age { get; set; }

        [XmlElement("SoldProducts")]
        public SoldProductsDto SoldProducts { get; set; }

        public class SoldProductsDto
        {
            [XmlElement("count")]
            public int Count { get; set; }

            [XmlArray("products")]
            public ProductDto[] Products { get; set; }

        }

    }
}
