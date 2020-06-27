using Microsoft.Data.SqlClient;
using System;

namespace _06RemoveVillain
{
    class StartUp
    {
        const string connectionString = @"Server=.;Database=MinionsDB;Integrated Security=true";

        static void Main()
        {
            int villainId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string villainName = ExistVillain(connection, villainId);

                if (villainName != String.Empty)
                {
                    Console.WriteLine($"{villainName} was deleted.");

                    DeleteMinions(connection, villainId);

                    DeleteVillain(connection, villainId);
                }
                else
                {
                    Console.WriteLine("No such villain was found.");
                }
            }
        }

        private static void DeleteVillain(SqlConnection connection, int villainId)
        {
            string deleteVillainQuery = @"DELETE FROM Villains
                                           WHERE Id = @villainId";

            using (SqlCommand deleteVillain = new SqlCommand(deleteVillainQuery, connection))
            {
                deleteVillain.Parameters.AddWithValue("@villainId", villainId);

                deleteVillain.ExecuteNonQuery();
            }
        }

        private static void DeleteMinions(SqlConnection connection, int villainId)
        {
            string minionsCount;

            string countMinionsQuery = @"SELECT COUNT(VillainId) AS num
                                           FROM MinionsVillains
                                          GROUP BY VillainId
                                         HAVING VillainId = @villainId";

            using (SqlCommand countMinions = new SqlCommand(countMinionsQuery, connection))
            {
                countMinions.Parameters.AddWithValue("@villainId", villainId);

                using (SqlDataReader reader = countMinions.ExecuteReader())
                {
                    reader.Read();

                    minionsCount = reader["num"].ToString();
                }
            }

            string deleteMinionsQuery = @"DELETE FROM MinionsVillains 
                                           WHERE VillainId = @villainId";

            using (SqlCommand deleteMinions = new SqlCommand(deleteMinionsQuery, connection))
            {
                deleteMinions.Parameters.AddWithValue("@villainId", villainId);

                deleteMinions.ExecuteNonQuery();
            }

            Console.WriteLine($"{minionsCount} minions were released.");
        }

        private static string ExistVillain(SqlConnection connection, int villainId)
        {
            string selectVillainQuery = @"SELECT Name FROM Villains WHERE Id = @villainId";

            using (SqlCommand selectVillain = new SqlCommand(selectVillainQuery, connection))
            {
                selectVillain.Parameters.AddWithValue("@villainId", villainId);

                using (SqlDataReader reader = selectVillain.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader["Name"]?.ToString();
                    }

                    return String.Empty;
                }
            }
        }
    }
}
