using System;
using System.Collections.Generic;
using System.Text;

namespace Mankind
{
    public class Worker : Human
    {
        private const string ErrorMessage = "Expected value mismatch! Argument: {0}";
        private double weekSalary;
        private double workHoursPerDay;

        public Worker(string firstName, string lastName,double weekSalary,double workHoursPerDay) 
            : base(firstName, lastName)
        {
            this.WeekSalary = weekSalary;
            this.WorkHoursPerDay = workHoursPerDay;
        }

        public double WeekSalary
        {
            get => weekSalary;
            private set
            {
                if (value <= 10)
                {
                    throw new ArgumentException(string.Format(ErrorMessage,nameof(this.weekSalary)));
                }
                this.weekSalary = value;
            }
        }
        public double WorkHoursPerDay
        {
            get => this.workHoursPerDay;
            private set
            {
                if (value < 1 || value > 12)
                {
                    throw new ArgumentException(string.Format(ErrorMessage, nameof(this.workHoursPerDay)));
                }
                this.workHoursPerDay = value;
            }
        }
        public double CalculateSalaryPerHour()
        {
            return this.WeekSalary / (this.WorkHoursPerDay * 5);
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(base.ToString());
            builder.AppendLine($"Week Salary: {this.weekSalary:F2}");
            builder.AppendLine($"Hours per day: {this.workHoursPerDay:F2}");
            builder.AppendLine($"Salary per hour: {this.CalculateSalaryPerHour():F2}");
            return builder.ToString().TrimEnd();
        }
    }
}
