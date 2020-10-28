namespace MySocialMediaDB
{
    using System;
    using MySocialMediaDB.Data;
    using Microsoft.EntityFrameworkCore;


    class StartUp
    {
        static void Main()
        {
            MySocialMediaDbContext context = new MySocialMediaDbContext();
            context.Database.Migrate();

            Console.WriteLine("Hello World!");
        }
    }
}
