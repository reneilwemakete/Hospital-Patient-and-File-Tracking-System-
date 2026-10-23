using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PRG281Assignment
{
    internal class FullAccessActions
    {
        // List to store all patients
        private List<Patient> patients;

        public FullAccessActions(List<Patient> patientList)        //constructor to recieve the shared patient list.
        {
            patients = patientList;     //sets the class's patient list to the shared notebook passed in
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

        public void AddPatientFile()
        {
            Console.Clear();
            Console.WriteLine("= ADD NEW PATIENT =");
            Console.Write("Patient ID: ");
            string id = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Invalid Patient ID.");
                Thread.Sleep(1500);
                return;
            }

            // Check if a patient ID already exists
            if (patients.Exists(p => p.Id == id))
            {
                Console.WriteLine("Patient ID already exists.");
                Thread.Sleep(1500);
                return;
            }

            Console.Write("Patient Name: ");
            string name = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid Patient Name.");
                Thread.Sleep(1500);
                return;
            }

            // Add new patient to the list
            patients.Add(new Patient { Id = id, Name = name, LastUpdated = DateTime.Now });
            Console.WriteLine($"\nPatient {name} added!");
            Thread.Sleep(1500);
        }

        public void DeletePatientFile()
        {
            Console.Clear();
            Console.WriteLine("= DELETE PATIENT =");
            ViewPatients();
            Console.Write("\nEnter patient ID to delete: ");
            string id = Console.ReadLine() ?? "";

            // Find patient by ID
            var patient = patients.Find(p => p.Id == id);
            if (patient != null)
            {
                patients.Remove(patient);
                Console.WriteLine("Patient record deleted.");
            }
            else
            {
                Console.WriteLine("Patient not found.");
            }
            Thread.Sleep(1500);
        }

        public void EditPatientFile() 
        {
            Console.WriteLine();
            Console.WriteLine("= EDIT PATIENT =");
            ViewPatients();
            Console.WriteLine("\n Enter patient ID to edit: ");
            string id = Console.ReadLine() ?? "";

            var patient = patients.Find(p => p.Id == id);
            if (patient == null)
            {
                Console.WriteLine("Patient Not Found");
                Thread.Sleep(1500);
                return;
            }

            Console.WriteLine("New Patient Name:");
            string newName = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("Invalid Patient Name");
                Thread.Sleep(1500);
                return;
            }

            patient.Name = newName;
            patient.LastUpdated = DateTime.Now;
            Console.WriteLine($"\n Patient {id} updated");
            Thread.Sleep(1500);
            return;
        }
        public void GenerateReport()
        {
            Console.Clear();
            Console.WriteLine("= GENERATE REPORT =");
            if (patients.Count == 0)
            {
                Console.WriteLine("No patient records to report.");
            }
            else
            {
                Console.WriteLine($"Total Patients: {patients.Count}\n");
                foreach (var patient in patients)
                {
                    Console.WriteLine($"Patient: {patient.Id}");
                    Console.WriteLine($"ID: {patient.Id}");
                    Console.WriteLine($"Name: {patient.Name}");
                    Console.WriteLine($"Last Updated: {patient.LastUpdated}");
                    Console.WriteLine("-------------------------------");
                }
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
