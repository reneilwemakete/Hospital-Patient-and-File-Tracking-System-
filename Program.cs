using System;
using System.Collections.Generic;
using System.Threading;

namespace PRG281Assignment
{
    enum LoginPage
    {
        Login = 1,
        Register,
        Exit
    }

    internal class Program
    {
        static User currentUser = null;    //A variable to track the currently logged in user
        static List<Patient> sharedPatients = new List<Patient>();    //a shared list for all patient data (accessed by both nurse and doctor)
        static List<User> users = new List<User>(); // Store registered users

        static void Main(string[] args)
        {
            //Creating hard coded patients data
            // Initialize shared patient list with default data
            sharedPatients.AddRange(new List<Patient>
            {
                new Patient { Id = "603944", Name = "Bontle Makete", LastUpdated = DateTime.Now },
                new Patient { Id = "602267", Name = "Mark Minnaar", LastUpdated = DateTime.Now },
                new Patient { Id = "603233", Name = "Tshepo Koitheng", LastUpdated = DateTime.Now }
            });

            users.Add(new User("doctor", "1234", AccessLevel.FullAccess));   //adds a ddefault doctor user with full access. 
            users.Add(new User("nurse", "1234", AccessLevel.ReadOnly));    //adds default user with read-only access


            bool exitSystem = false;

            while (!exitSystem)
            {
                Console.Clear();
                Console.WriteLine("=== HOSPITAL MANAGEMENT SYSTEM ===");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit");
                Console.Write("Select option (1-3): ");

                if (Enum.TryParse(Console.ReadLine(), out LoginPage choice) &&
                    Enum.IsDefined(typeof(LoginPage), choice))
                {
                    switch (choice)
                    {
                        case LoginPage.Login:
                            HandleLogin();
                            break;

                        case LoginPage.Register:
                            User.CreateUser();
                            break;

                        case LoginPage.Exit:
                            exitSystem = true;
                            Console.WriteLine("\nExiting system...");
                            Thread.Sleep(1000);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid input! Please enter 1-3.");
                    Thread.Sleep(1500);
                }
            }
        }

        static void HandleLogin()
        {
            Console.Clear();
            Console.WriteLine("=== LOGIN ===");

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            // Simple authentication.
            // I still need to figure out how to compare the credentials from the User class.  :(
            // so here is some hardcoded credentials for demonstration purposes.
            if (username == "doctor" && password == "1234")
            {
                currentUser = new User(username, password, AccessLevel.FullAccess);
            }
            else if (username == "nurse" && password == "1234")
            {
                currentUser = new User(username, password, AccessLevel.ReadOnly);
            }
            else
            {
                Console.WriteLine("\nInvalid credentials!");
                Thread.Sleep(1500);
                return;
            }

            Console.WriteLine($"\nWelcome, {currentUser.Username}!");
            Thread.Sleep(1000);
            ShowUserMenu();
        }

        static void ShowUserMenu()
        {
            //Objects for the 2 access levels
            FullAccessActions fullAccessActions = new FullAccessActions(sharedPatients);
            ReadOnlyActions readOnlyActions = new ReadOnlyActions(sharedPatients);

            bool logout = false;

            while (!logout && currentUser != null)
            {
                currentUser.DisplayMainMenu();
                Console.Write("\nSelect option: ");
                string input = Console.ReadLine();

                if (currentUser.Access == AccessLevel.FullAccess)
                {
                    switch (input)
                    {
                        case "1": // View Patients
                            Console.WriteLine("\nShowing all patients...");
                            Thread.Sleep(1000);
                            fullAccessActions.ViewPatients();
                            break;
                        
                        case "2": // Add Patient
                            Console.WriteLine("\nAdding new patient...");
                            Thread.Sleep(1000);
                            fullAccessActions.AddPatientFile();
                            break;
                        
                        case "3": // Edit Patient
                            Console.WriteLine("\nEditing patient...");
                            Thread.Sleep(1000);
                            fullAccessActions.EditPatientFile();                          
                            break;

                        case "4": // Delete Patient
                            Console.WriteLine("\nDeleting patient...");
                            fullAccessActions.DeletePatientFile();
                            Thread.Sleep(1000);
                            break;
                        
                        case "5": // Generate Report
                            Console.WriteLine("\nGenerating report...");
                            Thread.Sleep(1000);
                            fullAccessActions.GenerateReport();
                            break;
                        
                        case "6": // Logout
                            logout = true;
                            Console.WriteLine("\nLogging out...");
                            Thread.Sleep(1000);
                            break;
                        
                        default:
                            Console.WriteLine("\nInvalid option!");
                            Thread.Sleep(1000);
                            break;
                    }
                }
                else // ReadOnly access
                {
                    switch (input)
                    {
                        case "1": // View Patients
                            Console.WriteLine("\nShowing all patients...");
                            Thread.Sleep(1000);
                            readOnlyActions.ViewPatients();
                            break;
                        case "2": // Generate Report
                            Console.WriteLine("\nGenerating report...");
                            readOnlyActions.GenerateReport();
                            Thread.Sleep(1000);
                            break;
                        case "3": // Logout
                            logout = true;
                            Console.WriteLine("\nLogging out...");
                            Thread.Sleep(1000);
                            break;
                        default:
                            Console.WriteLine("\nInvalid option!");
                            Thread.Sleep(1000);
                            break;
                    }
                }
            }

            currentUser = null;
        }
    }
}
