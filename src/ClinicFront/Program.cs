using System;
using ClinicBack;

namespace ClinicFront
{
    class Program
    {
        static readonly ClinicServer newClinic = new ClinicServer();

        public static void Main(string[] args)
        {
            FrontUtils.Welcome();
            string? username = FrontUtils.UserInputString("Enter username: ");
            while (username == "" || username == null)
            {
                username = FrontUtils.UserInputString("\t  Username cannot be null! Enter a valid username: ");
            }
            string password = FrontUtils.ReadPassword();
            bool success = newClinic.LoginUser(username, password);
            Console.WriteLine(success);
        }
    }
}