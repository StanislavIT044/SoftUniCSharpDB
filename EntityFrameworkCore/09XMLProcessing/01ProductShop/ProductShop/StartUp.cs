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
    using ProductShop.Dtos.Export;

    public class StartUp
    {
        const string DirectoryPath = "../../../Datasets/";

        public static void Main()
        {
            ProductShopContext db = new ProductShopContext();

            //ResetDb(db);

            //Problem01
            //string inputXml = File.ReadAllText(DirectoryPath + "users.xml");
            //Console.WriteLine(ImportUsers(db, inputXml));

            //Problem02
            //string inputXml = File.ReadAllText(DirectoryPath + "products.xml");
            //Console.WriteLine(ImportProducts(db, inputXml));

            //Problem03
            //string inputXml = File.ReadAllText(DirectoryPath + "categories.xml");
            //Console.WriteLine(ImportCategories(db, inputXml));

            //Problem04
            //string inputXml = File.ReadAllText(DirectoryPath + "categories-products.xml");
            //Console.WriteLine(ImportCategoryProducts(db, inputXml));

            //Problem05
            //Console.WriteLine(GetProductsInRange(db));
            //WriteInFile(GetProductsInRange(db), "exportedProducts.xml");

            //Problem06
            //Console.WriteLine(GetSoldProducts(db));
            //WriteInFile(GetSoldProducts(db), "soldProducts.xml");

            //Problem07
            //Console.WriteLine(GetCategoriesByProductsCount(db));
            //WriteInFile(GetCategoriesByProductsCount(db), "categoiesByProductsCount.xml");

            //Problem08
            //Console.WriteLine(GetUsersWithProducts(db));
            //WriteInFile(GetUsersWithProducts(db), "usersWithProducts.xml");
        }

        private static void ResetDb(ProductShopContext context)
        {
            context.Database.EnsureDeleted();
            Console.WriteLine("Database was deleted");

            context.Database.EnsureCreated();
            Console.WriteLine("Database was created");
        }

        private static void WriteInFile(string xml, string path)
        {
            File.WriteAllText("../../../Results/" + path, xml);
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
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            string rootElement = "Categories";
            var categoriesDto = XMLConverter.Deserializer<ImportCategoryDto>(inputXml, rootElement);

            List<Category> categories = new List<Category>();

            foreach (var c in categoriesDto)
            {
                if (c.Name != null)
                {
                    Category category = new Category
                    {
                        Name = c.Name
                    };

                    categories.Add(category);
                }
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        //Problem04
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            string rootElement = "CategoryProducts";
            var categoryProductDtos = XMLConverter.Deserializer<ImportCategoryProductDto>(inputXml, rootElement);

            List<CategoryProduct> categoryProducts = categoryProductDtos
                .Where(i => context.Categories.Any(s => s.Id == i.CategoryId) && 
                            context.Products.Any(s => s.Id == i.ProductId))
                .Select(cp => new CategoryProduct
                {
                    CategoryId =  cp.CategoryId,
                    ProductId = cp.ProductId
                })
                .ToList();

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }

        //Problem05
        public static string GetProductsInRange(ProductShopContext context)
        {
            string rootElement = "Products";

            List<ExportProductDto> products = context
                .Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new ExportProductDto
                {
                    Name = p.Name,
                    Price = p.Price,
                    Buyer = p.Buyer.FirstName + " " + p.Buyer.LastName
                })
                .OrderBy(p => p.Price)
                .Take(10)
                .ToList();

            string xml = XMLConverter.Serialize(products, rootElement);

            return xml;
        }

        //Problem06
        public static string GetSoldProducts(ProductShopContext context)
        {
            string rootElement = "Users";

            List<ExportUserSoldProductDto> usersWithProducts = context
                .Users
                .Where(up => up.ProductsSold.Any())
                .Select(up => new ExportUserSoldProductDto
                {
                    FirstName = up.FirstName,
                    LastName = up.LastName,
                    SoldProducts = up.ProductsSold
                        .Select(p => new UserProductDto
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                        .ToArray()
                })
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(5)
                .ToList();

            string xml = XMLConverter.Serialize(usersWithProducts, rootElement);

            return xml;
        }

        //Problem07
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            string rootElement = "Categories";

            List<ExportCategoryDto> categoies = context
                .Categories
                .Select(c => new ExportCategoryDto
                {
                    Name = c.Name,
                    Count = c.CategoryProducts.Count,
                    AveragePrice = c.CategoryProducts
                        .Average(p => p.Product.Price),
                    TotalRevenue = c.CategoryProducts
                        .Sum(p => p.Product.Price)
                })
                .OrderByDescending(c => c.Count)
                .ThenBy(c => c.TotalRevenue)
                .ToList();

            string xml = XMLConverter.Serialize(categoies, rootElement);

            return xml;
        }

        //Problem08
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            ExportUserDto[] usersAndProducts = context
                .Users
                .ToArray()
                .Where(p => p.ProductsSold.Any())
                .Select(u => new ExportUserDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new ExportProductCountDto
                    {
                        Count = u.ProductsSold.Count(),
                        Products = u.ProductsSold
                            .Select(p => new ExportProductSecondDto
                            {
                                Name = p.Name,
                                Price = p.Price
                            })
                            .OrderByDescending(p => p.Price)
                            .ToArray()
                    }
                })
                .OrderByDescending(x => x.SoldProducts.Count)
                .Take(10)
                .ToArray();

            ExportUserCountDto result = new ExportUserCountDto
            {
                Users = usersAndProducts,
                Count = context.Users.Count(p => p.ProductsSold.Any())
            };

            string xml = XMLConverter.Serialize(result, "Users");

            return xml;
        }
    }
}