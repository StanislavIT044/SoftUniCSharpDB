using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        const string DirectoryPath = "../../../Datasets/Results"; 

        public static void Main()
        {
            ProductShopContext db = new ProductShopContext();

            //ResetDatabase(db);

            //Problem01
            //string inputJson = File.ReadAllText("../../../Datasets/users.json");
            //Console.WriteLine(ImportUsers(db, inputJson));

            //Problem02
            //string inputJson = File.ReadAllText("../../../Datasets/products.json");
            //Console.WriteLine(ImportProducts(db, inputJson));

            //Problem03
            //string inputJson = File.ReadAllText("../../../Datasets/categories.json");
            //Console.WriteLine(ImportCategories(db, inputJson));

            //Problem04
            //string inputJson = File.ReadAllText("../../../Datasets/categories-products.json");
            //Console.WriteLine(ImportCategoryProducts(db, inputJson));

            //Problem05
            //Console.WriteLine(GetProductsInRange(db));
            //string json = GetProductsInRange(db);
            //WriteInFile(json, "products-in-range.json");

            //Problem06
            //Console.WriteLine(GetSoldProducts(db));
            //string json = GetSoldProducts(db);
            //WriteInFile(json, "users-sold-products.json");

            //Problem07
            //Console.WriteLine(GetCategoriesByProductsCount(db));
            //string json = GetCategoriesByProductsCount(db);
            //WriteInFile(json, "categories-by-products.json");

            //Problem08
            //Console.WriteLine(GetUsersWithProducts(db));
            //string json = GetUsersWithProducts(db);
            //WriteInFile(json, "users-and-products.json");
        }

        private static void ResetDatabase(ProductShopContext db)
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

        //Problem01
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            List<User> users = JsonConvert.DeserializeObject<List<User>>(inputJson);

            context.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count}";
        }

        //Problem02
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(inputJson);

            context.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count}";
        }

        //Problem03     
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(inputJson)
                .Where(c => c.Name != null)
                .ToList();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        //Problem04
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            List<CategoryProduct> categoryProducts = JsonConvert
                .DeserializeObject<List<CategoryProduct>>(inputJson);

            context.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }

        //Problem05
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context
                .Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = p.Seller.FirstName + " " + p.Seller.LastName
                })
                .OrderBy(p => p.price);

            string json = JsonConvert.SerializeObject(products, Formatting.Indented);

            return json;
        }

        //Problem06
        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context
                .Users
                .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold
                        .Where(p => p.Buyer != null)
                        .Select(p => new
                        {
                            name = p.Name,
                            price = p.Price,
                            buyerFirstName = p.Buyer.FirstName,
                            buyerLastName = p.Buyer.LastName
                        })
                })
                .ToList();

            string json = JsonConvert.SerializeObject(users, Formatting.Indented);

            return json;
        }

        //Problem07
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context
                .Categories
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.CategoryProducts.Count(),
                    averagePrice = c.CategoryProducts.Average(p => p.Product.Price).ToString("F"),
                    totalRevenue = c.CategoryProducts.Sum(p => p.Product.Price).ToString()
                })
                .OrderByDescending(p => p.productsCount);

            string json = JsonConvert.SerializeObject(categories, Formatting.Indented);

            return json;
        }

        //Problem08
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context
                .Users
                .AsEnumerable()
                .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
                .OrderByDescending(u => u.ProductsSold.Count(c => c.Buyer != null))
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = new
                    {
                        count = u.ProductsSold.Count(b => b.Buyer != null),
                        products = u.ProductsSold.Where(b => b.Buyer != null)
                            .Select(ps => new
                            {
                                name = ps.Name,
                                price = ps.Price
                            }).ToList()
                    }
                })
                .ToList();

            var usersToReturn = new
            {
                usersCount = users.Count(),
                users
            };

            string json = JsonConvert.SerializeObject(usersToReturn, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            return json;
        }
    }
}