namespace GetInformation
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Data.SqlClient;

    public class Program
    {
        private static string connectionString = @"Server=.;Database=MinionsDB;Integrated Security=true";
        private const string dbName = "MinionsDB";
        private static SqlConnection connection;

        public static async Task Main()
        {
            await GetVillainsWithMoreThen3Minions();
            Console.WriteLine("------------------------------");
            await GetMinionsForEachVilian();
        }

        private static async Task GetMinionsForEachVilian()
        {
            int villainId = int.Parse(Console.ReadLine());
            string selectQuery =
            @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                     m.Name AS [Name], 
                     m.Age AS Age
                FROM MinionsVillains AS mv
                JOIN Minions As m ON mv.MinionId = m.Id
               WHERE mv.VillainId = @villianId
               ORDER BY m.Name";

            await OpenSqlConnectionAsync(connectionString);

            SqlCommand command = new SqlCommand(selectQuery, connection);
            command.Parameters.AddWithValue("villianId", villainId);

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                try
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["RowNum"]}. {reader["Name"]} {reader["Age"]}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static async Task GetVillainsWithMoreThen3Minions()
        {
            string selectQuery = @"SELECT v.[Name], COUNT(mv.VillainId) AS MinionsCount 
                                     FROM Villains AS v
                                     JOIN MinionsVillains AS mv ON mv.VillainId = v.Id
                                    GROUP BY v.Id, v.[Name]
                                   HAVING COUNT(mv.VillainId) > 3
                                    ORDER BY COUNT(mv.VillainId)";

            await OpenSqlConnectionAsync(connectionString);

            SqlCommand command = new SqlCommand(selectQuery, connection);

            using (connection)
            {
                try
                {
                    using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                    {
                        try
                        {
                            while (await dataReader.ReadAsync())
                            {
                                Console.WriteLine($"{dataReader["Name"]} - {dataReader["MinionsCount"]}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }

                    Console.WriteLine("Select is executed successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"There was error with select!");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static async Task OpenSqlConnectionAsync(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
        }
    }
}
