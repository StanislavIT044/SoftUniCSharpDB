namespace ProductShop
{
    using System;
    using System.IO;
    using System.Xml;
    using XMLHelper;
    using ProductShop.Data;
    using ProductShop.Dtos.Import;
    using System.Collections.Generic;
    using ProductShop.Models;
    using System.Xml.Serialization;
    using Microsoft.EntityFrameworkCore.Internal;
    using System.Linq;

    public class StartUp
    {
        const string DirectoryPath = "../../../Datasets/";

        public static void Main()
        {
            ProductShopContext db = new ProductShopContext();

            ResetDb(db);

            //Problem01
            string inputXml = File.ReadAllText(DirectoryPath + "users.xml");
            Console.WriteLine(ImportUsers(db, inputXml));

            //Problem02
            inputXml = File.ReadAllText(DirectoryPath + "products.xml");
            Console.WriteLine(ImportProducts(db, inputXml));
        }

        private static void ResetDb(ProductShopContext context)
        {
            context.Database.EnsureDeleted();
            Console.WriteLine("Database was deleted");

            context.Database.EnsureCreated();
            Console.WriteLine("Database was created");
        }

        //Problem01
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var usersResult = XMLConverter.Deserializer<ImportUserDto>(inputXml, "Users");

            List<User> users = new List<User>();

            foreach (var u in usersResult)
            {
                User user = new User()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age
                };

                users.Add(user);
            }

            context.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count}";
        }

        //Problem02
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var usersResult = XMLConverter.Deserializer<ImportProductDto>(inputXml, "Products");

            List<Product> products = new List<Product>();

            foreach (var p in usersResult)
            {
                Product product = new Product()
                {
                    Name = p.Name,
                    Price = (decimal)p.Price,
                    BuyerId = p.BuyerId,
                    SellerId = p.SellerId                    
                };

                products.Add(product);
            }

            context.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count}";
        }

        //Problem03
        //public static string ImportCategories(ProductShopContext context, string inputXml)
        //{
        //    List<string> categories = new List<string>();

        //    return $"Successfully imported {categories.Count}";
        //}
    }
}