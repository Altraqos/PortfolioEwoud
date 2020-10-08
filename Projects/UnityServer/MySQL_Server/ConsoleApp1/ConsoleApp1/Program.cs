using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            MySqlConnection connection;

            try
            {
                connection = new MySqlConnection("datasource = localhost; port = 3306; username = root; password = ascent");
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    Console.WriteLine("Database opened successfully");
                }
            }

            catch
            {
                return;
            }

            Console.ReadKey();
        }
    }
}

