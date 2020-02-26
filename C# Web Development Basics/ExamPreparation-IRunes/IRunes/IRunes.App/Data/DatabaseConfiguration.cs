using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.App.Data
{
    public class DatabaseConfiguration
    {
        public const string ConnectionString =
            @"Server=.\SQLEXPRESS;Database=IRunesDB;Trusted_Connection=True;Integrated Security=True;";
    }
}
