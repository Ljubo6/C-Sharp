using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _15._Remove_Town
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

            Town town = context.Towns.FirstOrDefault(t => t.Name == "Seattle");
            List<Address> addresses = context
                .Addresses
                .Where(a => a.TownId == town.TownId)
                .ToList();

            foreach (Employee emp in context.Employees)
            {
                if (addresses.Contains(emp.Address))
                {
                    emp.Address = null;
                }
            }

            context.Addresses.RemoveRange(addresses);
            context.Towns.Remove(town);
            context.SaveChanges();

            if (addresses.Count == 1)
            {
                sb.AppendLine($"1 address in Seattle were deleted");
            }
            else
            {
                sb.AppendLine($"{addresses.Count} addresses in Seattle were deleted");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
