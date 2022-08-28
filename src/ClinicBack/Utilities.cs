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

        public static void InsertDataIntoPatients(Patient patient)
        {
            connection = setConnection();
            command = new SqlCommand("insert into Patients values(@firstname, @lastname, @sex, @age, @dob)");
            command.Connection = connection;
            command.Parameters.AddWithValue("@firstname", patient.FirstName);
            command.Parameters.AddWithValue("@lastname", patient.LastName);
            command.Parameters.AddWithValue("@sex", patient.Sex);
            command.Parameters.AddWithValue("@age", patient.Age);
            command.Parameters.AddWithValue("@dob", patient.DOB);
            command.ExecuteNonQuery();
        }

        public static void InsertDataIntoAppointments(Appointment appointment)
        {
            connection = setConnection();
            command = new SqlCommand("insert into Appointments values(@visit_date, @appointment_time, @doctor_id, @patient_id)");
            command.Connection = connection;
            command.Parameters.AddWithValue("@visit_date", appointment.VisitDate);
            command.Parameters.AddWithValue("@appointment_time", appointment.AppointmentTime);
            command.Parameters.AddWithValue("@doctor_id", appointment.DoctorId);
            command.Parameters.AddWithValue("@patient_id", appointment.PatientId);
            command.ExecuteNonQuery();
        }

        public static void DeleteAppointment(int appointmentId)
        {
            connection = setConnection();
            command = new SqlCommand("delete from Appointments where appointment_id = @appointment_id");
            command.Connection = connection;
            command.Parameters.AddWithValue("@appointment_id", appointmentId);
            command.ExecuteNonQuery();
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

        public static Patient? FindPatient(string firstname, string lastname, string sex, int age, DateTime dob, List<Patient> patients)
        {
            Patient? requiredPatient = null;

            foreach (Patient patient in patients)
            {
                if (patient.FirstName == firstname && patient.LastName == lastname && patient.Sex == sex && patient.DOB == dob)
                {
                    if(age != patient.Age)
                    {
                        requiredPatient = patient;
                    }
                    else
                    {
                        requiredPatient = patient;
                    }
                }
            }

            return requiredPatient;
        }

        public static Doctor? FindDoctor(int doctorId, List<Doctor> doctors)
        {
            Doctor? requiredDoctor = null;

            foreach (Doctor doctor in doctors)
            {
                if (doctor.DoctorID == doctorId)
                {
                    requiredDoctor = doctor;
                }
            }

            return requiredDoctor;
        }

        public static List<Appointment> FindAppointmentsOnAParticularDateByPatient(int patientId, DateTime date, List<Appointment> appointments)
        {
            List<Appointment> requiredAppointments = new List<Appointment>();

            foreach (Appointment appointment in appointments)
            {
                if (appointment.PatientId == patientId && appointment.VisitDate == date)
                {
                    requiredAppointments.Add(appointment);
                }
            }

            return requiredAppointments;
        }

        public static List<Appointment> FindAppointmentsOnAParticularDateForADoctor(int doctorId, DateTime date, List<Appointment> appointments)
        {
            List<Appointment> requiredAppointments = new List<Appointment>();

            foreach (Appointment appointment in appointments)
            {
                if (appointment.DoctorId == doctorId && appointment.VisitDate == date)
                {
                    requiredAppointments.Add(appointment);
                }
            }

            return requiredAppointments;
        }

        public static void DisplayError(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(e.Message);
            Console.ResetColor();
        }

        public static string ChangeFormat(DateTime date)
        {
            return String.Join("/", date.ToShortDateString().Split("-"));
        }
    }
}
