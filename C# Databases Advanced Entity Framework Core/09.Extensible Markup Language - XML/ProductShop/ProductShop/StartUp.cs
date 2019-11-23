using AutoMapper;
using ProductShop.Data;
using ProductShop.Dtos.Export;
using ProductShop.Dtos.Import;
using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using static ProductShop.Dtos.Export.ExportUsersAndProductsDto;
using static ProductShop.Dtos.Export.ExportUserSoldProductDto;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(x => x.AddProfile<ProductShopProfile>());
            using (var db = new ProductShopContext())
            {
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();

                var users = File.ReadAllText("../../../Datasets/users.xml");
                var products = File.ReadAllText("../../../Datasets/products.xml");
                var categories = File.ReadAllText("../../../Datasets/categories.xml");
                var categoriesProducts = File.ReadAllText("../../../Datasets/categories-products.xml");

                //Console.WriteLine(ImportUsers(db, users));
                //Console.WriteLine(ImportProducts(db, products));
                //Console.WriteLine(ImportCategories(db, categories));
                //Console.WriteLine(ImportCategoryProducts(db, categoriesProducts));
                //Console.WriteLine(GetProductsInRange(db));
                //Console.WriteLine(GetSoldProducts(db));
                //Console.WriteLine(GetCategoriesByProductsCount(db));
                Console.WriteLine(GetUsersWithProducts(db));
            }
        }

        //01. Import Users

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportUserDto[]), new XmlRootAttribute("Users"));


            var usersDto = (ImportUserDto[])xmlSerializer.Deserialize(new StringReader(inputXml));

            List<User> users = new List<User>();

            foreach (var userDto in usersDto)
            {
                var user = Mapper.Map<User>(userDto);
                users.Add(user);
            }
            context.Users.AddRange(users);
            context.SaveChanges();
            return $"Successfully imported {users.Count}";
        }

        //02. Import Products

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportProductDto[]), new XmlRootAttribute("Products"));

            var productsDto = (ImportProductDto[])xmlSerializer.Deserialize(new StringReader(inputXml));

            List<Product> products = new List<Product>();

            foreach (var productDto in productsDto)
            {
                var product = new Product
                {
                    Name = productDto.Name,
                    Price = productDto.Price,
                    SellerId = productDto.SellerId,
                    BuyerId = productDto.BuyerId
                };
                products.Add(product);
            }
            context.Products.AddRange(products);
            context.SaveChanges();
            return $"Successfully imported {products.Count}";
        }

        //03. Import Categories

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportCategoryDto[]), new XmlRootAttribute("Categories"));

            var categoriesDto = (ImportCategoryDto[])xmlSerializer.Deserialize(new StringReader(inputXml));

            List<Category> categories = new List<Category>();

            foreach (var categoryDto in categoriesDto)
            {
                if (categoryDto.Name == null)
                {
                    continue;
                }
                var category = new Category 
                {
                    Name = categoryDto.Name
                };
                categories.Add(category);
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();
            return $"Successfully imported {categories.Count}";
        }

        //04. Import Categories and Products

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportCategoryProductDto[]), new XmlRootAttribute("CategoryProducts"));

            var categoriesProductsDto = (ImportCategoryProductDto[])xmlSerializer.Deserialize(new StringReader(inputXml));

            List<CategoryProduct> categoriesProducts = new List<CategoryProduct>();

            foreach (var categoryProductDto in categoriesProductsDto)
            {
                var product = context.Products.Find(categoryProductDto.ProductId);
                var category = context.Categories.Find(categoryProductDto.CategoryId);
                if (product == null || category == null)
                {
                    continue;
                }
                var categoryProduct = new CategoryProduct 
                {
                    ProductId = categoryProductDto.ProductId,
                    CategoryId = categoryProductDto.CategoryId
                };

                categoriesProducts.Add(categoryProduct);
            }

            context.CategoryProducts.AddRange(categoriesProducts);
            context.SaveChanges();
            return $"Successfully imported {categoriesProducts.Count}";
        }

        //05. Export Products In Range

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new ExportProductInRangeDto 
                {
                    Name = p.Name,
                    Price = p.Price,
                    Buyer = $"{p.Buyer.FirstName} {p.Buyer.LastName}"
                })
                .Take(10)
                .ToArray();
            var xmlSerializer = new XmlSerializer(typeof(ExportProductInRangeDto[]),new XmlRootAttribute("Products"));

            StringBuilder sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(sb), products,namespaces);
            return sb.ToString().TrimEnd();
        }

        //06. Export Sold Products

        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(p => p.ProductsSold.Any())
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .Select(x => new ExportUserSoldProductDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SoldProducts = x.ProductsSold.Select(p => new ProductDto
                    {
                        Name = p.Name,
                        Price = p.Price
                    })
                    .ToArray()
                })
                .Take(5)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ExportUserSoldProductDto[]), new XmlRootAttribute("Users"));

            StringBuilder sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(sb), users, namespaces);
            return sb.ToString().TrimEnd();
        }

        //07. Export Categories By Products Count

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Select(x => new ExportCategoriesByProductsCountDto
                {
                    Name = x.Name,
                    Count = x.CategoryProducts.Count(),
                    AveragePrice = x.CategoryProducts.Average(p => p.Product.Price),
                    TotalRevenue = x.CategoryProducts.Sum(p => p.Product.Price)
                })
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.TotalRevenue)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ExportCategoriesByProductsCountDto[]),new XmlRootAttribute("Categories"));
            StringBuilder sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(sb),categories,namespaces);
            return sb.ToString().TrimEnd();
        }


        //08. Export Users and Products

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context
                .Users
                .Where(x => x.ProductsSold.Any())
                .OrderByDescending(x => x.ProductsSold.Count)
                .Select(x => new ExportUsersAndProductsDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age,
                    SoldProducts = new SoldProductsDto
                    {
                        Count = x.ProductsSold.Count(),
                        Products = x.ProductsSold.Select(p => new ProductDto
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                        .OrderByDescending(n => n.Price)
                        .ToArray()
                    }
                })
                .Take(10)
                .ToArray();

            var customExport = new ExportCustomUserProductDto 
            {
                Count = context
                .Users
                .Count(x => x.ProductsSold.Any()),
                ExportUsersAndProductsDto = users
            };

            var xmlSerializer = new XmlSerializer(typeof(ExportCustomUserProductDto), new XmlRootAttribute("Users"));
            StringBuilder sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(sb), customExport, namespaces);
            return sb.ToString().TrimEnd();
        }
    }
}