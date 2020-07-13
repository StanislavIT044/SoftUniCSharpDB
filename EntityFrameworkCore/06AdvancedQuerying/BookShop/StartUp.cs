namespace BookShop
{
    using BookShop.Models;
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

            //string input = Console.ReadLine();

            //Problem01
            //Console.ReadLine(GetBooksByAgeRestriction(db, input)); 

            //Problem02
            //Console.ReadLine(GetGoldenBooks(db));

            //Problem03
            //Console.ReadLine(GetBooksByPrice(db));

            //Problem04
            //Console.ReadLine(GetBooksNotReleasedIn(db, input));

            //Problem05
            //Console.WriteLine(GetBooksByCategory(db, input));

            //Problem06
            //Console.WriteLine(GetBooksReleasedBefore(db, input));

            //Problem07
            //Console.WriteLine(GetAuthorNamesEndingIn(db, input));

            //Problem08
            //Console.WriteLine(GetBookTitlesContaining(db, input));

            //Problem09
            //Console.WriteLine(GetBooksByAuthor(db, input));

            //Problem10
            //Console.WriteLine(CountBooks(db, input));

            //Problem11
            //Console.WriteLine(CountCopiesByAuthor(db));

            //Problem12
            //Console.WriteLine(GetTotalProfitByCategory(db));

            //Problem13
            //Console.WriteLine(GetMostRecentBooks(db));

            //Problem14
            //IncreasePrices(db);

            //Problem15
            //RemoveBooks(db);
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
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem05
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            List<string> categories = input
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToLower())
                .ToList();

            List<string> books = context
                    .BooksCategories
                    .Where(b => categories.Contains(b.Category.Name.ToLower()))
                    .OrderBy(b => b.Book.Title)
                    .Select(b => b.Book.Title)
                    .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem06
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime dateTime = DateTime.ParseExact(date, "dd-MM-yyyy", null);

            var books = context
                .Books
                .Where(b => b.ReleaseDate < dateTime)
                .Select(b =>
                new 
                {
                    b.Title,
                    b.EditionType,
                    b.Price,
                    b.ReleaseDate
                })
                .OrderByDescending(b => b.ReleaseDate)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem07
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input) 
        {
            var authors = context
                .Authors
                .Where(b => b.FirstName.EndsWith(input.ToLower()))
                .Select(b => new {  Name = b.FirstName + " " + b.LastName })
                .OrderBy(b => b.Name)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var author in authors)
            {
                sb.AppendLine(author.Name);
            }

            return sb.ToString().TrimEnd();
        }

        //Problem08
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            List<string> books = context
                .Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem09
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context
                .Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .Select(b => 
                new 
                {
                    b.BookId,
                    b.Title,
                    b.Author.FirstName,
                    b.Author.LastName
                })
                .OrderBy(b => b.BookId)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.FirstName} {book.LastName})");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem10
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            int booksCount = context
                .Books
                .Where(b => b.Title.Length > lengthCheck)
                .Count();

            return booksCount;
        }

        //Problem11
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context
                .Authors
                .Select(a =>
                new
                {
                    AuthorName = a.FirstName + " " + a.LastName,
                    Copies = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(a => a.Copies)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var author in authors)
            {
                sb.AppendLine($"{author.AuthorName} - {author.Copies}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem12
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categories = context
                .Categories
                .Select(x => new
                {
                    x.Name,
                    Profit = x.CategoryBooks.Sum(b => b.Book.Price * b.Book.Copies)
                })
                .OrderByDescending(x => x.Profit)
                .ThenBy(x => x.Name)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var c in categories)
            {
                sb.AppendLine($"{c.Name} ${c.Profit}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem13
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var books = context.Categories
                .Select(x => new
                {
                    x.Name,
                    RecentBooks = x.CategoryBooks
                        .Where(b => b.Book.ReleaseDate != null)
                        .OrderByDescending(b => b.Book.ReleaseDate)
                        .Select(b => new 
                        { 
                            BookName = b.Book.Title,
                            RealeaseYear = b.Book.ReleaseDate.Value.Year 
                        })
                        .Take(3)
                })
                .OrderBy(x => x.Name)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var category in books)
            {
                sb.AppendLine($"--{category.Name}");

                foreach (var book in category.RecentBooks)
                {
                    sb.AppendLine($"{book.BookName} ({book.RealeaseYear})");
                }
            }

            return sb.ToString().TrimEnd();
        }

        //Problem14
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context
                .Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList();

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }

        //Problem15
        public static int RemoveBooks(BookShopContext context)
        {
            var books = context
                .Books
                .Where(b => b.Copies <= 4200)
                .ToList();

            int count = books.Count();

            context.RemoveRange(books);
            context.SaveChanges();

            return count;
        }
    }
}
