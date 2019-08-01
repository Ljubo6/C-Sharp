using SoftUniRestaurant.Core.Factories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SoftUniRestaurant.Core.Factories
{
    public class Engine : IEngine
    {
        private RestaurantController restaurantController;
        public Engine()
        {
            this.restaurantController = new RestaurantController();
        }
        public void Run()
        {
            string input = string.Empty;

            while ((input = Console.ReadLine()) != "END")
            {
                
                try
                {
                    string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    string command = tokens[0];
                    string[] arguments = tokens.Skip(1).ToArray();
                    string result = ReadCommand(command,arguments);
                    Console.WriteLine(result);
                   
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }
            Console.WriteLine(restaurantController.GetSummary());

        }

        private string ReadCommand(string command, string[] arguments)
        {
            string type = string.Empty;
            string name = string.Empty;
            decimal price = 0;
            int servingSize = 0;
            string brand = string.Empty;
            int tableNumber = 0;
            int capacity = 0;
            int numberOfPeople = 0;


            string result = string.Empty;
            switch (command)
            {
                case "AddFood":
                    type = arguments[0];
                    name = arguments[1];
                    price = decimal.Parse(arguments[2]);
                    result = restaurantController.AddFood(type,name,price);
                    break;
                case "AddDrink":
                    type = arguments[0];
                    name = arguments[1];
                    servingSize = int.Parse(arguments[2]);
                    brand = arguments[3];
                    result = restaurantController.AddDrink(type,name,servingSize,brand);
                    break;
                case "AddTable":
                    type = arguments[0];
                    tableNumber = int.Parse(arguments[1]);
                    capacity = int.Parse(arguments[2]);
                    result = restaurantController.AddTable(type,tableNumber,capacity);
                    break;
                case "ReserveTable":
                    numberOfPeople = int.Parse(arguments[0]);
                    result = restaurantController.ReserveTable(numberOfPeople);
                    break;
                case "OrderFood":
                    tableNumber = int.Parse(arguments[0]);
                    name = arguments[1];
                    result = restaurantController.OrderFood(tableNumber, name);
                    break;
                case "OrderDrink":
                    tableNumber = int.Parse(arguments[0]);
                    name = arguments[1];
                    brand = arguments[2];
                    result = restaurantController.OrderDrink(tableNumber, name,brand);
                    break;
                case "LeaveTable":
                    tableNumber = int.Parse(arguments[0]);
                    result = restaurantController.LeaveTable(tableNumber);
                    break;
                case "GetFreeTablesInfo":
                    result = restaurantController.GetFreeTablesInfo();
                    break;
                case "GetOccupiedTablesInfo":
                    result = restaurantController.GetOccupiedTablesInfo();
                    break;

                default:
                    break;
            }
            return result;
        }
    }
}
