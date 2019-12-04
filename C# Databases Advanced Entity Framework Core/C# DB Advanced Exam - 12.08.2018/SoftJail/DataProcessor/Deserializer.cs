namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var departmentsAndSellsDto = JsonConvert.DeserializeObject<ImportDepartmentAndCellDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            var departments = new List<Department>();
            var cells = new List<Cell>();

            foreach (var departmentAndCellDto in departmentsAndSellsDto)
            {
                var isValid = IsValid(departmentAndCellDto) && departmentAndCellDto.Cells.All(IsValid);

                if (isValid)
                {
                    Department department = new Department
                    {
                        Name = departmentAndCellDto.Name,
                    };
                    context.Departments.Add(department);
                    context.SaveChanges();
                    foreach (var importCell in departmentAndCellDto.Cells)
                    {
                        Cell cell = new Cell
                        {
                            CellNumber = importCell.CellNumber,
                            HasWindow = importCell.HasWindow,
                            DepartmentId = department.Id
                        };
                        cells.Add(cell);
                    }                   

                    sb.AppendLine($"Imported {departmentAndCellDto.Name} with {departmentAndCellDto.Cells.Length} cells");
                }
                else
                {
                    sb.AppendLine($"Invalid Data");
                }
            }
            context.Cells.AddRange(cells);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {

            var prisonersAndMailsDto = JsonConvert.DeserializeObject<ImportPrisonerAndMailDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            List<Prisoner> validPrisoners = new List<Prisoner>();

            foreach (var dto in prisonersAndMailsDto)
            {
                var isValid = IsValid(dto) && dto.FullName != null && dto.Mails.All(IsValid);

                if (isValid)
                {
                    var prisoner = new Prisoner
                    {
                        FullName = dto.FullName,
                        Nickname = dto.Nickname,
                        Age = dto.Age,
                        IncarcerationDate = DateTime.ParseExact(dto.IncarcerationDate,"dd/MM/yyyy",CultureInfo.InvariantCulture),
                        ReleaseDate = dto.ReleaseDate == null ? (DateTime?)null : DateTime.ParseExact(dto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Bail = dto.Bail,
                        CellId = dto.CellId,
                        Mails = dto.Mails.Select(m => new Mail 
                        {
                            Description = m.Description,
                            Sender = m.Sender,
                            Address = m.Address
                        }).ToArray()

                    };
                    validPrisoners.Add(prisoner);
                    sb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
                }
                else
                {
                    sb.AppendLine("Invalid Data");
                }
            }
            context.Prisoners.AddRange(validPrisoners);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {

            var xmlSerializer = new XmlSerializer(typeof(ImportOfficerAndPrisonerXmlDto[]), new XmlRootAttribute("Officers"));
            var officersDto = (ImportOfficerAndPrisonerXmlDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();

            var validOfficers = new List<Officer>();

            List<OfficerPrisoner> officerPrisoners = new List<OfficerPrisoner>();
            foreach (var dto in officersDto)
            {

                bool isValidPosition = Enum.TryParse(dto.Position, out Position position);
                bool isValidWeapon = Enum.TryParse(dto.Weapon, out Weapon weapon);
                if (IsValid(dto) && isValidPosition && isValidWeapon)
                {
                    var officer = new Officer
                    {
                        FullName = dto.Name,
                        Salary = dto.Money,
                        Position = position,
                        Weapon = weapon,
                        DepartmentId = dto.DepartmentId,
                        OfficerPrisoners = dto.Prisoners
                        .Select(p =>
                        new OfficerPrisoner
                        {
                            PrisonerId = p.Id
                        })
                        .ToArray()
                    };
                    validOfficers.Add(officer);
                    sb.AppendLine($"Imported {officer.FullName} ({officer.OfficerPrisoners.Count} prisoners)");
                }

                else
                {
                    sb.AppendLine("Invalid Data");
                }

            }

            context.Officers.AddRange(validOfficers);
            context.SaveChanges();
           
            return sb.ToString().TrimEnd();
        }
        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();
            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}