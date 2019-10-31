using SoftUni.Data;
using System;
using System.Linq;
using System.Text;

namespace _12._Increase_Salaries
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var context = new SoftUniContext())
            {
                var result = IncreaseSalaries(context);
                Console.WriteLine(result);
            }
        }
        public static string IncreaseSalaries(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            string[] departments = new string[] { "Engineering", "Tool Design", "Marketing", "Information Services" };
            var employee = context.Employees
                            .Where(x => departments.Contains(x.Department.Name))
                            .ToList();
            foreach (var e in employee)
            {
                e.Salary *= 1.12m;
            }

            context.SaveChanges();

            var employeeWithIncreaseSalary = context.Employees
                                            .Where(x => departments.Contains(x.Department.Name))
                                            .Select(x => new
                                            {
                                                x.FirstName,
                                                x.LastName,
                                                x.Salary
                                            })
                                            .OrderBy(x => x.FirstName)
                                            .ThenBy(x => x.LastName);
            foreach (var emp in employeeWithIncreaseSalary)
            {
                sb.AppendLine($"{emp.FirstName} {emp.LastName} (${emp.Salary:F2})");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
