using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBack
{
    internal class Appointment
    {
        public int AppointmentId { get; set; }
        public DateOnly VisitDate { get; set; }
        public string? AppointmentTime { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        public Appointment() { }

        public Appointment(int appointmentId, DateOnly visitDate, string? appointmentTime, int doctorId, int patientId)
        {
            AppointmentId = appointmentId;
            VisitDate = visitDate;
            AppointmentTime = appointmentTime;
            DoctorId = doctorId;
            PatientId = patientId;
        }
    }
}
