using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Linq;
using System.Text;

namespace _06._Adding_a_New_Address_and_Updating_Employee
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var context = new SoftUniContext())
            {
                var result = AddNewAddressToEmployee( context);
                Console.WriteLine(result);
            }
        }
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var address = new Address
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            context.Addresses.Add(address);

            var nakov = context.Employees.FirstOrDefault(x => x.LastName == "Nakov");

            nakov.Address = address;

            context.SaveChanges();


            StringBuilder sb = new StringBuilder();
            var employeeAddresses = context.Employees
                            .OrderByDescending(x => x.AddressId)
                            .Select(x => x.Address.AddressText)
                            .Take(10)
                            .ToList();
            foreach (var employeeAddress in employeeAddresses)
            {
                sb.AppendLine($"{employeeAddress}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
