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
        const string directoryPath = "../../../Datasets";

        public static void Main()
        {
            CarDealerContext db = new CarDealerContext();

            ResetDb(db);

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
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.WriteAllText(directoryPath + "/" + fileName, json);
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
    }
}