using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Dto.Export;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        private const string ImportMessage = "Successfully imported {0}";
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //var categoriesProductsJson = File.ReadAllText(@"C:\Users\Ljubo\source\repos\Projects\C# Databases Advanced Entity Framework Core\08.JavaScript Object Notation - JSON\Product Shop\ProductShop\Datasets\categories-products.json");
            //Or
            //var categoriesProductsJson = File.ReadAllText("./../../../Datasets/categories-products.json");

            //Console.WriteLine(ImportProducts(context,productsJson));

            Console.WriteLine(GetUsersWithProducts(context));
        }

        //01. Import Users
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<User[]>(inputJson);

            var validUsers = new List<User>();

            foreach (var user in users)
            {
                if (user.LastName == null || user.LastName.Length < 3)
                {
                    continue;
                }
                validUsers.Add(user);
            }
            context.Users.AddRange(validUsers);
            context.SaveChanges();
            return string.Format(ImportMessage,validUsers.Count);
        }

        //02. Import Products

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson)
                .Where(p => p.Name != null && p.Name.Trim().Length >= 3)
                .ToArray();
            context.Products.AddRange(products);
            context.SaveChanges();

            return string.Format(ImportMessage, products.Length);
        }

        //03. Import Categories

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert.DeserializeObject<List<Category>>(inputJson);

            var validCategories = new List<Category>();

            foreach (var category in categories)
            {
                if (category.Name == null ||
                    category.Name.Length < 3 ||
                    category.Name.Length > 15)
                {
                    continue;
                }

                validCategories.Add(category);
            }

            context.Categories.AddRange(validCategories);
            context.SaveChanges();
            return string.Format(ImportMessage, validCategories.Count);
        }

        //04. Import Categories and Products

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoriesProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);
            context.CategoryProducts.AddRange(categoriesProducts);
            var count = context.SaveChanges();

            return $"Successfully imported {count}";
        }  

            //05. Export Products In Range

            public static string GetProductsInRange(ProductShopContext context)
        {
            var productsInRange = context
                .Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new ProductDto
                {
                    Name = p.Name,
                    Price = p.Price,
                    Seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
                })
                .OrderBy(p => p.Price)
                .ToList();

            var json = JsonConvert.SerializeObject(productsInRange,Formatting.Indented);
            return json;
                
        }

        //06. Export Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            var filteredUsers = context
                .Users
                .Where(u => u.ProductsSold.Any(ps => ps.Buyer != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new UserDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.ProductsSold
                    .Where(p => p.Buyer != null)
                    .Select(p => new SoldProductDto
                    {
                        Name = p.Name,
                        Price = p.Price,
                        BuyerFirstName = p.Buyer.FirstName,
                        BuyerLastName = p.Buyer.LastName
                    })
                    .ToList()
                })
                .ToList();

            var json = JsonConvert.SerializeObject(filteredUsers,Formatting.Indented);
            return json;

        }

        //07. Export Categories By Products Count

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var exportCategories = context.Categories
                .OrderByDescending(c => c.CategoryProducts.Count())
                .Select(c => new CategoriesDto
                {
                    Category = c.Name,
                    ProductsCount = c.CategoryProducts.Count(),
                    AveragePrice = $@"{c.CategoryProducts
                    .Sum(p => p.Product.Price) / c.CategoryProducts.Count():F2}",
                    TotalRevenue = $"{c.CategoryProducts.Sum(p => p.Product.Price):F2}"
                })
                .ToList();

            var json = JsonConvert.SerializeObject(exportCategories, Formatting.Indented);

            return json;
        }

        //08. Export Users and Products
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context
                .Users
                .Where(u => u.ProductsSold.Any(ps => ps.Buyer != null))
                .OrderByDescending(p => p.ProductsSold.Count(ps => ps.Buyer != null))
                .Select(u => new UserWithProductsDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new SoldProductsToUserDto
                    {
                        Count = u.ProductsSold.Count(ps => ps.Buyer != null),
                        Products = u.ProductsSold
                        .Where(p => p.Buyer != null)
                        .Select(p => new SoldProductsDto
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                        .ToList()
                    }
                })
                .ToList();
            var result = new UsersAndProductsDto
            {
                UsersCount = users.Count(),
                Users = users
            };

            var json = JsonConvert.SerializeObject(result,Formatting.Indented,
                new JsonSerializerSettings 
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            return json;
        }
    }
}