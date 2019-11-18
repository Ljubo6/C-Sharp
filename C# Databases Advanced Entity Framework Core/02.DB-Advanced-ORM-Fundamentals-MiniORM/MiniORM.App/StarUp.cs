

namespace MiniORM.App
{
    using System;
    using System.Linq;
    using Data;
    using Data.Entities;
    public class StarUp
    {
        public static void Main(string[] args)
        {
            SoftUniDbContext db = new SoftUniDbContext(Configuration.ConnectionString);

            db.Employees.Add(new Employee("Gosho", "Inserted", db.Departments.First().Id, true));

            Employee employee = db.Employees.Last();
            employee.FirstName = "Modified";

            db.SaveChanges();
        }
    }
}
