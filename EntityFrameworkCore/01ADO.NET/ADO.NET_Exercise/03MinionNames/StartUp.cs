using Microsoft.Data.SqlClient;
using System;

namespace _03MinionNames
{
    class StartUp
    {
        static void Main()
        {
            int villianId = int.Parse(Console.ReadLine());

            string connectionString = @"Server=.;Database=MinionsDB;Integrated Security=true";
            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                connection.Open();

                string selectQuery = @$"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                               m.Name, 
                                               m.Age
                                          FROM MinionsVillains AS mv
                                          JOIN Minions As m ON mv.MinionId = m.Id
                                         WHERE mv.VillainId = {villianId}
                                      ORDER BY m.Name";

                SqlCommand command = new SqlCommand(selectQuery, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine(reader["Name"].ToString());

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader}");
                    }
                }
            }
        }
    }
}
