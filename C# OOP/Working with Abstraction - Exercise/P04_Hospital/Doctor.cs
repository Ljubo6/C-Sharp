namespace P04_Hospital
{
    using System.Collections.Generic;
    public class Doctor
    {
        private string firstName;
        private string lastName;
        private List<string> patients;



        public Doctor(string firstName,string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Patients = new List<string>();
        }
        public List<string> Patients
        {
            get { return patients; }
            set { patients = value; }
        }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

    }
}
