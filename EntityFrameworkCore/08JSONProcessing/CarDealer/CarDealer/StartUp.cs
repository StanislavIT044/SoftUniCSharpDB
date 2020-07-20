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
        const string DirectoryPath = "../../../Datasets/Results";

        public static void Main()
        {
            CarDealerContext db = new CarDealerContext();

            //ResetDb(db);

            //Problem09
            //string inputJson = File.ReadAllText(directoryPath + "/suppliers.json");
            //Console.WriteLine(ImportSuppliers(db, inputJson));

            //Problem10
            //string inputJson = File.ReadAllText(directoryPath + "/parts.json");
            //Console.WriteLine(ImportParts(db, inputJson));

            //Problem11
            //string inputJson = File.ReadAllText(directoryPath + "/cars.json");
            //Console.WriteLine(ImportCars(db, inputJson));

            //Problem12
            //string inputJson = File.ReadAllText(directoryPath + "/customers.json");
            //Console.WriteLine(ImportCustomers(db, inputJson));

            //Problem13
            //string inputJson = File.ReadAllText(directoryPath + "/sales.json");
            //Console.WriteLine(ImportSales(db, inputJson));

            //Problem14
            //WriteInFile(GetOrderedCustomers(db), "ordered-customers.json");
            //Console.WriteLine(GetOrderedCustomers(db));

            //Problem15
            //WriteInFile(GetCarsFromMakeToyota(db), "toyota-cars.json");
            //Console.WriteLine(GetCarsFromMakeToyota(db));

            //Problem16
            //WriteInFile(GetLocalSuppliers(db), "local-suppliers.json");
            //Console.WriteLine(GetLocalSuppliers(db));

            //Problem17
            //WriteInFile(GetCarsWithTheirListOfParts(db), "cars-and-parts.json");
            //Console.WriteLine(GetCarsWithTheirListOfParts(db));

            //Problem18
            //WriteInFile(GetTotalSalesByCustomer(db), "customers-total-sales.json");
            //Console.WriteLine(GetTotalSalesByCustomer(db));

            //Problem19
            //WriteInFile(GetSalesWithAppliedDiscount(db), "sales-discounts.json");
            //Console.WriteLine(GetSalesWithAppliedDiscount(db));
        }

        private static void ResetDb(CarDealerContext db)
        {
            db.Database.EnsureDeleted();
            Console.WriteLine("Database was successfully deleted!");

            db.Database.EnsureCreated();
            Console.WriteLine("Database was successfully created!");
        }

        private static void WriteInFile(string json, string fileName)
        {
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }

            File.WriteAllText(DirectoryPath + "/" + fileName, json);
        }

        //Problem09
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(inputJson);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}.";
        }

        //Problem10
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            int suppliersCount = context.Suppliers.Count();

            List<Part> parts = JsonConvert
                .DeserializeObject<List<Part>>(inputJson)
                .Where(x => x.SupplierId <= suppliersCount)
                .ToList();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count()}.";
        }

        //Problem11
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            List<CarDTO> carsDTO = JsonConvert.DeserializeObject<List<CarDTO>>(inputJson);
            List<Car> cars = new List<Car>();
            List<PartCar> carParts = new List<PartCar>();

            foreach (var carDTO in carsDTO)
            {
                Car car = new Car()
                {
                    Make = carDTO.Make,
                    Model = carDTO.Model,
                    TravelledDistance = carDTO.TravelledDistance
                };

                cars.Add(car);

                foreach (var carPartId in carDTO.PartsId.Distinct())
                {
                    PartCar pc = new PartCar()
                    {
                        PartId = carPartId,
                        Car = car
                    };

                    carParts.Add(pc);
                }
            }
            
            context.Cars.AddRange(cars);
            context.PartCars.AddRange(carParts);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}.";
        }

        //Problem12
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(inputJson);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}.";
        }

        //Problem13
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            List<Sale> sales = JsonConvert.DeserializeObject<List<Sale>>(inputJson,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore});

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}.";
        }

        //Problem14
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

            string json = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return json;
        }

        //Problem15
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context
                .Cars
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

            string json = JsonConvert.SerializeObject(cars, Formatting.Indented);

            return json;
        }

        //Problem16
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context
                .Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToList();

            string json = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

            return json;
        }

        //Problem17
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context
                .Cars
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
                        Price = p.Part.Price.ToString("F")
                    })
                    .ToList()
                })
                .ToList();

            string json = JsonConvert.SerializeObject(cars, Formatting.Indented);

            return json;
        }

        //Problem18
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context
                .Customers
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count,
                    spentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(p => p.Part.Price))
                })
                .OrderByDescending(c => c.spentMoney)
                .ThenByDescending(c => c.boughtCars)
                .ToList();

            string json = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return json;
        }

        //Problem19
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context
                .Sales
                .Take(10)
                .Select(s => new
                {
                    car = new
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    customerName = s.Customer.Name,
                    Discount = s.Discount.ToString("F"),
                    price = s.Car.PartCars.Sum(x => x.Part.Price).ToString("F"),
                    priceWithDiscount = (s.Car.PartCars.Sum(x => x.Part.Price) * (1M - s.Discount / 100M)).ToString("F")
                })
                .ToList();

            string json = JsonConvert.SerializeObject(sales, Formatting.Indented);

            return json;
        }
    }
}