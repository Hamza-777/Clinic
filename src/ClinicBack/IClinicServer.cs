using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBack
{
    internal interface IClinicServer
    {
        bool LoginUser(string username, string password);
        bool LogoutUser();
        void ViewDoctors();
        public bool RegisterNewPatient(string firstname, string lastname, string sex, int age, DateTime dob);
    }
}
