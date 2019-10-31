using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace _09._Employee_147
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var context = new SoftUniContext())
            {
                var result = GetEmployee147(context);
                Console.WriteLine(result);
            }
        }
        public static string GetEmployee147(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var employee = context.Employees
                .Select(x => new
                {
                    x.EmployeeId,
                    x.FirstName,
                    x.LastName,
                    x.JobTitle,
                    Projects = x.EmployeesProjects
                                .Select(ep => new
                                {
                                    ProjectName = ep.Project.Name
                                })
                })
                .FirstOrDefault(x => x.EmployeeId == 147);
            sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
            foreach (var project in employee.Projects.OrderBy(x => x.ProjectName))
            {
                sb.AppendLine(project.ProjectName);
            }
            return sb.ToString().TrimEnd();
        }
    }
}
