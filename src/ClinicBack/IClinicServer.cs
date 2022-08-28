using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBack
{
    internal interface IClinicServer
    {
        public bool LoginUser(string username, string password);
        public bool LogoutUser();
        public List<Doctor> ViewDoctors();
        public List<Doctor> ViewDoctors(string specialization);
        public List<int> PatientsList();
        public bool RegisterNewPatient(string firstname, string lastname, string sex, int age, DateTime dob);
        public bool MakeAppointment(int patientId, int doctorId, DateTime visitDate);
        public bool CancelAppointment(int patientId, DateTime visitDate);
    }
}
