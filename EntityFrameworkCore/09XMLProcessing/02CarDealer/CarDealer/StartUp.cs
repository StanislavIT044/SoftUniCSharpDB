namespace CarDealer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;
    using CarDealer.Data;
    using CarDealer.DataTransferObjects;
    using CarDealer.Dtos.Import;
    using CarDealer.Models;
    using Microsoft.EntityFrameworkCore.Internal;

    public class StartUp
    {
        const string DirectoryPath = "../../../Datasets/";

        public static void Main()
        {
            CarDealerContext db = new CarDealerContext();

            //ResetDb(db);

            //Problem09
            //string inputXml = File.ReadAllText(DirectoryPath + "suppliers.xml");
            //Console.WriteLine(ImportSuppliers(db, inputXml));

            //Problem10
            //string inputXml = File.ReadAllText(DirectoryPath + "parts.xml");
            //Console.WriteLine(ImportParts(db, inputXml));

            //Problem11
            //string inputXml = File.ReadAllText(DirectoryPath + "cars.xml");
            //Console.WriteLine(ImportCars(db, inputXml));

            //Problem12
            //string inputXml = File.ReadAllText(DirectoryPath + "customers.xml");
            //Console.WriteLine(ImportCustomers(db, inputXml));

            //Problem13
            //string inputXml = File.ReadAllText(DirectoryPath + "sales.xml");
            //Console.WriteLine(ImportSales(db, inputXml));
        }

        private static void ResetDb(CarDealerContext context)
        {
            context.Database.EnsureDeleted();
            Console.WriteLine("Database was deleted!");

            context.Database.EnsureCreated();
            Console.WriteLine("Database was created!");
        }

        //Problem09
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ImportSupliersDto>), 
                                            new XmlRootAttribute("Suppliers"));

            List<ImportSupliersDto> suppliersResult = (List<ImportSupliersDto>)serializer
                .Deserialize(new StringReader(inputXml));

            List<Supplier> suppliers = new List<Supplier>();

            foreach (var s in suppliersResult)
            {
                Supplier supplier = new Supplier
                {
                    Name = s.Name,
                    IsImporter = s.IsImporter
                };

                suppliers.Add(supplier);
                Console.WriteLine(supplier.Name);
            }

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}";
        }

        //Problem10
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            string rootElement = "Parts";

            XmlSerializer serializer = new XmlSerializer(typeof(List<ImportPartDto>),
                                       new XmlRootAttribute(rootElement));

            List<ImportPartDto> importPartDtos = (List<ImportPartDto>)serializer
                .Deserialize(new StringReader(inputXml));

            List<Part> parts = importPartDtos
                .Where(p => context.Suppliers.Any(s => s.Id == p.SupplierId))
                .Select(p => new Part 
                {
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    SupplierId = (int)p.SupplierId
                })
                .ToList();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}";
        }

        //Problem11
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ImportCarDto>), new XmlRootAttribute("Cars"));

            List<ImportCarDto> carsDto = (List<ImportCarDto>)serializer
                .Deserialize(new StringReader(inputXml));

            List<Car> cars = new List<Car>();
            List<PartCar> partCars = new List<PartCar>();

            foreach (var c in carsDto)
            {
                var car = new Car()
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                };

                var parts = c
                    .Parts
                    .Select(p => p.Id)
                    .Where(p => context.Parts.Any(part => part.Id == p))
                    .Distinct();

                foreach (var partId in parts)
                {
                    var carPart = new PartCar()
                    {
                        PartId = partId,
                        Car = car
                    };

                    partCars.Add(carPart);
                }

                cars.Add(car);
            }

            context.Cars.AddRange(cars);
            context.PartCars.AddRange(partCars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}";
        }

        //Problem12
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            string rootName = "Customers";

            XmlSerializer serializer = new XmlSerializer(typeof(List<ImportCustomerDto>),
                                       new XmlRootAttribute(rootName));

            List<ImportCustomerDto> importCustomerDtos = (List<ImportCustomerDto>)serializer
                .Deserialize(new StringReader(inputXml));

            List<Customer> customers = importCustomerDtos
                .Select(c => new Customer
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                })
                .ToList();

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}";
        }

        //Problem13
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            string rootName = "Sales";

            XmlSerializer serializer = new XmlSerializer(typeof(List<ImportSalesDto>),
                                       new XmlRootAttribute(rootName));

            List<ImportSalesDto> importSalesDtos = (List<ImportSalesDto>)serializer
                .Deserialize(new StringReader(inputXml));

            List<Sale> sales = importSalesDtos
                .Where(s => context.Cars.Any(c => c.Id == s.CarId))
                .Select(s => new Sale
                {
                    CarId = s.CarId,
                    CustomerId = s.CustomerId,
                    Discount = s.Discount
                })
                .ToList();

            context.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}";
        }
    }
}