using SoftUniRestaurant.Core.Factories.Contracts;
using SoftUniRestaurant.Models.Tables;
using SoftUniRestaurant.Models.Tables.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniRestaurant.Core.Factories
{
    public class TableFactory : ITableFactory
    {
        public ITable CreateTable(string type, int tableNumber, int capacity)
        {
            ITable table = null;
            switch (type)
            {
                case "Inside":
                    table = new InsideTable(tableNumber, capacity);
                    break;
                case "Outside":
                    table = new OutsideTable(tableNumber,capacity);
                    break;
                default:
                    break;
            }
            return table;
        }
    }
}
