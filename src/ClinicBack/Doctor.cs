using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBack
{
    internal class Doctor
    {
        public int DoctorID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Sex { get; set; }
        public string? Specialization { get; set; }
        public List<string> VisitingHours { get; set; }

        public Doctor() { }

        public Doctor(int doctorID, string? firstName, string? lastName, string? sex, string? specialization, string? visitingHours)
        {
            DoctorID = doctorID;
            FirstName = firstName;
            LastName = lastName;
            Sex = sex;
            Specialization = specialization;
            string[] splitted = visitingHours.Split(",");
            VisitingHours = new List<string>() { splitted[0].Split(" ")[1], splitted[1].Split(" ")[1]  };
        }

        public override string ToString()
        {
            return String.Format($"Doctor Id: {DoctorID} \nFirst Name: {FirstName} \nLast Name: {LastName} \nSex: {Sex} \nSpecialization: {Specialization} \nVisiting Hours: From {VisitingHours[0]} To {VisitingHours[1]}");
        }
    }
}
