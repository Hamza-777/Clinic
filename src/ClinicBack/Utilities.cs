using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ClinicBack
{
    internal class Utilities
    {
        private static SqlConnection? connection;
        private static SqlCommand? command;

        private static SqlConnection setConnection()
        {
            string connectionString = "Data Source=.;Initial Catalog=ClinicDB;Integrated Security=true";
            connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public static bool FindUserWithUserNameAndPassword(string username, string password)
        {
            bool res = false;
            connection = setConnection();
            command = new SqlCommand("select * from Users where username = @username and password = @password");
            command.Connection = connection;
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                if (dr.IsDBNull(0))
                {
                    res = false;
                } else
                {
                    res = true;
                    break;
                }
            }
            dr.Close();
            return res;
        }

        public static void DisplayError(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(e.Message);
            Console.ResetColor();
        }
    }
}
