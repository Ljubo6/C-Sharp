using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mankind
{
    public class Student : Human
    {
        private string facultyNumber;

        public Student(string firstName, string lastName,string facultyNumber) 
            : base(firstName, lastName)
        {
            this.FacultyNumber = facultyNumber;
        }

        public string FacultyNumber
        {
            get => this.facultyNumber;
            private set
            {
                if ((value.Length < 5 || value.Length > 10) 
                    || value.All(x => char.IsLetterOrDigit(x)) == false)
                {
                    throw new ArgumentException("Invalid faculty number!");
                }
                this.facultyNumber = value;
            }
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(base.ToString());
            builder.Append($"Faculty number: {facultyNumber}");
            return builder.ToString();
        }
    }
}
