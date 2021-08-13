using Hagrid_QuikTrip.Employee;
using System;
using System.Collections.Generic;

namespace Hagrid_QuikTrip
{
    class Program
    {
        static void Main(string[] args)
        {
            bool startMenu = true;

            var assistEmployees = new List<AssistantManager>
            {
                new AssistantManager("Bob Ross", 2, 3, 1)
            };

            while (startMenu)
            {
                MainMenu();
            }

            bool MainMenu()
            {
                Console.Clear();
                Console.WriteLine("Welcome to the QuikTrip Sales Management System");
                Console.WriteLine();
                Console.WriteLine("Please choose an option below:");
                Console.WriteLine("1. Enter District Sales");
                Console.WriteLine("2. Generate District Report");
                Console.WriteLine("3. Add New Employee");
                Console.WriteLine("4. Add a Store/District");
                Console.WriteLine("5. Exit");
                Console.WriteLine();
                Console.Write("\r\nEnter an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        startMenu = false;
                        Console.Write("\r\nEnter sales from one of the following districts: ");
                        Console.ReadLine();
                        return true;
                        break;
                    case "2":
                        startMenu = false;
                        Console.WriteLine("2");
                        return true;
                        break;
                    case "3":
                        startMenu = false;
                        AddNewEmployee();
                        Console.WriteLine("3");
                        return true;
                        break;
                    case "4":
                        startMenu = false;
                        Console.WriteLine("4");
                        return true;
                        break;
                    case "5":
                        Environment.Exit(0);
                        return true;
                        break;
                    default:
                        Console.WriteLine();
                        return true;
                        break;


                        void AddNewEmployee()
                        {
                            Console.Clear();
                            Console.WriteLine("Enter Employee name");

                            string Name;
                            Name = Console.ReadLine();

                            Console.WriteLine("Enter Store ID");
                            int StoreID;
                            StoreID = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Enter District ID");
                            int DistrictID;
                            DistrictID = Convert.ToInt32(Console.ReadLine());


                            var newAssistEmployee = new AssistantManager(Name, StoreID, DistrictID, 2);

                            assistEmployees.Add(newAssistEmployee);
                            Console.WriteLine($"Welcome to Hagrid LLC {Name}");
                            SendMeHome();
                        }
                        
                }
   
            }
            void SendMeHome()
            {
                Console.WriteLine("Returning To Main Menu");
                var command = Console.ReadLine();
                startMenu = true;
            }
        }
    }
}
