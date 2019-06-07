using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Hospital
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            Dictionary<string, List<string>> departmentsAndPatients = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> doctorsAndPatients = new Dictionary<string, List<string>>();
            while ((input = Console.ReadLine()) != "Output")
            {
                string[] tokens = input.Split();
                string department = tokens[0];
                string doctorName = tokens[1] + " " + tokens[2];
                string patientName = tokens[3];

                if (!departmentsAndPatients.ContainsKey(department))
                {
                    departmentsAndPatients.Add(department,new List<string>());
                }
                departmentsAndPatients[department].Add(patientName);

                if (!doctorsAndPatients.ContainsKey(doctorName))
                {
                    doctorsAndPatients.Add(doctorName,new List<string>());
                }
                doctorsAndPatients[doctorName].Add(patientName);
            }
            input = String.Empty;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] tokens = input.Split();
                if (tokens.Length == 1)
                {
                    var currentDepartment = departmentsAndPatients[tokens[0]];
                    Console.WriteLine(string.Join(Environment.NewLine,currentDepartment));
                }
                else if (int.TryParse(tokens[1],out int result))
                {
                    string dep = tokens[0];
                    int roomNumber = result;
                    if (roomNumber > 20)
                    {
                        continue;
                    }
                    var patientInRoom = departmentsAndPatients[dep]
                        .Skip(3 *( roomNumber - 1))
                        .Take(3)
                        .OrderBy(x => x)
                        .ToList();
                    Console.WriteLine(string.Join(Environment.NewLine, patientInRoom));
                }
                else
                {
                    string doc = tokens[0] + " " + tokens[1];
                    var patientList = doctorsAndPatients[doc].OrderBy(x => x);
                    Console.WriteLine(string.Join(Environment.NewLine, patientList));
                }
            }
        }
    }
}
