using System;
using System.Data.SqlClient;


namespace _1._Initial_Setup
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                string createDatabase = "CREATE DATABASE MinionsDB";
                ExecuteNonQuery(connection, createDatabase);

            }
        }

        private static void ExecuteNonQuery(SqlConnection connection, string cmdText)
        {
            using (SqlCommand commnand = new SqlCommand(cmdText, connection))
            {
                commnand.ExecuteNonQuery();
            }
        }
    }
}
