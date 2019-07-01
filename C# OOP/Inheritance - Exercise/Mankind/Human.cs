using System;
using System.Collections.Generic;
using System.Text;

namespace Mankind
{
    public class Human
    {
        private const int MinLengthFirstName = 4;
        private const int MinLengthLastName = 3;

        private string firstName;
        private string lastName;

        public Human(string firstName,string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
        public string FirstName
        {
            get => this.firstName;
            private set
            {
                
                this.ValidateFirstLetterIsUpper(value, nameof(this.firstName));
                this.ValidateLength(value,MinLengthFirstName, nameof(this.firstName));
                this.firstName = value;
            }
        }
        public string LastName
        {
            get => this.lastName;
            private set
            {
                this.ValidateFirstLetterIsUpper(value,nameof(this.lastName));
                this.ValidateLength(value, MinLengthLastName, nameof(this.lastName));

                this.lastName = value;
            }
        }
        private void ValidateFirstLetterIsUpper(string value, string parameterName)
        {
            if (!Char.IsUpper(value[0]))
            {
                throw new ArgumentException($"Expected upper case letter! Argument: {parameterName}");
            }
        }
        private void ValidateLength(string value, int validLength, string parameterName)
        {
            if (value.Length < validLength)
            {
                throw new ArgumentException($"Expected length at least {validLength} symbols! Argument: {parameterName}");
            }
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"First Name: {this.firstName}");
            builder.AppendLine($"Last Name: {this.lastName}");
            return builder.ToString().TrimEnd();
        }
    }
}
