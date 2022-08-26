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
                        while (username == "" || username == null)
                        {
                            username = FrontUtils.UserInputString("Username cannot be null! Enter a valid username: ");
                        }
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
            int age = 0, patientId, doctorId;
            DateTime? date = null;

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
                        while (firstname == "" || firstname == null)
                        {
                            firstname = FrontUtils.UserInputString("\t  First Name cannot be null! Enter a valid First Name: ");
                        }
                        lastname = FrontUtils.UserInputString("\t  Enter Last Name: ");
                        while (lastname == "" || lastname == null)
                        {
                            lastname = FrontUtils.UserInputString("\t  Last Name cannot be null! Enter a valid Last Name: ");
                        }
                        sex = FrontUtils.UserInputString("\t  Enter Sex (M/F/Others): ");
                        while (sex == "" || sex == null)
                        {
                            sex = FrontUtils.UserInputString("\t  Sex cannot be null! Enter a valid Sex: ");
                        }
                        age = FrontUtils.UserInputInt("\t  Enter Age (0 to 120): ");
                        while (age < 0 || age > 120)
                        {
                            age = FrontUtils.UserInputInt("\t  Age must be between 0 to 120! Enter a valid Age: ");
                        }
                        date = FrontUtils.UserInputDate("\t  Enter Date of Birth (DD/MM/YYYY): ");
                        while (date == null)
                        {
                            date = FrontUtils.UserInputDate("\t  Date Of Birth cannot be null! Enter a valid Date: ");
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
                        patientId = FrontUtils.UserInputInt("Enter patiend Id: ");
                        specialization = FrontUtils.UserInputString("Enter the required specialization: ");
                        newClinic.ViewDoctors(specialization);
                        doctorId = FrontUtils.UserInputInt("Enter id of the doctor you want to book an appointment with: ");
                        date = FrontUtils.UserInputDate("\t  Enter Visit Date (DD/MM/YYYY): ");
                        while (date == null)
                        {
                            date = FrontUtils.UserInputDate("\t  Visit Date cannot be null! Enter a valid Date: ");
                        }
                        success = newClinic.MakeAppointment(patientId, doctorId, (DateTime)date);
                        if(success)
                        {
                            FrontUtils.StartLoop("Making an appointment...");
                            FrontUtils.WriteLine($"Appointment booked with doctor {doctorId} for the date {date} successfully!!");
                        }
                        break;
                    case 4:
                        Console.Clear();
                        FrontUtils.WriteLine("Enter following details to cancel an appointment...");
                        patientId = FrontUtils.UserInputInt("Enter patiend Id: ");
                        date = FrontUtils.UserInputDate("\t  Enter Visit Date (DD/MM/YYYY): ");
                        while (date == null)
                        {
                            date = FrontUtils.UserInputDate("\t  Visit Date cannot be null! Enter a valid Date: ");
                        }
                        success = newClinic.CancelAppointment(patientId, (DateTime)date);
                        if (success)
                        {
                            FrontUtils.StartLoop("Cancelling an appointment...");
                            FrontUtils.WriteLine($"Appointment cancelled for patient {patientId} for the date {date} successfully!!");
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