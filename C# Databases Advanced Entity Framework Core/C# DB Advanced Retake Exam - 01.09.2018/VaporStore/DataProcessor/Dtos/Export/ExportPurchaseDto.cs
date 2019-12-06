using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dtos.Export
{
    [XmlType("Purchase")]
    public class ExportPurchaseDto
    {
        public string Card { get; set; }
        public string Cvc { get; set; }
        public string Date { get; set; }
        public ExportGameDto Game { get; set; }
    }
}
