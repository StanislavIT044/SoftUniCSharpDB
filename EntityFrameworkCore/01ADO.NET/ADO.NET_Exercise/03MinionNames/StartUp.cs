using Microsoft.Data.SqlClient;
using System;

namespace _03MinionNames
{
    class StartUp
    {
        const string connectionString = @"Server=.;Database=MinionsDB;Integrated Security=true";

        static void Main()
        {
            int villainId = int.Parse(Console.ReadLine());

            string villainName = GetVillainName(villainId);

            if (villainName == null)
            {
                Console.WriteLine($"No villain with ID {villainId} exists in the database.");
            }
            else
            {
                Console.WriteLine($"Villain: {villainName}");

                GetMinios(villainId);
            }
        }

        private static string GetVillainName(int villianId)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                connection.Open();

                string getVillainName = @$"SELECT [Name]
                                             FROM Minions
                                            WHERE Id = {villianId}";

                SqlCommand getVillainNameCommand = new SqlCommand(getVillainName, connection);

                using (SqlDataReader villainNameReader = getVillainNameCommand.ExecuteReader())
                {
                    villainNameReader.Read();

                    try
                    {
                        string villainName = villainNameReader["Name"].ToString();

                        return villainName;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
        }

        private static void GetMinios(int villianId)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                connection.Open();

                string selectQuery = @$"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                               m.Name AS [Name], 
                                               m.Age AS Age
                                          FROM MinionsVillains AS mv
                                          JOIN Minions As m ON mv.MinionId = m.Id
                                         WHERE mv.VillainId = {villianId}
                                      ORDER BY m.Name";

                SqlCommand command = new SqlCommand(selectQuery, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["RowNum"]}. {reader["Name"]} {reader["Age"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("(no minions)");
                    }
                }
            }
        }
    }
}
