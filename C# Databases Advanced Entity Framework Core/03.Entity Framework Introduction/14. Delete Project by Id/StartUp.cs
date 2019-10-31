using SoftUni.Data;
using System;
using System.Linq;
using System.Text;

namespace _14._Delete_Project_by_Id
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var context = new SoftUniContext())
            {
                var result = RemoveTown(context);
                Console.WriteLine(result);
            }
        }
        public static string RemoveTown(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var project = context.Projects
                          .FirstOrDefault(x => x.ProjectId == 2);

            var employees = context.EmployeesProjects
                            .Where(ep => ep.ProjectId == project.ProjectId)
                            .ToList();

            context.EmployeesProjects.RemoveRange(employees);
            context.Projects.Remove(project);

            context.SaveChanges();

            var projects = context.Projects
                           .Select(x => new
                           {
                               x.Name
                           })
                           .Take(10)
                           .ToList();

            foreach (var p in projects)
            {
                sb.AppendLine(p.Name);
            }
            return sb.ToString().TrimEnd();
        }
    }
}
