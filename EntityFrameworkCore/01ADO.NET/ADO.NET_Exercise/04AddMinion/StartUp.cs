using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Text;

namespace _04AddMinion
{
    class StartUp
    {
        const string connectionString = @"Server=.;Database=MinionsDB;Integrated Security=true";

        static void Main()
        {
            string[] minionsInput = Console.ReadLine()
                .Split(": ", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
            string[] minionsInfo = minionsInput[1]
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            string[] villainsInfo = Console.ReadLine()
                .Split(": ", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
            string villainName = villainsInfo[1];

            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                connection.Open();

                string result = addMinionToDB(connection, minionsInfo, villainName);

                Console.WriteLine(result);
            }
        }

        private static string addMinionToDB(SqlConnection connection, string[] minionsInfo, string villainName)
        {
            StringBuilder output = new StringBuilder();

            string minionName = minionsInfo[0];
            string minionAge = minionsInfo[1];
            string minionTown = minionsInfo[2];
            string townId = EnsureTownExist(connection, minionTown, output);
            string villainId = EnsureVillainExist(connection, villainName, output);

            string insertMinionQuery = $@"INSERT INTO Minions([Name], Age, TownId) VALUES
                                          (@minionName, @minionAge, @townId)";

            string minionId;

            using (SqlCommand command = new SqlCommand(insertMinionQuery, connection))
            {
                command.Parameters.AddWithValue("@minionName", minionName);
                command.Parameters.AddWithValue("@minionAge", minionAge);
                command.Parameters.AddWithValue("@townId", townId);

                command.ExecuteNonQuery();

                string getMinionIdQuery = $@"SELECT Id FROM Minions WHERE [Name] = @minionName";

                using (SqlCommand getMinionIdCommand = new SqlCommand(getMinionIdQuery, connection))
                {
                    getMinionIdCommand.Parameters.AddWithValue("@minionName", minionName);

                    minionId = getMinionIdCommand.ExecuteScalar().ToString();
                }
            }

            string insertIntoMappingQuery = $@"INSERT INTO MinionsVillains (MinionId, VillainId)
                                               VALUES (@minionId, @villainId)";

            using (SqlCommand insertIntoMappingCommand = new SqlCommand(insertIntoMappingQuery, connection))
            {
                insertIntoMappingCommand.Parameters.AddWithValue("@minionId", minionId);
                insertIntoMappingCommand.Parameters.AddWithValue("@villainId", villainId);

                insertIntoMappingCommand.ExecuteNonQuery();
            }

            output.AppendLine($"Successfully added {minionName} to be minion of {villainName}.");

            return output.ToString().TrimEnd();
        }

        private static string EnsureVillainExist(SqlConnection connection, string villainName, StringBuilder output)
        {
            string getVillainIdQuery = $@"SELECT Id FROM Villains
                                           WHERE [Name] = @villainName";

            using (SqlCommand command = new SqlCommand(getVillainIdQuery, connection))
            {
                command.Parameters.AddWithValue("@villainName", villainName);

                string villainId = command.ExecuteScalar()?.ToString();

                if (villainId == null)
                {
                    string getFactorIdQuery = $@"SELECT Id FROM EvilnessFactors
                                                  WHERE [Name] = 'Evil'";//?

                    using (SqlCommand getFactorIdCommand = new SqlCommand(getFactorIdQuery, connection))
                    {
                        getFactorIdCommand.Parameters.AddWithValue("@villainName", villainName);

                        string factorId = getFactorIdCommand.ExecuteScalar()?.ToString();

                        string insertVillainQuery = $@"INSERT INTO Villains ([Name], EvilnessFactorId)
                                                       VALUES (@villainName, @factorId)";

                        using (SqlCommand insertVillain = new SqlCommand(insertVillainQuery, connection))
                        {
                            insertVillain.Parameters.AddWithValue("@villainName", villainName);
                            insertVillain.Parameters.AddWithValue("@factorId", factorId);

                            insertVillain.ExecuteNonQuery();

                            villainId = command.ExecuteScalar().ToString();

                            output.AppendLine($"Villain {villainName} was added to the database.");
                        }
                    }
                }

                return villainId;
            }
        }

        private static string EnsureTownExist(SqlConnection connection, string minionTown, StringBuilder output)
        {
            string getTownIdQuery = @$"SELECT Id FROM Towns
                                        WHERE [Name] = @townName";

            using (SqlCommand command = new SqlCommand(getTownIdQuery, connection))
            {
                command.Parameters.AddWithValue("@townName", minionTown);

                string townId = command.ExecuteScalar()?.ToString();

                if (townId == null)
                {
                    string insertTownQuery = $@"INSERT INTO Towns ([Name], CountryCode)
                                                VALUES (@minionTown, 1)";

                    using (SqlCommand insertCommand = new SqlCommand(insertTownQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@minionTown", minionTown);

                        insertCommand.ExecuteNonQuery();

                        townId = command.ExecuteScalar().ToString();

                        output.AppendLine($"Town {minionTown} waqs added to the database.");
                    }
                }

                return townId;
            }
        }
    }
}
