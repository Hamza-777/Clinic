using System;
using ClinicBack;

namespace ClinicFront
{
    class Program
    {
        static readonly ClinicServer newClinic = new ClinicServer();

        public static void LoggedOut()
        {
            bool keepLooping = true;
            string? username, password;

            while (keepLooping)
            {
                FrontUtils.LoggedOutOptions(); // Displays Options For Online Banking

                int choice = FrontUtils.UserInputInt("Your Choice? "); // Gets user's choice from options above

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        username = FrontUtils.UserInputString("Enter username: ");
                        password = FrontUtils.ReadPassword();
                        bool success = newClinic.LoginUser(username, password);

                        if (success)
                        {
                            FrontUtils.StartLoop("Logging you in..");
                            FrontUtils.WriteLine($"{ClinicServer.loggedInUser} successfully logged in!");
                            LoggedIn();
                        }
                        Console.WriteLine("");
                        break;
                    case 2:
                        keepLooping = false;
                        FrontUtils.StartLoop("Exiting the application...");
                        Console.Clear();
                        break;
                    default:
                        FrontUtils.InValidChoice();
                        break;
                }
            }
        }

        public static void LoggedIn()
        {
            bool loginLooping = true, success;
            string? firstname, lastname, sex, specialization;
            int age, patientId, doctorId;
            DateTime? date, currentDate = DateTime.Now;
            List<string> sexes = new List<string> { "m", "f", "others"};
            List<string> specials = new List<string> { "general", "internal medicine", "pediatrics", "orthopedics", "opthalmology"};
            List<int> doctorIds = new List<int>(), patientsList;
            List<Doctor> doctors;

            while (loginLooping)
            {
                FrontUtils.LoggedInOptions(); // Displays Options For Online Banking

                int choice = FrontUtils.UserInputInt("Your Choice? "); // Gets user's choice from options above

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        FrontUtils.WriteLine("Here's the list of all the doctors that work in this clinic: ");
                        FrontUtils.StartLoop("Collectiong info on all doctors..");
                        newClinic.ViewDoctors();
                        Console.WriteLine("");
                        break;
                    case 2:
                        Console.Clear();
                        FrontUtils.WriteLine("Enter following details to register a new patient...");
                        firstname = FrontUtils.UserInputString("\t  Enter First Name: ");
                        while (firstname == "" || firstname == null || FrontUtils.ValidateUserName(firstname) == false)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("First Name cannot be null or contain special characters!!");
                            firstname = FrontUtils.UserInputString("\t  Enter a valid First Name: ");
                        }
                        lastname = FrontUtils.UserInputString("\t  Enter Last Name: ");
                        while (lastname == "" || lastname == null || FrontUtils.ValidateUserName(lastname) == false)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Last Name cannot be null or contain special characters!!");
                            lastname = FrontUtils.UserInputString("\t  Enter a valid Last Name: ");
                        }
                        sex = FrontUtils.UserInputString("\t  Enter Sex (M/F/Others): ");
                        while (!sexes.Contains(sex.ToLower()))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Sex value has to be on of these (M / F / Others)!!");
                            sex = FrontUtils.UserInputString("\t  Enter a valid Sex: ");
                        }
                        age = FrontUtils.UserInputInt("\t  Enter Age (0 to 120): ");
                        while (age < 0 || age > 120)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Age must be between 0 to 120!!");
                            age = FrontUtils.UserInputInt("\t  Enter a valid Age: ");
                        }
                        date = FrontUtils.UserInputDate("\t  Enter Date of Birth (DD/MM/YYYY): ");
                        while (date == null || age != FrontUtils.CalculateAge((DateTime)date))
                        {

                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Date cannot be null or contradicting to the entered age!!");
                            date = FrontUtils.UserInputDate("\t  Enter a valid Date: ");
                        }
                        success = newClinic.RegisterNewPatient(firstname, lastname, sex, age, (DateTime)date);
                        if (success)
                        {
                            FrontUtils.StartLoop("Registering a new patient...");
                            FrontUtils.WriteLine($"New patient with Name {firstname + " " + lastname} registered successfully!!");
                        }
                        break;
                    case 3:
                        Console.Clear();
                        FrontUtils.WriteLine("Enter following details to make an appointment...");
                        patientsList = newClinic.PatientsList();
                        patientId = FrontUtils.UserInputInt("Enter patiend Id: ");
                        while (!patientsList.Contains(patientId))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Entered patient Id does not exist!!");
                            patientId = FrontUtils.UserInputInt("Enter patiend Id: ");
                        }
                        specialization = FrontUtils.UserInputString("Enter the required specialization (General/Internal Medicine/Pediatrics/Orthopedics/Opthalmology): ");
                        while (!specials.Contains(specialization.ToLower()))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Specialization value has to be on of these (General/Internal Medicine/Pediatrics/Orthopedics/Opthalmology)!!");
                            specialization = FrontUtils.UserInputString("\t  Enter a valid Specialization: ");
                        }
                        doctors = newClinic.ViewDoctors(specialization);
                        foreach(Doctor doctor in doctors)
                        {
                            doctorIds.Add(doctor.DoctorID);
                        }
                        doctorId = FrontUtils.UserInputInt("Enter id of the doctor you want to book an appointment with: ");
                        while (!doctorIds.Contains(doctorId))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Doctor Id must be one from the options above!!");
                            doctorId = FrontUtils.UserInputInt("Enter id of the doctor you want to book an appointment with: ");
                        }
                        date = FrontUtils.UserInputDate("\t  Enter Visit Date (DD/MM/YYYY): ");
                        while (date == null || !FrontUtils.IsValidDate((DateTime)date, (DateTime)currentDate))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Visit Date cannot be null or lesser than todays date!!");
                            date = FrontUtils.UserInputDate("\t  Enter a valid Date: ");
                        }
                        success = newClinic.MakeAppointment(patientId, doctorId, (DateTime)date);
                        if(success)
                        {
                            FrontUtils.StartLoop("Making an appointment...");
                            FrontUtils.WriteLine($"Appointment booked with doctor {doctorId} for the date {FrontUtils.ChangeFormat((DateTime)date)} successfully!!");
                        }
                        break;
                    case 4:
                        Console.Clear();
                        FrontUtils.WriteLine("Enter following details to cancel an appointment...");
                        patientsList = newClinic.PatientsList();
                        patientId = FrontUtils.UserInputInt("Enter patiend Id: ");
                        while (!patientsList.Contains(patientId))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Entered patient Id does not exist!!");
                            patientId = FrontUtils.UserInputInt("Enter patiend Id: ");
                        }
                        date = FrontUtils.UserInputDate("\t  Enter Visit Date (DD/MM/YYYY): ");
                        while (date == null || !FrontUtils.IsValidDate((DateTime)date, (DateTime)currentDate))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Visit Date cannot be null or lesser than todays date!!");
                            date = FrontUtils.UserInputDate("\t  Enter a valid Date: ");
                        }
                        success = newClinic.CancelAppointment(patientId, (DateTime)date);
                        if (success)
                        {
                            FrontUtils.StartLoop("Cancelling an appointment...");
                            FrontUtils.WriteLine($"Appointment cancelled for patient {patientId} for the date {FrontUtils.ChangeFormat((DateTime)date)} successfully!!");
                        }
                        break;
                    case 5:
                        success = newClinic.LogoutUser();
                        if (success)
                        {
                            FrontUtils.StartLoop("Logging You Out...");
                            FrontUtils.WriteLine("Logged Out Successfully");
                        }
                        loginLooping = false;
                        break;
                    default:
                        FrontUtils.InValidChoice();
                        break;
                }
            }
        }

        public static void Main(string[] args)
        {
            FrontUtils.Welcome();
            LoggedOut();
            FrontUtils.Greetings();
        }
    }
}