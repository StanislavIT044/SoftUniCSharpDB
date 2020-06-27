using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace _07PrintAllMinionNames
{
    class StartUp
    {
        const string connectionString = @"Server=.;Database=MinionsDB;Integrated Security=true";

        static void Main()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            
            OrderNames(connection);
        }

        private static void OrderNames(SqlConnection connection)
        {
            List<string> initialOrderOfMinions = new List<string>();
            List<string> arrangedOrderOfMinions = new List<string>();

            using (connection)
            {
                connection.Open();

                var command = new SqlCommand("SELECT Name FROM Minions", connection);

                var reader = command.ExecuteReader();

                using (reader)
                {
                    if (!reader.HasRows)
                    {
                        return;
                    }

                    while (reader.Read())
                    {
                        initialOrderOfMinions.Add(reader["Name"].ToString());
                    }
                }
            }

            while (initialOrderOfMinions.Count > 0)
            {

                arrangedOrderOfMinions.Add(initialOrderOfMinions[0]);
                initialOrderOfMinions.RemoveAt(0);

                if (initialOrderOfMinions.Count > 0)
                {
                    arrangedOrderOfMinions.Add(initialOrderOfMinions[initialOrderOfMinions.Count - 1]);
                    initialOrderOfMinions.RemoveAt(initialOrderOfMinions.Count - 1);
                }
            }

            foreach (var minion in arrangedOrderOfMinions)
            {
                Console.WriteLine(minion);
            }
        }
    }
}
