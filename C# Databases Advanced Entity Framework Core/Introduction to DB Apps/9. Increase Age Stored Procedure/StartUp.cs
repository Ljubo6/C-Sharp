﻿using _1._Initial_Setup;
using System;
using System.Data.SqlClient;

namespace _9._Increase_Age_Stored_Procedure
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int id = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string uspGetOlderProc = "EXEC usp_GetOlder @id";

                using (SqlCommand command = new SqlCommand(uspGetOlderProc,connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        string name = (string)reader[0];
                        int age = (int)reader[1];

                        Console.WriteLine($"{name} – {age} years old");
                    }
                }
            }
        }
    }
}
