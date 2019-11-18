using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var db = new CarDealerContext())
            {
                //db.Database.EnsureCreated();
                //string suppliers = File.ReadAllText("./../../../Datasets/suppliers.json");
                //string parts = File.ReadAllText("./../../../Datasets/parts.json");
                //string cars = File.ReadAllText("./../../../Datasets/cars.json");
                //string customers = File.ReadAllText("./../../../Datasets/customers.json");
                //string sales = File.ReadAllText("./../../../Datasets/sales.json");

                //Console.WriteLine(ImportSuppliers(db,suppliers));
                //Console.WriteLine(ImportParts(db,parts));
                //Console.WriteLine(ImportCars(db,cars));
                //Console.WriteLine(ImportCustomers(db,customers));
                //Console.WriteLine(ImportSales(db,sales));
                //Console.WriteLine(GetOrderedCustomers(db));
                //Console.WriteLine(GetCarsFromMakeToyota(db));
                //Console.WriteLine(GetLocalSuppliers(db));
                //Console.WriteLine(GetCarsWithTheirListOfParts(db));
                //Console.WriteLine(GetTotalSalesByCustomer(db));
                Console.WriteLine(GetSalesWithAppliedDiscount(db));
            }
        }
        
        //09. Import Suppliers

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson);
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
            return $"Successfully imported {suppliers.Length}.";
        }

        //10. Import Parts

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var parts = JsonConvert.DeserializeObject<Part[]>(inputJson)
                .Where(p => context.Suppliers.Any(s => s.Id == p.SupplierId)).ToArray();
            context.Parts.AddRange(parts);
            context.SaveChanges();
            return $"Successfully imported {parts.Length}.";
        }

        //11. Import Cars

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var carsDto = JsonConvert.DeserializeObject<ImportCarDto[]>(inputJson);
            foreach (var carDto in carsDto)
            {
                Car car = new Car 
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                    TravelledDistance = carDto.TravelledDistance
                };
                context.Add(car);
                foreach (var partId in carDto.PartsId)
                {
                    PartCar partCar = new PartCar 
                    {
                        CarId = car.Id,
                        PartId = partId
                    };
                    if (car.PartCars.FirstOrDefault(p => p.PartId == partId) == null )
                    {
                        context.PartCars.Add(partCar);
                    }
                }
            }
            context.SaveChanges();
            return $"Successfully imported {carsDto.Length}.";
        }

        //12. Import Customers

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<Customer[]>(inputJson);
            context.Customers.AddRange(customers);
            context.SaveChanges();
            return $"Successfully imported {customers.Length}.";
        }

        //13. Import Sales

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<Sale[]>(inputJson);
            context.Sales.AddRange(sales);
            context.SaveChanges();
            return $"Successfully imported {sales.Length}.";
        }

        //14. Export Ordered Customers

        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context
                .Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(c => new
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy"),
                    IsYoungDriver = c.IsYoungDriver
                })
                .ToList();

            var json = JsonConvert.SerializeObject(customers,Formatting.Indented);

            return json;
        }

        //15. Export Cars From Make Toyota

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new 
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToList();

            var json = JsonConvert.SerializeObject(cars,Formatting.Indented);
            return json;
        }

        //16. Export Local Suppliers

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count()
                })
                .ToList();

            var json = JsonConvert.SerializeObject(suppliers,Formatting.Indented);
            return json;
        }

        //17. Export Cars With Their List Of Parts

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(c => new
                {
                    car = new
                    {
                        Make = c.Make,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance
                    },
                    parts = c.PartCars
                    .Select(p => new
                    {
                        Name = p.Part.Name,
                        Price = $"{p.Part.Price:F2}"
                    }).ToList()
                })
                .ToList();

            var json = JsonConvert.SerializeObject(cars,Formatting.Indented);
            return json;
        }

        //18. Export Total Sales By Customer

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Count() >= 1)
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count(),
                    spentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(pc => pc.Part.Price))
                })
                .OrderByDescending(x => x.spentMoney)
                .ThenByDescending(x => x.boughtCars)
                .ToList();

            var json = JsonConvert.SerializeObject(customers,Formatting.Indented);
            return json;

        }

        //19. Export Sales With Applied Discount

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Select(s => new
                {
                    car = new 
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    customerName = s.Customer.Name,
                    Discount = $"{s.Discount:F2}",
                    price = $"{s.Car.PartCars.Sum(pc => pc.Part.Price):F2}",
                    priceWithDiscount = $"{s.Car.PartCars.Sum(pc => pc.Part.Price) * ((100 - s.Discount) / 100):F2}"
                })
                .Take(10)
                .ToList();
            var json = JsonConvert.SerializeObject(sales, Formatting.Indented);
            return json;
        }
    }
}