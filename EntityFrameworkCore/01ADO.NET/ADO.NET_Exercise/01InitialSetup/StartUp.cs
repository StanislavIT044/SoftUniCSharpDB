using System;
using Microsoft.Data.SqlClient;

namespace _01InitialSetup
{
    class StartUp
    {
        private static string connectionString = @"Server=.;Database={0};Integrated Security=true";
        private const string dbName = "MinionsDB";
        private static SqlConnection connection;

        static void Main()
        {
            CreateDB();

            CreateTables();

            InsertIntoTables();
        }

        private static void InsertIntoTables() 
        {
            connection = new SqlConnection(String.Format(connectionString, dbName));
            connection.Open();

            using (connection)
            {
                string insertIntoTablesQuery = @"INSERT INTO Countries ([Name]) VALUES ('Bulgaria'),('England'),('Cyprus'),('Germany'),('Norway')
                                                 INSERT INTO Towns ([Name], CountryCode) VALUES ('Plovdiv', 1),('Varna', 1),('Burgas', 1),('Sofia', 1),('London', 2),('Southampton', 2),('Bath', 2),('Liverpool', 2),('Berlin', 3),('Frankfurt', 3),('Oslo', 4)                                                
                                                 INSERT INTO Minions (Name, Age, TownId) VALUES('Bob', 42, 3),('Kevin', 1, 1),('Bob ', 32, 6),('Simon', 45, 3),('Cathleen', 11, 2),('Carry ', 50, 10),('Becky', 125, 5),('Mars', 21, 1),('Misho', 5, 10),('Zoe', 125, 5),('Json', 21, 1)                                                
                                                 INSERT INTO EvilnessFactors (Name) VALUES ('Super good'),('Good'),('Bad'), ('Evil'),('Super evil') 
                                                 INSERT INTO Villains (Name, EvilnessFactorId) VALUES ('Gru',2),('Victor',1),('Jilly',3),('Miro',4),('Rosen',5),('Dimityr',1),('Dobromir',2)
                                                 INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (4,2),(1,1),(5,7),(3,5),(2,6),(11,5),(8,4),(9,7),(7,1),(1,3),(7,3),(5,3),(4,3),(1,2),(2,1),(2,7)";

                SqlCommand insertIntoTables = new SqlCommand(insertIntoTablesQuery, connection);

                try
                {
                    insertIntoTables.ExecuteNonQuery();

                    Console.WriteLine("Insert seccessfully!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void CreateTables()
        {
            connection = new SqlConnection(String.Format(connectionString, dbName));
            connection.Open();

            using (connection)
            {
                string createTablesQuery = @"CREATE TABLE Countries (Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50))
                                             CREATE TABLE Towns(Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50), CountryCode INT FOREIGN KEY REFERENCES Countries(Id))                     
                                             CREATE TABLE Minions(Id INT PRIMARY KEY IDENTITY,Name VARCHAR(30), Age INT, TownId INT FOREIGN KEY REFERENCES Towns(Id))                                    
                                             CREATE TABLE EvilnessFactors(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50))                                   
                                             CREATE TABLE Villains (Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50), EvilnessFactorId INT FOREIGN KEY REFERENCES EvilnessFactors(Id))                      
                                             CREATE TABLE MinionsVillains (MinionId INT FOREIGN KEY REFERENCES Minions(Id),VillainId INT FOREIGN KEY REFERENCES Villains(Id),CONSTRAINT PK_MinionsVillains PRIMARY KEY (MinionId, VillainId))";

                SqlCommand createTable = new SqlCommand(createTablesQuery, connection);

                try
                {
                    createTable.ExecuteNonQuery();

                    Console.WriteLine("Tables created seccessfully!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void CreateDB()
        {
            connection = new SqlConnection(String.Format(connectionString, "master"));

            connection.Open();

            using (connection)
            {
                try
                {
                    string queryCreateDB = $"CREATE DATABASE {dbName}";

                    SqlCommand createDB = new SqlCommand(queryCreateDB, connection);

                    createDB.ExecuteNonQuery();

                    Console.WriteLine("Database created seccessfully!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("There was error creating database!");
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
