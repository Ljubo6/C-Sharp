namespace _08.PetClinic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<Pet> pets = new List<Pet>();
            List<Clinic> clinics = new List<Clinic>();
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] commandInput = Console.ReadLine().Split();

                string command = commandInput[0];
                string name = string.Empty;
                string clinicName = string.Empty;
                switch (command)
                {
                    case "Create":                       
                        try
                        {
                            string typeOfCreation = commandInput[1];
                            if (typeOfCreation == "Pet")
                            {
                                name = commandInput[2];
                                int age = int.Parse(commandInput[3]);
                                string kind = commandInput[4];

                                Pet pet = new Pet(name, age, kind);

                                pets.Add(pet);
                            }
                            else
                            {
                                name = commandInput[2];
                                int roomCount = int.Parse(commandInput[3]);
                                Clinic clinic = new Clinic(name, roomCount);
                                clinics.Add(clinic);
                            }
                        }
                        catch (InvalidOperationException e)
                        {

                            Console.WriteLine(e.Message);
                        }
                        
                        break;
                    case "Add":
                        name = commandInput[1];
                        clinicName = commandInput[2];
                        Pet petToAdd = pets.FirstOrDefault(p => p.Name == name);
                        Clinic clinicToAdd = clinics.FirstOrDefault(c => c.Name == clinicName);
                        Console.WriteLine(clinicToAdd.Add(petToAdd));                        
                        break;
                    case "Release":
                        clinicName = commandInput[1];
                        Clinic clinicToRelease = clinics.FirstOrDefault(c => c.Name == clinicName);
                        Console.WriteLine(clinicToRelease.Release());
                        break;
                    case "HasEmptyRooms":
                        clinicName = commandInput[1];
                        Clinic clinikToCheck = clinics.FirstOrDefault(c => c.Name == clinicName);
                        Console.WriteLine(clinikToCheck.HasEmptyRooms);
                        break;
                    case "Print":
                        clinicName = commandInput[1];
                        Clinic clinicToPrint = clinics.FirstOrDefault(c => c.Name == clinicName);
                        if (commandInput.Length == 3)
                        {
                            int roomNumber = int.Parse(commandInput[2]);
                            Console.WriteLine(clinicToPrint.Print(roomNumber));
                        }
                        else
                        {
                            Console.WriteLine(clinicToPrint.PrintAll());
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
