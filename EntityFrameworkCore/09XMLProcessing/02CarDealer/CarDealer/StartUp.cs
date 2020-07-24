namespace CarDealer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using CarDealer.Data;
    using CarDealer.DataTransferObjects;
    using CarDealer.Models;

    public class StartUp
    {
        const string DirectoryPath = "../../../Datasets/";

        public static void Main()
        {
            CarDealerContext db = new CarDealerContext();

            ResetDb(db);

            //Problem09
            string inputXml = File.ReadAllText(DirectoryPath + "suppliers.xml");
            Console.WriteLine(ImportSuppliers(db, inputXml));
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
    }
}