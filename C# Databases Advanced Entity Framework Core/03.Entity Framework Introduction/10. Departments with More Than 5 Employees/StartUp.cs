using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace _10._Departments_with_More_Than_5_Employees
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var context = new SoftUniContext())
            {
                var result = GetDepartmentsWithMoreThan5Employees(context);
                Console.WriteLine(result);
            }
        }
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var departments = context.Departments
                             .Select(x => new
                             {
                                 DepartmentName = x.Name,
                                 ManagerFirstName = x.Manager.FirstName,
                                 ManagerLastName = x.Manager.LastName,
                                 EmployeeCount = x.Employees.Count,
                                 Employee = x.Employees.Select(e => new
                                 {
                                     EmployeeFirstName = e.FirstName,
                                     EmployeeLastName = e.LastName,
                                     EmployeeJobTitle = e.JobTitle
                                 })

                             })
                             .Where(x => x.EmployeeCount > 5)
                             .OrderBy(x => x.EmployeeCount)
                             .ThenBy(x => x.DepartmentName)
                             .ToList();
            foreach (var department in departments)
            {
                sb.AppendLine($"{department.DepartmentName} – {department.ManagerFirstName} {department.ManagerLastName}");

                foreach (var dep in department.Employee.OrderBy(x => x.EmployeeFirstName).ThenBy(x => x.EmployeeLastName))
                {
                    sb.AppendLine($"{dep.EmployeeFirstName} {dep.EmployeeLastName} - {dep.EmployeeJobTitle}");
                }
            }
            return sb.ToString().TrimEnd();
        }
    }
}
