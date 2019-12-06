using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dtos.Export
{
    [XmlType("User")]
    public class ExportUserDto
    {
        [XmlAttribute("username")]
        public string Username { get; set; }
        public ExportPurchaseDto[] Purchases { get; set; }
        public decimal TotalSpent { get; set; }

    }
}
