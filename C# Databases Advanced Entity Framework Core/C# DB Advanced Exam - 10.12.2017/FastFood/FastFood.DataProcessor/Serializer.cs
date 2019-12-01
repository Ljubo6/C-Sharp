using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using FastFood.Data;
using FastFood.DataProcessor.Dto.Export;
using FastFood.Models.Enums;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace FastFood.DataProcessor
{
	public class Serializer
	{
		public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
		{
            var orderTypeAsEnum = Enum.Parse<OrderType>(orderType);
            var employee = context.Employees
                .ToArray()
                .Where(x => x.Name == employeeName)
                .Select(x => new
                {
                    Name = x.Name,
                    Orders = x.Orders.Where(s => s.Type == orderTypeAsEnum)
                    .Select(c => new
                    {
                        Customer = c.Customer,
                        Items = c.OrderItems.Select(i => new
                        {
                            Name = i.Item.Name,
                            Price = i.Item.Price,
                            Quantity = i.Quantity
                        })
                        .ToArray(),
                        TotalPrice = c.TotalPrice
                    })
                    .OrderByDescending(t => t.TotalPrice)
                    .ThenByDescending(z => z.Items.Length)
                    .ToArray(),
                    TotalMade = x.Orders.Where(s => s.Type == orderTypeAsEnum)
                    .Sum(p => p.TotalPrice)

                })
                .FirstOrDefault();


            var json = JsonConvert.SerializeObject(employee, Formatting.Indented);

            return json;
        }

		public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
		{
            var categoriesArray = categoriesString.Split(',');

            var categories = context.Categories
                .Where(x => categoriesArray.Any(s => s == x.Name))
                .Select(s => new ExportCategoryDto
                {
                    Name = s.Name,
                    MostPopularItem = s.Items
                    .Select(z => new ExportMostPopularItemDto
                    {
                        Name = z.Name,
                        TimesSold = z.OrderItems.Sum(x => x.Quantity),
                        TotalMade = z.OrderItems.Sum(x => x.Item.Price * x.Quantity)
                    })
                    .OrderByDescending(x => x.TotalMade)
                    .ThenByDescending(x => x.TimesSold)
                    .FirstOrDefault()

                })
                .OrderByDescending(x => x.MostPopularItem.TotalMade)
                .ThenByDescending(x => x.MostPopularItem.TimesSold)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ExportCategoryDto[]), new XmlRootAttribute("Categories"));

            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(sb), categories, namespaces);

            return sb.ToString().TrimEnd();
        }
	}
}