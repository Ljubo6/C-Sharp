namespace BookShop
{
    using BookShop.Models.Enums;
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
            using (var db = new BookShopContext())
            {
                //int date = int.Parse(Console.ReadLine());
                //DbInitializer.ResetDatabase(db);
                //var result = GetBooksReleasedBefore(db,date);

                //Console.WriteLine(result);

                int result = (RemoveBooks(db));
                Console.WriteLine(result);
                //IncreasePrices(db);
            }
        }

        //1. Age Restriction
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            //var ageRestriction = Enum.Parse<AgeRestriction>(command,true);

            var books = context.Books
                .Where(b => b.AgeRestriction.ToString().ToLower() == command.ToLower())
                .Select(b => new
                {
                    b.Title
                })
                .OrderBy(b => b.Title)
                .ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }
            return sb.ToString().TrimEnd();
        }

        //2. Golden Books
        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .Select(b => new
                {
                    b.BookId,
                    b.Title
                })
                .OrderBy(b => b.BookId)
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }
            return sb.ToString().TrimEnd();

        }
        //3. Books by Price
        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Price > 40)
                .Select(b => new
                {
                    b.Price,
                    b.Title
                })
                .OrderByDescending(b => b.Price);
            StringBuilder sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:F2}");
            }
            return sb.ToString().TrimEnd();
        }

        //4. Not Released In
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .Select(b => new
                {
                    b.BookId,
                    b.Title
                })
                .OrderBy(b => b.BookId)
                .ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }
            return sb.ToString().TrimEnd();
        }

        //5. Book Titles by Category

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var listOfCategories = input.ToLower().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

            List<string> booksTitles = new List<string>();

            foreach (var item in listOfCategories)
            {
                var books = context.Books
                    .Where(b => b.BookCategories.Any(x => x.Category.Name.ToLower() == item.ToLower()))
               .Select(b => new
               {
                   b.Title
               })
               .OrderBy(b => b.Title)
               .ToList();

                foreach (var book in books)
                {
                    booksTitles.Add(book.Title);
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (var title in booksTitles.OrderBy(x => x))
            {
                sb.AppendLine(title);
            }
            return sb.ToString().TrimEnd();
        }

        //6. Released Before Date

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime dateTime = DateTime.ParseExact(date, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var books = context.Books
                .Where(b => b.ReleaseDate < dateTime)
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price,
                    b.ReleaseDate
                })
                .OrderByDescending(b => b.ReleaseDate)
                .ToList();
            //StringBuilder sb = new StringBuilder();

            //foreach (var book in books)
            //{
            //    sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
            //}
            //return sb.ToString().TrimEnd();

            return String.Join(Environment.NewLine, books.Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:f2}"));
        }

        //7. Author Search

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var name = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FullName = a.FirstName + " " + a.LastName
                })
                .OrderBy(a => a.FullName)
                .ToList();
            return string.Join(Environment.NewLine,name.Select(a => $"{a.FullName}"));
        }

        //8. Book Search
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => new 
                {
                    b.Title
                })
                .OrderBy(b => b.Title)
                .ToList();
            // return string.Join(Environment.NewLine,books);
            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString().TrimEnd();
        }

        //9. Book Search by Author

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .Select(b => new 
                {
                    b.BookId,
                    b.Title,
                    b.Author.FirstName,
                    b.Author.LastName
                })
                .OrderBy(b => b.BookId);

            return string.Join(Environment.NewLine, books.Select(b => $"{b.Title} ({b.FirstName} {b.LastName})"));
        }

        //10. Count Books

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            return context.Books.Where(b => b.Title.Length > lengthCheck).Count();
        }

        //11. Total Book Copies
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var books = context.Authors
                .Select(a => new
                {
                    Firstname = a.FirstName,
                    LastName = a.LastName,
                    Count = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(b => b.Count)
                .ToList();
            return string.Join(Environment.NewLine,books.Select(b => $"{b.Firstname} {b.LastName} - {b.Count}"));
        }

        //12. Profit by Category

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    TotalProfit = c.CategoryBooks.Sum(x => x.Book.Copies * x.Book.Price),
                    CategoryName = c.Name
                })
                .OrderByDescending(c => c.TotalProfit)
                .ThenBy(c => c.CategoryName)
                .ToList();
            return string.Join(Environment.NewLine, categories.Select(c => $"{c.CategoryName} ${c.TotalProfit}"));
        }


        //13. Most Recent Books

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categories = context.Categories
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    CategoryName = c.Name,
                    BooksNames = c.CategoryBooks
                    .Select(b => new
                    {
                        b.Book.Title,
                        b.Book.ReleaseDate,
                        b.Book.ReleaseDate.Value.Year
                    })
                    .OrderByDescending(b => b.ReleaseDate)
                    .Take(3)
                    .ToList()
                })
                .ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var category in categories)
            {
                sb.AppendLine($"--{category.CategoryName}");
                foreach (var book in category.BooksNames)
                {
                    sb.AppendLine($"{book.Title} ({book.Year})");
                }
            }
            return sb.ToString().TrimEnd();
        }

        //14. Increase Prices

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010);
            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }

        //15. Remove Books

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books.Where(b => b.Copies < 4200);

            int count = books.Count();

            foreach (var book in books)
            {
                context.Books.Remove(book);
            }
            context.SaveChanges();
            return count;
        }
    }
}