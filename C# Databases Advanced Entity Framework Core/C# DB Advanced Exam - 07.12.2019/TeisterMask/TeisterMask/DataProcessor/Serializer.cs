namespace TeisterMask.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.Data.Models.Enums;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var projects = context.Projects
                .Where(x => x.Tasks.Any())
                .Select(p => new ExportProjectXmlDto 
                {
                    TasksCount = p.Tasks.Count,
                    ProjectName = p.Name,
                    HasEndDate = p.DueDate == null ? "No" : "Yes",
                    Tasks = p.Tasks
                    .Select(t => new ExportTaskXmlDto 
                    {
                        Name = t.Name,
                        Label = t.LabelType.ToString()
                    })
                    .OrderBy(t => t.Name)
                    .ToArray()
                })
                .OrderByDescending(p => p.TasksCount)
                .ThenBy(p => p.ProjectName)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ExportProjectXmlDto[]), new XmlRootAttribute("Projects"));

            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(sb), projects, namespaces);

            return sb.ToString().TrimEnd();

        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {

            var employees = context.Employees
                .Where(x => x.EmployeesTasks.Any(y => y.Task.OpenDate >= date))
                .OrderByDescending(e => e.EmployeesTasks.Count(t => t.Task.OpenDate >= date))
                .ThenBy(e => e.Username)
                .Select(e => new
                {
                    Username = e.Username,
                    Tasks = e.EmployeesTasks
                    .Where(z => z.Task.OpenDate >= date)
                    .Select(t => new
                    {
                        TaskName = t.Task.Name,
                        OpenDate = t.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                        DueDate = t.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                        LabelType = t.Task.LabelType.ToString(),
                        ExecutionType = t.Task.ExecutionType.ToString()
                    })

                    .OrderByDescending(t => DateTime.ParseExact(t.DueDate,"d",CultureInfo.InvariantCulture))
                    .ThenBy(t => t.TaskName)
                    .ToArray()
                })
                .Take(10)
                .ToArray();


            var json = JsonConvert.SerializeObject(employees, Newtonsoft.Json.Formatting.Indented);

            return json;

        }
    }
}