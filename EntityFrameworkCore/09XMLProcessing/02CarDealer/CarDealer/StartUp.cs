namespace CarDealer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Unicode;
    using System.Xml;
    using System.Xml.Serialization;
    using CarDealer.Data;
    using CarDealer.DataTransferObjects;
    using CarDealer.DataTransferObjects.ExportDtos;
    using CarDealer.Dtos.Export;
    using CarDealer.Dtos.Import;
    using CarDealer.Dtos.Import.Export;
    using CarDealer.Models;
    using CarDealer.XMLHelper;
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

            //Problem14
            //Console.WriteLine(GetCarsWithDistance(db));
            //WriteInFile((GetCarsWithDistance(db), "cars.xml")

            //Problem15
            //Console.WriteLine(GetCarsFromMakeBmw(db));
            //WriteInFile(GetCarsFromMakeBmw(db), "bmw-cars.xml");

            //Problem16
            //Console.WriteLine(GetLocalSuppliers(db));
            //WriteInFile(GetLocalSuppliers(db), "local-suppliers.xml");

            //Problem17
            //Console.WriteLine(GetCarsWithTheirListOfParts(db));
            //WriteInFile(GetCarsWithTheirListOfParts(db), "cars-and-parts.xml");

            //Problem18
            //Console.WriteLine(GetCarsWithTheirListOfParts(db));
            //WriteInFile(GetCarsWithTheirListOfParts(db), "customers-total-sales.xml");

            //Problem19
            //Console.WriteLine(GetSalesWithAppliedDiscount(db));
            //WriteInFile(GetSalesWithAppliedDiscount(db), "sales-discounts.xml");
        }

        private static void ResetDb(CarDealerContext context)
        {
            context.Database.EnsureDeleted();
            Console.WriteLine("Database was deleted!");

            context.Database.EnsureCreated();
            Console.WriteLine("Database was created!");
        }

        private static void WriteInFile(string xml, string fileName)
        {
            File.WriteAllText("../../../ExportsXMLs/" + fileName, xml);
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

        //Problem14
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            string rootName = "cars";

            List<ExportCarDto> cars = context.Cars
                .Where(x => x.TravelledDistance > 2000000)
                .Select(x => new ExportCarDto
                {
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                .OrderBy(x => x.Make)
                .ThenBy(x => x.Model)
                .Take(10)
                .ToList();

            string xml = XMLConverter.Serialize(cars, rootName);

            return xml;
        }

        //Problem15
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            string rootElement = "cars";

            StringBuilder sb = new StringBuilder();

            List<ExportCarBMWDto> cars = context
                .Cars
                .Where(x => x.Make.ToLower() == "bmw")
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .Select(c => new ExportCarBMWDto
                {
                    Id = c.Id,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToList();

            string xml = XMLConverter.Serialize(cars, rootElement);

            return xml;
        }

        //Problem16
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            string rootName = "suppliers";

            List<ExportLocalSupplierDto> suppliers = context
                .Suppliers
                .Where(s => !s.IsImporter)
                .Select(s => new ExportLocalSupplierDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToList();

            string xml = XMLConverter.Serialize(suppliers, rootName);

            return xml;
        }

        //Problem17
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            string rootName = "cars";

            StringWriter sw = new StringWriter();

            List<ExportCarsWithPartsDto> exportCarsWithPartsDtos = context
                .Cars
                .Select(c => new ExportCarsWithPartsDto
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.PartCars
                    .Select(pc => new CarPart 
                    { 
                        Name = pc.Part.Name, 
                        Price = pc.Part.Price 
                    })
                    .OrderByDescending(pc => pc.Price)
                    .ToList()
                })
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .ToList();

            XmlSerializer serializer = new XmlSerializer(typeof(List<ExportCarsWithPartsDto>),
                                       new XmlRootAttribute(rootName));

            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add(string.Empty, string.Empty);

            serializer.Serialize(sw, exportCarsWithPartsDtos, xmlNamespaces);

            return sw.ToString();
        }

        //Problem18
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            string rootName = "customers";

            StringWriter sw = new StringWriter();

            List<ExportCustomersDto> exportCustomers = context
                .Sales
                .Where(s => s.Customer
                    .Sales
                    .Any())
                .Select(s => new ExportCustomersDto
                {
                    FullName = s.Customer.Name,
                    BoughtCarsCount = s.Customer.Sales.Count,
                    SpentMoney = s.Car.PartCars.Sum(pc => pc.Part.Price)
                })
                .OrderByDescending(x => x.SpentMoney)
                .ToList();

            XmlSerializer serializer = new XmlSerializer(typeof(List<ExportCustomersDto>),
                                       new XmlRootAttribute(rootName));

            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add(string.Empty, string.Empty);

            serializer.Serialize(sw, exportCustomers, xmlNamespaces);

            return sw.ToString();
        }

        //Problem19
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            string rootName = "sales";

            StringWriter sw = new StringWriter();

            List<ExportSaleInfoDto> sales = context
                .Sales
                .Select(s => new ExportSaleInfoDto
                {
                    Car = new CarInfo
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    Discount = s.Discount,
                    CustomerName = s.Customer.Name,
                    Price = s.Car.PartCars.Sum(pc => pc.Part.Price),
                    PriceWithDiscount = s.Car.PartCars.Sum(pc => pc.Part.Price) - s.Car.PartCars.Sum(pc => pc.Part.Price) * s.Discount / 100.0M
                })
                .ToList();

            XmlSerializer serializer = new XmlSerializer(typeof(List<ExportSaleInfoDto>),
                                       new XmlRootAttribute(rootName));

            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add(string.Empty, string.Empty);

            serializer.Serialize(sw, sales, xmlNamespaces);

            return sw.ToString();
        }
    }
}