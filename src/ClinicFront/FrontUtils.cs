﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicFront
{
    internal class FrontUtils
    {
        public static void Welcome()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("##############################################################################");
            Console.WriteLine("############################ WELCOME TO THE CLINIC ###########################");
            Console.WriteLine("##############################################################################");
            Console.ResetColor();
        }

        public static void InValidChoice()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("##############################################################################");
            Console.WriteLine("                            ENTER A VALID CHOICE!!                            ");
            Console.WriteLine("##############################################################################");
            Console.ResetColor();
        }

        public static void Greetings()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("##############################################################################");
            Console.WriteLine("                             THANK YOU FOR VISITING                           ");
            Console.WriteLine("##############################################################################");
            Console.ResetColor();
        }

        public static void LoggedInOptions()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Choose One of the options below to get started...");
            Console.ResetColor();
            WriteLine("\t 1. View Doctors");
            WriteLine("\t 2. Add Patient");
            WriteLine("\t 3. Schedule Appointment");
            WriteLine("\t 4. Cancel Appointment");
            WriteLine("\t 5. Log Out");
        }

        public static void LoggedOutOptions()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Choose One of the options below to get started...");
            Console.ResetColor();
            WriteLine("\t 1. Login To Your Account");
            WriteLine("\t 2. Exit Application");
        }

        public static int UserInputInt(string message)
        {
            WriteLine(message, false);
            int res = ReadInt();
            return res;
        }

        public static string? UserInputString(string message)
        {
            WriteLine(message, false);
            string? res = ReadString();
            return res;
        }

        public static DateTime UserInputDate(string message)
        {
            WriteLine(message, false);
            DateTime res = Convert.ToDateTime(ReadString());
            return res;
        }

        public static void RunLoop(DateTime startTime, string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(message);
            while (DateTime.UtcNow - startTime < TimeSpan.FromMilliseconds(120))
            {
                DateTime newStrtTime = DateTime.UtcNow;
                while (DateTime.UtcNow - startTime < TimeSpan.FromMilliseconds(200))
                {
                    string passingTime = "Passing Time";
                }
                Console.Write(".");
            }
            Console.ResetColor();
        }

        public static void StartLoop(string message)
        {
            for (int i = 0; i < 7; i++)
            {
                Console.Clear();
                DateTime startTime = DateTime.UtcNow;
                RunLoop(startTime, message);
            }
            Console.WriteLine();
        }

        public static void WriteLine(string message, bool line = true)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            if (line)
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.Write(message);
            }
            Console.ResetColor();
        }

        public static string? ReadString()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            string? res = Console.ReadLine();
            Console.ResetColor();
            return res;
        }

        public static int ReadInt()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            int res = Convert.ToInt32(Console.ReadLine());
            Console.ResetColor();
            return res;
        }

        public static string ReadPassword()
        {
            string password = "";
            WriteLine("Enter password: ", false);
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                password += key.KeyChar;
            }

            return password;
        }

    }
}
