namespace BookShop
{
    using Data;
    using Initializer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            //using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);
            
            var db = new BookShopContext();

            int input = int.Parse(Console.ReadLine());

            //Problem01
            //GetBooksByAgeRestriction(db, input); 

            //Problem02
            //GetGoldenBooks(db);

            //Problem03
            //GetBooksByPrice(db);

            //Problem04
            GetBooksNotReleasedIn(db, input);
        }

        //Problem01
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            List<string> books = context
                .Books
                .Where(b => b.AgeRestriction.ToString().ToLower() == command.ToLower())
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine(book);
            }

            return sb.ToString().TrimEnd();
        }

        //Problem02
        public static string GetGoldenBooks(BookShopContext context)
        {
            List<string> books = context
                .Books
                .Where(b => b.Copies < 5000 &&
                            b.EditionType == Models.Enums.EditionType.Gold)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine(book);
            }

            return sb.ToString().TrimEnd();
        }

        //Problem03
        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context
                .Books
                .Where(b => b.Price >= 40)
                .Select(b =>
                new
                {
                    b.Title,
                    b.Price
                })
                .OrderByDescending(b => b.Price)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem04
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            List<string> books = context
                .Books
                .Where(b => b.Copies < 5000 &&
                            b.EditionType == Models.Enums.EditionType.Gold)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            return null;
        }
    }
}
