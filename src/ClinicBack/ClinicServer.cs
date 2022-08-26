namespace ClinicBack
{
    public class ClinicServer : IClinicServer
    {
        public static string? loggedInUser = null;
        static List<Doctor> doctors = Utilities.SelectAllDoctors();
        static List<Patient> patients = Utilities.SelectAllPatients();
        static List<Appointment> appointments = Utilities.SelectAllAppointments();

        public bool LoginUser(string username, string password)
        {
            try
            {
                bool userExists = Utilities.FindUserWithUserNameAndPassword(username, password);

                if (userExists)
                {
                    loggedInUser = username;
                    return true;
                }
                else
                {
                    throw new Exception("You’ve entered an incorrect username or password");
                }
            }
            catch (Exception e)
            {
                Utilities.DisplayError(e);
                return false;
            }
        }

        public bool LogoutUser()
        {
            loggedInUser = null;
            return true;
        }

        public void ViewDoctors()
        {
            foreach(Doctor doctor in doctors)
            {
                Console.WriteLine(doctor.ToString());
            }
        }

        public bool RegisterNewPatient(string firstname, string lastname, string sex, int age, DateTime dob)
        {
            try
            {
                Patient? requiredPatient = Utilities.FindPatient(firstname, lastname, sex, age, dob, patients);

                if (requiredPatient != null)
                {
                    throw new Exception("Patient with given details already exists!");
                } else {
                    Patient patient = new Patient(0, firstname, lastname, sex, age, dob);
                    Utilities.InsertDataIntoPatients(patient);
                    patients = Utilities.SelectAllPatients();
                    return true;
                }
            }
            catch (Exception e)
            {
                Utilities.DisplayError(e);
                return false;
            }
        }
    }
}