using System;
using System.Collections.Generic;
using System.Linq;

namespace _04.HospitalWithClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Patient> patients = new List<Patient>();
            string input = string.Empty;
            
            while ((input = Console.ReadLine()) != "Output")
            {
                string[] tokens = input.Split();
                string department = tokens[0];
                string doctorName = tokens[1] + " " + tokens[2];
                string patientName = tokens[3];

                Patient patient = new Patient(department,doctorName,patientName);
                patients.Add(patient);

            }
            input = String.Empty;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] tokens = input.Split();
                if (tokens.Length == 1)
                {
                    var currentDepartment = patients.Where(x => x.Department == tokens[0]).ToList();
                    PrinPatients( currentDepartment);
                }
                else if (int.TryParse(tokens[1], out int result))
                {
                    string dep = tokens[0];
                    int roomNumber = result;
                    if (roomNumber > 20)
                    {
                        continue;
                    }
                    var patientInRoom = patients
                        .Where(x => x.Department == dep)
                        .Skip(3 * (roomNumber - 1))
                        .Take(3)
                        .OrderBy(x => x.PatientName)
                        .ToList();
                    PrinPatients(patientInRoom);
                }
                else
                {
                    string doc = tokens[0] + " " + tokens[1];
                    var patientList = patients.Where(x => x.DoctorName == doc).OrderBy(x => x.PatientName).ToList(); ;
                    PrinPatients(patientList);
                }
            }
        }

        private static void PrinPatients(List<Patient> patients)
        {
            foreach (var patient in patients)
            {
                Console.WriteLine(patient.PatientName);
            }
        }
    }
    public class Patient
    {
        public Patient(string department,string doctorName,string patientName)
        {
            this.Department = department;
            this.DoctorName = doctorName;
            this.PatientName = patientName;
        }
        public string Department { get; set; }
        public string DoctorName { get; set; }
        public string PatientName  { get; set; }

    }
}
