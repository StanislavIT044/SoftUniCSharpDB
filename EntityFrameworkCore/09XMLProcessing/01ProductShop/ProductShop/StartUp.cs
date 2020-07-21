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

    public class StartUp
    {
        const string DirectoryPath = "../../../Datasets/";

        public static void Main()
        {
            ProductShopContext db = new ProductShopContext();

            //ResetDb(db);

            //Problem01
            string inputXml = File.ReadAllText(DirectoryPath + "users.xml");
            Console.WriteLine(ImportUsers(db, inputXml));
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
    }
}