using SoftUni.Data;
using System;
using System.Linq;
using System.Text;

namespace _08._Addresses_by_Town
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var context = new SoftUniContext())
            {
                var result = GetAddressesByTown(context);
                Console.WriteLine(result);
            }
        }
        public static string GetAddressesByTown(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var addresses = context.Addresses
                        .Select(x => new
                        {
                            EmployeeCount = x.Employees.Count,
                            TownName = x.Town.Name,
                            AddressText = x.AddressText

                        })
                        .OrderByDescending(x => x.EmployeeCount)
                        .ThenBy(x => x.TownName)
                        .ThenBy(x => x.AddressText)
                        .Take(10)
                        .ToList();
            foreach (var address in addresses)
            {
                sb.AppendLine($"{address.AddressText}, {address.TownName} - {address.EmployeeCount} employees");
            }
            return sb.ToString().TrimEnd();
        }

    }
}
