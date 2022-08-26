using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBack
{
    internal class Doctor
    {
        List<int> visitingHours = new List<int>();
        public int DoctorID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Sex { get; set; }
        public string? Specialization { get; set; }
        public List<int> VisitingHours {
            get
            {
                return visitingHours ?? (visitingHours = new List<int>());
            }
            set
            {
                string[] splitted = value.ToString().Split(",");
                foreach (string s in splitted)
                {
                    visitingHours.Add(int.Parse(s.Split(" ")[1]));
                }
            }
        }

        public Doctor() { }

        public Doctor(int doctorID, string? firstName, string? lastName, string? sex, string? specialization, List<int> visitingHours)
        {
            DoctorID = doctorID;
            FirstName = firstName;
            LastName = lastName;
            Sex = sex;
            Specialization = specialization;
            VisitingHours = visitingHours;
        }
    }
}
