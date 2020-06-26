using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace _05ChangeTownNamesCasing
{
    class Program
    {
        const string connectionString = @"Server=.;Database=MinionsDB;Integrated Security=true";

        static void Main()
        {
            string country = Console.ReadLine();

            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                connection.Open();

                UpdateTownsInCountry(connection, country);
            }
        }

        private static void PrintUpdatedTowns(SqlConnection connection, string country)
        {
            List<string> towns = new List<string>();

            string selectTownsQuery = @" SELECT t.Name 
                                           FROM Towns as t
                                           JOIN Countries AS c ON c.Id = t.CountryCode
                                          WHERE c.Name = @countryName";

            using (SqlCommand selectTowns = new SqlCommand(selectTownsQuery, connection))
            {
                selectTowns.Parameters.AddWithValue("@countryName", country);

                using (SqlDataReader reader = selectTowns.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        towns.Add($"{reader["Name"]}");
                    }
                }
            }

            Console.WriteLine($"[{string.Join(", ", towns)}]");
        }

        private static void UpdateTownsInCountry(SqlConnection connection, string country)
        {
            string countTownsQuery = @"SELECT COUNT(t.Name) 
                                             FROM Towns as t
                                             JOIN Countries AS c ON c.Id = t.CountryCode
                                            WHERE c.Name = @country";

            int townCount;

            using (SqlCommand countTowns = new SqlCommand(countTownsQuery, connection))
            {
                countTowns.Parameters.AddWithValue("@country", country);

                townCount = (int)countTowns.ExecuteScalar();
            }

            string updateTownsQuery = $@"UPDATE Towns
                                            SET Name = UPPER(Name)
                                          WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @country)";

            using (SqlCommand updateTowns = new SqlCommand(updateTownsQuery, connection))
            {
                updateTowns.Parameters.AddWithValue("@country", country);

                updateTowns.ExecuteScalar()?.ToString();

                if (townCount == 0)
                {
                    Console.WriteLine("No town names were affected.");
                }
                else
                {
                    Console.WriteLine($"{townCount} town names were affected.");

                    PrintUpdatedTowns(connection, country);
                }
            }
        }
    }
}
