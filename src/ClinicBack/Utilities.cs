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

        public static List<Doctor> SelectAllDoctors()
        {
            List<Doctor> doctorsList = new List<Doctor>();
            connection = setConnection();
            command = new SqlCommand("select * from Doctors");
            command.Connection = connection;
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                if (dr.IsDBNull(0))
                {
                    return doctorsList;
                }
                doctorsList.Add(new Doctor(dr.GetInt32(0), dr.GetFieldValue<string>(1), dr.GetFieldValue<string>(2), dr.GetFieldValue<string>(3), dr.GetFieldValue<string>(4), dr.GetFieldValue<string>(5)));
            }
            dr.Close();

            return doctorsList;
        }

        public static List<Patient> SelectAllPatients()
        {
            List<Patient> patientsList = new List<Patient>();
            connection = setConnection();
            command = new SqlCommand("select * from Patients");
            command.Connection = connection;
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                if (dr.IsDBNull(0))
                {
                    return patientsList;
                }
                patientsList.Add(new Patient(dr.GetInt32(0), dr.GetFieldValue<string>(1), dr.GetFieldValue<string>(2), dr.GetFieldValue<string>(3), dr.GetInt32(4), dr.GetDateTime(5)));
            }
            dr.Close();

            return patientsList;
        }

        public static List<Appointment> SelectAllAppointments()
        {
            List<Appointment> appointmentsList = new List<Appointment>();
            connection = setConnection();
            command = new SqlCommand("select * from Appointments");
            command.Connection = connection;
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                if (dr.IsDBNull(0))
                {
                    return appointmentsList;
                }
                appointmentsList.Add(new Appointment(dr.GetInt32(0), dr.GetDateTime(1), dr.GetFieldValue<string>(2), dr.GetInt32(3), dr.GetInt32(4)));
            }
            dr.Close();

            return appointmentsList;
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
