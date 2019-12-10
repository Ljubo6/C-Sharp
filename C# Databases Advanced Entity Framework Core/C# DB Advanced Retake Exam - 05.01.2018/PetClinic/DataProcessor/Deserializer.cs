namespace PetClinic.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Newtonsoft.Json;
    using PetClinic.Data;
    using PetClinic.DataProcessor.Dto.ImportDto;
    using PetClinic.Models;

    public class Deserializer
    {

        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            var animalsDto = JsonConvert.DeserializeObject<ImportAnimalAidDto[]>(jsonString);

            var sb = new StringBuilder();

            var validAnimals = new List<AnimalAid>();

            foreach (var animalDto in animalsDto)
            {
                var isExist = validAnimals.Any(x => x.Name == animalDto.Name);
                if (!IsValid(animalDto) || isExist)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                AnimalAid animalAid = new AnimalAid
                {
                    Name = animalDto.Name,
                    Price = animalDto.Price
                };
                sb.AppendLine($"Record {animalDto.Name} successfully imported.");
                validAnimals.Add(animalAid);

            }
            context.AnimalAids.AddRange(validAnimals);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            var animalsDto = JsonConvert.DeserializeObject<ImportAnimalDto[]>(jsonString);

            var sb = new StringBuilder();

            var validAnimals = new List<Animal>();

            foreach (var animalDto in animalsDto)
            {
                var isPassportExist = validAnimals.Any(a => a.Passport.SerialNumber == animalDto.Passport.SerialNumber);

                DateTime dateTime;
                bool isValidDate = DateTime.TryParseExact(animalDto.Passport.RegistrationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);


                if (!IsValid(animalDto) || !IsValid(animalDto.Passport) || isPassportExist)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }
                Animal animal = new Animal
                {
                    Name = animalDto.Name,
                    Type = animalDto.Type,
                    Age = animalDto.Age,
                    Passport = new Passport
                    {
                        SerialNumber = animalDto.Passport.SerialNumber,
                        OwnerName = animalDto.Passport.OwnerName,
                        OwnerPhoneNumber = animalDto.Passport.OwnerPhoneNumber,
                        RegistrationDate = dateTime
                    }

                };
                sb.AppendLine($"Record {animal.Name} Passport №: {animal.Passport.SerialNumber} successfully imported.");
                validAnimals.Add(animal);
            }
            context.Animals.AddRange(validAnimals);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportVetXmlDto[]), new XmlRootAttribute("Vets"));
            var vetsDto = (ImportVetXmlDto[])serializer.Deserialize(new StringReader(xmlString));
            StringBuilder sb = new StringBuilder();
            var validVets = new List<Vet>();


            foreach (var vetDto in vetsDto)
            {
                var isExist = validVets.Any(x => x.PhoneNumber == vetDto.PhoneNumber);

                if (!IsValid(vetDto) || isExist)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                Vet vet = new Vet
                {
                    Name = vetDto.Name,
                    Profession = vetDto.Profession,
                    Age = vetDto.Age,
                    PhoneNumber = vetDto.PhoneNumber
                };

                validVets.Add(vet);
                sb.AppendLine($"Record {vet.Name} successfully imported.");
            }
            context.Vets.AddRange(validVets);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportProcedureXmlDto[]), new XmlRootAttribute("Procedures"));
            var proceduresDto = (ImportProcedureXmlDto[])serializer.Deserialize(new StringReader(xmlString));
            StringBuilder sb = new StringBuilder();
            var validProcedures = new List<Procedure>();

            foreach (var procedureDto in proceduresDto)
            {
                var vet = context.Vets.SingleOrDefault(x => x.Name == procedureDto.Vet);
                var animal = context.Animals.SingleOrDefault(x => x.PassportSerialNumber == procedureDto.Animal);

                var validProcedureAnimalAids = new List<ProcedureAnimalAid>();

                bool allAidsExist = true;

                foreach (var animalAidDto in procedureDto.AnimalAids)
                {
                    var animalAid = context.AnimalAids.SingleOrDefault(x => x.Name == animalAidDto.Name);
                    if (animalAid == null
                        || validProcedureAnimalAids.Any(x => x.AnimalAid.Name == animalAidDto.Name))
                    {
                        allAidsExist = false;
                        break;
                    }

                    var animalAidProcedure = new ProcedureAnimalAid
                    {
                        AnimalAid = animalAid
                    };

                    validProcedureAnimalAids.Add(animalAidProcedure);
                }

                if (!IsValid(procedureDto) || !procedureDto.AnimalAids.All(IsValid)
                    || vet == null
                    || animal == null
                    || !allAidsExist)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                var procedure = new Procedure
                {
                    Animal = animal,
                    Vet = vet,
                    DateTime = DateTime.ParseExact(procedureDto.DateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    ProcedureAnimalAids = validProcedureAnimalAids
                };
                validProcedures.Add(procedure);

                sb.AppendLine("Record successfully imported.");
            }

            context.Procedures.AddRange(validProcedures);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }
        private static bool IsValid(object instance)
        {
            var validationContext = new ValidationContext(instance);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(instance, validationContext, validationResults, true);

            return isValid;
        }
    }
}