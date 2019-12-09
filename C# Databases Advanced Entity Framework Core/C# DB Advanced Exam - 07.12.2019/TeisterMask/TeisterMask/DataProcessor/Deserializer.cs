namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    using Data;
    using System.Xml.Serialization;
    using TeisterMask.DataProcessor.ImportDto;
    using System.IO;
    using System.Text;
    using TeisterMask.Data.Models;
    using System.Linq;
    using System.Globalization;
    using TeisterMask.Data.Models.Enums;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportProjectXmlDto[]), new XmlRootAttribute("Projects"));
            var projectsDto = (ImportProjectXmlDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();

            var projects = new List<Project>();



            foreach (var projectDto in projectsDto)
            {

                if (!IsValid(projectDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var project = new Project
                {
                    Name = projectDto.Name,
                    OpenDate = DateTime.ParseExact(projectDto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    DueDate = projectDto.DueDate == null || projectDto.DueDate == "" ? (DateTime?)null : DateTime.ParseExact(projectDto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                };

                foreach (var taskDto in projectDto.Tasks)
                {

                    var execution = Enum.TryParse<ExecutionType>(taskDto.ExecutionType.ToString(), out ExecutionType executionType);
                    var label = Enum.TryParse<LabelType>(taskDto.LabelType.ToString(), out LabelType labelType);

                    if (!IsValid(taskDto) || !execution || !label)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }


                    DateTime taskOpenDate = DateTime.ParseExact(taskDto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    DateTime projectOpenDate = project.OpenDate;

                    DateTime taskDueDate = DateTime.ParseExact(taskDto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    DateTime? projectDueDate = project.DueDate;

                    if (taskOpenDate < projectOpenDate || taskDueDate > projectDueDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var task = new Task
                    {
                        Name = taskDto.Name,
                        OpenDate = taskOpenDate,
                        DueDate = taskDueDate,
                        ExecutionType = executionType,
                        LabelType = labelType
                    };
                    project.Tasks.Add(task);

                }
                projects.Add(project);
                sb.AppendLine(string.Format(SuccessfullyImportedProject, project.Name, project.Tasks.Count));
            }

            context.Projects.AddRange(projects);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var employeesDto = JsonConvert.DeserializeObject<ImportEmployeeDto[]>(jsonString);

            List<Employee> employees = new List<Employee>();

            StringBuilder sb = new StringBuilder();            

            foreach (var employeeDto in employeesDto)
            {
                bool isValidEmployee = IsValid(employeeDto);

                if (isValidEmployee == false)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Employee employee = new Employee
                {
                    Username = employeeDto.Username,
                    Email = employeeDto.Email,
                    Phone = employeeDto.Phone
                };

                foreach (var taskId in employeeDto.Tasks.Distinct())
                {
                    if (context.Tasks.Find(taskId) == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    employee.EmployeesTasks.Add(new EmployeeTask { TaskId = taskId });
                }

                employees.Add(employee);
                sb.AppendLine(string.Format(SuccessfullyImportedEmployee,
                    employee.Username,
                    employee.EmployeesTasks.Count));
            }

            context.Employees.AddRange(employees);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }

    }
}