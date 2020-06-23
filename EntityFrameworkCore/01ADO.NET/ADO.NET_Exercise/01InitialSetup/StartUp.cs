using System;
using Microsoft.Data.SqlClient;

namespace _01InitialSetup
{
    class StartUp
    {
        static void Main()
        {
            string connectionString = @"Server=.;Database=master;Integrated Security=true";

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using (connection)//44:40
            {
                string queryCreateDB = "CREATE DATABASE MinionsDB";

                SqlCommand createDB = new SqlCommand(queryCreateDB, connection);

                createDB.ExecuteNonQuery();
            }
        }
    }
}
