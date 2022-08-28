using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBack
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Sex { get; set; }
        public int Age { get; set; }
        public DateTime DOB { get; set; }

        public Patient() { }

        public Patient(int patientID, string? firstName, string? lastName, string? sex, int age, DateTime dOB)
        {
            PatientID = patientID;
            FirstName = firstName;
            LastName = lastName;
            Sex = sex;
            Age = age;
            DOB = dOB;
        }

        public override string ToString()
        {
            return String.Format($"Patient Id: {PatientID} \nFirst Name: {FirstName} \nLast Name: {LastName} \nSex: {Sex} \nAge: {Age} \nDate Of Birth: {Utilities.ChangeFormat(DOB)}");
        }
    }
}
