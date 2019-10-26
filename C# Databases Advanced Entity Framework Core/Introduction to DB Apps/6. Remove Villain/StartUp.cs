using _1._Initial_Setup;
using System;
using System.Data.SqlClient;

namespace _6._Remove_Villain
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int id = int.Parse(Console.ReadLine());
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                string villainQuery = "SELECT Name FROM Villains WHERE Id = @villainId";
                string villainName;
                using (SqlCommand command = new SqlCommand(villainQuery,connection))
                {
                    command.Parameters.AddWithValue("@villainId", id);
                    villainName = (string)command.ExecuteScalar();

                    if (villainName == null)
                    {
                        Console.WriteLine("No such villain was found.");
                        return;
                    }
                }
                int affectedRows = DeleteMinionsVillainsById(connection,id);

                DeleteVillainsById(connection, id);

                Console.WriteLine($"{villainName} was deleted.");
                Console.WriteLine($"{affectedRows} was deleted.");
            }
        }

        private static void DeleteVillainsById(SqlConnection connection, int id)
        {

            string deleteVillainQuery = @"DELETE FROM Villains
      WHERE Id = @villainId";
            using (SqlCommand command = new SqlCommand(deleteVillainQuery, connection))
            {
                command.Parameters.AddWithValue("@villainId", id);
                command.ExecuteNonQuery();
            }

        }

        private static int DeleteMinionsVillainsById(SqlConnection connection, int id)
        {
            string deleteVillainQuery = @"DELETE FROM MinionsVillains 
      WHERE VillainId = @villainId";

            using (SqlCommand command = new SqlCommand(deleteVillainQuery, connection))
            {
                command.Parameters.AddWithValue("@villainId", id);
                return command.ExecuteNonQuery();
            }

        }
    }
}
