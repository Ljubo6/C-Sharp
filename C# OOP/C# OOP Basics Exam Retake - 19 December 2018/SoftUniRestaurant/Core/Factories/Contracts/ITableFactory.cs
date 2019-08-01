using SoftUniRestaurant.Models.Tables.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniRestaurant.Core.Factories.Contracts
{
    public interface ITableFactory
    {
        ITable CreateTable(string type,int tableNumber,int capacity);
    }
}
