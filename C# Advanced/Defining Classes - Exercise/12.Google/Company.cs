namespace _12.Google
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class Company
    {
        public Company(string companyName,string department,decimal salary)
        {
            this.CompanyName = companyName;
            this.Deparment = department;
            this.Salary = salary;
        }
        public string CompanyName { get; set; }
        public string Deparment { get; set; }
        public decimal Salary { get; set; }

        public override string ToString()
        {
            return $"{this.CompanyName} {this.Deparment} {this.Salary:F2}";
        }
    }
}
