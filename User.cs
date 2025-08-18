using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PRG281Assignment
{
    public enum AccessLevel
    {
        ReadOnly = 1,
        FullAccess
    }

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public AccessLevel Access { get; set; }
        public static int UserCount = 0;

        public User(string username, string password, AccessLevel access)
        {
            Username = username;
            Password = password;
            Access = access;
            UserCount++;
        }

        public static User CreateUser()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTER NEW USER ===");

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.WriteLine("Access Level:");
            Console.WriteLine("1. Read-Only (Nurse)");
            Console.WriteLine("2. Full Access (Doctor)");
            Console.Write("Select (1-2): ");

            AccessLevel access;
            while (!Enum.TryParse(Console.ReadLine(), out access) || !Enum.IsDefined(typeof(AccessLevel), access))
            {
                Console.WriteLine("Invalid input. Please enter 1 or 2:");
            }

            Console.WriteLine($"\nUser '{username}' created successfully!");
            Thread.Sleep(1500);

            return new User(username, password, access);
        }

        public void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine($"=== MAIN MENU ({Username}) ===");
            Console.WriteLine($"Access Level: {Access}");

            if (Access == AccessLevel.FullAccess)
            {
                Console.WriteLine("1. View Patients");
                Console.WriteLine("2. Add Patient");
                Console.WriteLine("3. Edit Patient");
                Console.WriteLine("4. Delete Patient");
                Console.WriteLine("5. Generate Report");
                Console.WriteLine("6. Logout");
            }
            else
            {
                Console.WriteLine("1. View Patients");
                Console.WriteLine("2. Generate Report");
                Console.WriteLine("3. Logout");
            }
        }
    }
}