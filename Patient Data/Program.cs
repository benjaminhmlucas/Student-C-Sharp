using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientData
{
    class Program
    {
        private const int LIST_SIZE = 200000;
        /// <summary>
        /// displays a patients info that is passed into the method
        /// </summary>
        /// <param name="p">patient who's data will be displayed</param>
        static void showPatient(Patient p)
        {
            //displays the patient's name, id and procedure cost
            Console.WriteLine("\n******************************************");
            Console.WriteLine("Patient's Name: {0} {1}", p.FirstName, p.LastName);
            Console.WriteLine("Patient's ID: {0}", p.Id);
            Console.WriteLine("Patient's Last Procedure Cost: {0:C2}", p.ProcedureCost);
            Console.WriteLine("******************************************");
        }
        /// <summary>
        /// displays a menu for user to choose an action from
        /// </summary>
        static void DisplayMenu() {
            Console.WriteLine("Please choose an action:");
            Console.WriteLine("(F)ind a patient");
            Console.WriteLine("(R)emove a patient");
            Console.WriteLine("(S)how Statistics");
            Console.WriteLine("(Q)uit");
            Console.WriteLine("Please type a letter for your choice:");            
        }
        static void Main(string[] args)
        {
            PatientData data = new PatientData(LIST_SIZE);
            data.LoadPatients("patientList.csv");
            Console.WriteLine("***********************************");//greeting to user
            Console.WriteLine("Welcome to Patty's Patient Finder! ");           
            Console.WriteLine("***********************************");
            DisplayMenu();
            ConsoleKeyInfo choice = Console.ReadKey();
            while (choice.Key != ConsoleKey.Q) {//switch statement to determine action to take, Q exits loop to Quit
                switch (choice.Key) {
                    case ConsoleKey.F://------------------------------------------------------------------find a patient
                        Console.WriteLine("\n***********************************");
                        Console.WriteLine("You have selected (F)ind a patient!");
                        Console.WriteLine("***********************************");
                        Console.WriteLine("\nEnter first and last name:");
                        string name = Console.ReadLine();
                        String[] names = name.Split();
                        while (names.Length != 2)
                        {
                            Console.WriteLine("\nXXXXXXXERRORXXXXXX");
                            Console.WriteLine("XIncorrect Input!X");
                            Console.WriteLine("XXXXXXXXXXXXXXXXXX\n");
                            Console.WriteLine("Enter first and last name:");
                            name = Console.ReadLine();
                            names = name.Split();
                        }

                        Patient.Comparisons = 0;
                        Patient pat = data.FindPatientByName(names[0], names[1]);
                        if (pat != null)
                        {
                            showPatient(pat);
                        }
                        else
                        {
                            Console.WriteLine("\nXXXXXXXXERRORXXXXXX");
                            Console.WriteLine("XPatient not foundX");
                            Console.WriteLine("XXXXXXXXERRORXXXXXX\n");
                        }
                        Console.WriteLine("Comparisions: {0}\n", Patient.Comparisons);
                        Console.WriteLine("Oh Man! Wasn't that fun?! Want to do it again?!");
                        DisplayMenu();
                        choice = Console.ReadKey();
                        break;
                    case ConsoleKey.R://------------------------------------------------------------------Remove a patient
                        Console.WriteLine("\n************************************");
                        Console.WriteLine("You have selected (R)emove a patient!");
                        Console.WriteLine("************************************");
                        Console.WriteLine("\nEnter first and last name:");
                        name = Console.ReadLine();
                        String[] bothName = name.Split();
                        while (bothName.Length != 2)
                        {
                            Console.WriteLine("\nXXXXXXXERRORXXXXXX");
                            Console.WriteLine("XIncorrect Input!X");
                            Console.WriteLine("XXXXXXXXXXXXXXXXXX\n");
                            Console.WriteLine("Enter first and last name:");
                            name = Console.ReadLine();
                            bothName = name.Split();
                        }

                        Patient.Comparisons = 0;
                        pat = data.FindPatientByName(bothName[0], bothName[1]);
                        if (pat != null)
                        {
                            Console.WriteLine("Following patient has been removed:");
                            showPatient(data.RemovePatient(pat));
                        }
                        else
                        {
                            Console.WriteLine("\nXXXXXXXXERRORXXXXXX");
                            Console.WriteLine("XPatient not foundX");
                            Console.WriteLine("XXXXXXXXERRORXXXXXX\n");
                        }
                        Console.WriteLine("Comparisions: {0}\n", Patient.Comparisons);
                        Console.WriteLine("Oh Man! Wasn't that fun?! Want to do it again?!");
                        DisplayMenu();
                        choice = Console.ReadKey();
                        break;
                    case ConsoleKey.S://-----------------------------------------------------------------show array and link list statistics
                        Console.WriteLine("\n************************************");
                        Console.WriteLine("You have selected (S)how Statistics!");
                        Console.WriteLine("************************************");
                        Console.WriteLine("Array Length: {0}", LIST_SIZE);//array length
                        Console.WriteLine("Percentage of used slots: {0:P}", data.FindPercentUsedSlots(LIST_SIZE));//percentage of used slots
                        Console.WriteLine("Longest List Length: {0}", data.FindLongestLength());//longest linked list
                        Console.WriteLine("Average List Length: {0}", data.FindAverageLength());//find average linked list length
                        Console.WriteLine("************************************");
                        DisplayMenu();
                        choice = Console.ReadKey();
                        break;
                    default://-----------------------------------------------------------------input error message to the user
                        Console.WriteLine("\nXXXXXXXERRORXXXXXX");
                        Console.WriteLine("XIncorrect Input!X");
                        Console.WriteLine("XXXXXXXXXXXXXXXXXX\n");
                        DisplayMenu();
                        choice = Console.ReadKey();
                        break;
                }
            }
            Console.WriteLine("********************************************");//user goodbye statement
            Console.WriteLine("Thank you for using Patty's Patient Finder!");
            Console.WriteLine("Press any key to close window.");
            Console.WriteLine("********************************************");
            Console.ReadKey();//pause before exiting
        }
    }
}
