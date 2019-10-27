﻿using System;
using System.Data.SqlClient;
using _1._Initial_Setup;

namespace _4._Add_Minion
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] minionInfo = Console.ReadLine().Split();
            string[] villainInfo = Console.ReadLine().Split();

            string minionName = minionInfo[1];
            int age = int.Parse(minionInfo[2]);
            string townName = minionInfo[3];

            string villainName = villainInfo[1];

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                int? townId = GetTownName(townName, connection);

                if (townId == null)
                {
                    AddTown(connection,townName);
                }

                townId = GetTownName(townName, connection);


                AddMinion(connection,minionName,age, townId);


                int? villainId = GetVillainByName(connection, villainName);

                if (villainId == null)
                {
                    AddVillain(connection, villainName);
                }

                villainId = GetVillainByName(connection, villainName);
                int minionId = GetMinionByName(connection,minionName);

                AddMinionVillain(connection,villainId,minionId,minionName,villainName);
            }


            
        }

        private static void AddMinionVillain(SqlConnection connection, int? villainId, int minionId,string minionName,string villainName)
        {

            string insertMinionVillain = "INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@villainId, @minionId)";


            using (SqlCommand command = new SqlCommand(insertMinionVillain,connection))
            {
                command.Parameters.AddWithValue("@villainId",villainId);
                command.Parameters.AddWithValue("@minionId",minionId);
                command.ExecuteNonQuery();
            }

            Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
        }

        private static int GetMinionByName(SqlConnection connection, string minionName)
        {
            string minionQuery = "SELECT Id FROM Minions WHERE Name = @Name";
            using (SqlCommand command = new SqlCommand(minionQuery, connection))
            {
                command.Parameters.AddWithValue("@Name",minionName);
                return (int)command.ExecuteScalar();
            }
        }

        private static void AddVillain(SqlConnection connection, string villainName)
        {
            string insertVillain = "INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)";
            using (SqlCommand command = new SqlCommand(insertVillain,connection))
            {
                command.Parameters.AddWithValue("@villainName",villainName);
                command.ExecuteNonQuery();
            }

            Console.WriteLine($"Villain {villainName} was added to the database.");
        }

        private static int? GetVillainByName(SqlConnection connection, string villainName)
        {
            string villainIdQuery = "SELECT Id FROM Villains WHERE Name = @Name";

            using (SqlCommand command = new SqlCommand(villainIdQuery, connection))
            {
                command.Parameters.AddWithValue("@Name", villainName);
                return (int?)command.ExecuteScalar();
            }
        }

        private static void AddMinion(SqlConnection connection, string minionName, int age, int? townId)
        {
            string insertMinionSql = "INSERT INTO Minions (Name, Age, TownId) VALUES (@name, @age, @townId)";

            using (SqlCommand command = new SqlCommand(insertMinionSql, connection))
            {
                command.Parameters.AddWithValue("@name",minionName);
                command.Parameters.AddWithValue("@age", age);
                command.Parameters.AddWithValue("@townId", townId);

                command.ExecuteNonQuery();
            }
        }

        private static int? GetTownName(string townName, SqlConnection connection)
        {
            string townIdQuery = "SELECT Id FROM Towns WHERE Name = @townName";

            using (SqlCommand command = new SqlCommand(townIdQuery, connection))
            {
                command.Parameters.AddWithValue("@townName", townName);
                return (int?)command.ExecuteScalar();

               
            }
        }

        private static void AddTown(SqlConnection connection, string townName)
        {
            string insertTownSql = "INSERT INTO Towns (Name) VALUES (@townName)";

            using (SqlCommand command = new SqlCommand(insertTownSql,connection))
            {
                command.Parameters.AddWithValue("@townName",townName);

                command.ExecuteNonQuery();
            }

            Console.WriteLine($"Town {townName} was added to the database.");
        }
    }
}
