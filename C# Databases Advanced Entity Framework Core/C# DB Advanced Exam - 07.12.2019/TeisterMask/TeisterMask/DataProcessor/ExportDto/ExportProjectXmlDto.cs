using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Project")]
    public class ExportProjectXmlDto
    {
        [XmlAttribute("TasksCount")]
        public int TasksCount { get; set; }

        public string ProjectName { get; set; }

        public string HasEndDate { get; set; }

        [XmlArray("Tasks")]
        public ExportTaskXmlDto[] Tasks { get; set; }
    }
}
