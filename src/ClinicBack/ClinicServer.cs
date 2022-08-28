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

        public void ViewDoctors(string specialization)
        {
            foreach (Doctor doctor in doctors)
            {
                if (doctor.Specialization == specialization)
                {
                    Console.WriteLine(doctor.ToString());
                }
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

        public bool MakeAppointment(int patientId, int doctorId, DateTime visitDate)
        {
            try
            {
                Doctor? requiredDoctor = Utilities.FindDoctor(doctorId, doctors);
                List<Appointment> appointmentsForEnteredDate = Utilities.FindAppointmentsOnAParticularDateForADoctor(doctorId, visitDate, appointments);
                List<string> bookedSlots = new List<string>();
                List<string> availableSlots = new List<string>();
                int currentHour = DateTime.Now.Hour;

                foreach (Appointment appointment in appointmentsForEnteredDate)
                {
                    bookedSlots.Add(appointment.AppointmentTime);
                }

                Console.WriteLine("Available Time Slots for selected date are: ");
                for(int i = int.Parse(requiredDoctor.VisitingHours[0].Split(":")[0]); i < int.Parse(requiredDoctor.VisitingHours[1].Split(":")[0]); i++)
                {
                    if(!bookedSlots.Contains(Convert.ToString(i)))
                    {
                        if(visitDate == DateTime.Now && i < currentHour)
                        {
                            availableSlots.Add(i.ToString());
                        }
                    }
                }

                if(availableSlots.Count > 0)
                {
                    foreach (string slot in availableSlots)
                    {
                        Console.Write(slot + " ");
                    }
                    Console.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Choose a time slot from the options above: ");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    string? slotNeeded = Console.ReadLine();
                    while (slotNeeded == "" || slotNeeded == null || !availableSlots.Contains(slotNeeded))
                    {
                        Console.ForegroundColor= ConsoleColor.DarkRed;
                        Console.Write("Entered slot cannot be null or anything other than the available slots!!");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("Entered slot cannot be null or anything other than the available slots: ");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        slotNeeded = Console.ReadLine();
                    }
                    Console.ResetColor();

                    Appointment appointment = new Appointment(0, visitDate, slotNeeded, doctorId, patientId);
                    Utilities.InsertDataIntoAppointments(appointment);
                    appointments = Utilities.SelectAllAppointments();
                    return true;
                } else
                {
                    throw new Exception("No slots available for the selected day!!");
                }
            }
            catch (Exception e)
            {
                Utilities.DisplayError(e);
                return false;
            }
        }

        public bool CancelAppointment(int patientId, DateTime visitDate)
        {
            try
            {
                List<Appointment> requiredAppointments = Utilities.FindAppointmentsOnAParticularDateByPatient(patientId, visitDate, appointments);
                List<int> appointmentIds = new List<int>();

                foreach(Appointment appointment in requiredAppointments)
                {
                    appointmentIds.Add(appointment.AppointmentId);
                }

                if (requiredAppointments.Count == 0)
                {
                    throw new Exception("Patient with given id has no appointments on the given date!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Here is the list of all appointments by patient on the given date: ");

                    foreach(Appointment appointment in requiredAppointments)
                    {
                        Console.WriteLine(appointment.ToString());
                    }

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Choose an appointment id from above appointments for cancellation: ");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    int appointmentId = Convert.ToInt32(Console.ReadLine());
                    while (!appointmentIds.Contains(appointmentId))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("Please choose an appointment from the options above: ");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        appointmentId = Convert.ToInt32(Console.ReadLine());
                    }
                    Console.ResetColor();

                    Utilities.DeleteAppointment(appointmentId);
                    appointments = Utilities.SelectAllAppointments();
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