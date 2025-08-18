using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG281Assignment
{
    internal class ReadOnlyActions
    {
        // List to store patients in memory (provided by another part of the program)
        private List<Patient> patients;

        // Constructor to receive the list of patients
        public ReadOnlyActions(List<Patient> patientList)
        {
            patients = patientList; // Set the list to the provided data
        }

        public void ViewPatients()
        {
            Console.Clear();
            Console.WriteLine("= PATIENT FILES =");
            if (patients.Count == 0)
            {
                Console.WriteLine("No patient records found.");
            }
            else
            {
                foreach (var patient in patients)
                {
                    Console.WriteLine($"- {patient.Id}: {patient.Name}");
                }
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public void GenerateReport()
        {
            Console.Clear();
            Console.WriteLine("= PATIENT REPORT =");
            if (patients.Count == 0)                          //checks if patient list is empty
            {
                Console.WriteLine("No patient records to report.");
            }
            else
            {
                Console.WriteLine($"Total Patients: {patients.Count}\n");         
                foreach (var patient in patients)
                {
                    Console.WriteLine($"Patient ID: {patient.Id}");
                    Console.WriteLine($"ID: {patient.Id}");
                    Console.WriteLine($"Name: {patient.Name}");
                    Console.WriteLine($"Last Updated: {patient.LastUpdated}");
                    Console.WriteLine("---------------------------");
                }
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

}
