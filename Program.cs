using System;
using System.Threading;
using System.Threading.Tasks;
using Hagrid_QuikTrip.Districts;
using Hagrid_QuikTrip.Stores;
using Hagrid_QuikTrip.Employees;
using System.Collections.Generic;
using System.Linq;


namespace Hagrid_QuikTrip
{
    class ParallelInvoke
    {

        static double RandomDollars(int lower, int upper)
        {
            // Generate random dollar amount between lower and upper amount in cents
            Random random = new Random();
            return (double)random.Next(lower, upper) / 100;
        }
        static void SalesGenerator(EmployeeRepository employees, StoreRepository stores,  ref bool quit, ref bool pause)
        {

            var employeeList = employees.GetEmployees();
            // Lower and upper limits for random sales amount
            int lower = 200;
            int upper= 2500;

            while (!quit)
            {
                foreach (var employee in employeeList)
                {
                    var type = employee.GetType().Name;
                    if (!pause)
                    {
                        // Find out if this is a store associates or an asstant manager - different types
                        double randomSale = RandomDollars(lower, upper);
                        if (type == "StoreAssociate")
                        {
                            var tempEmployee = (StoreAssociate)employee;
                            stores.GetStores().First(store => store.StoreID == tempEmployee.StoreID).QuarterlySales += randomSale;
                            employee.RetailQuarterlySales += randomSale;

                        }
                        else if (type == "AssistantManager")
                        {
                            var tempEmployee = (AssistantManager)employee;
                            stores.GetStores().First(store => store.StoreID == tempEmployee.StoreID).QuarterlySales += randomSale;
                            employee.RetailQuarterlySales += randomSale;
                        }

                    }

                }
                Thread.Sleep(1000);

                foreach (var store in stores.GetStores())
                {
                    // Reset quarterly sales, since we are accumulating at the employee level.
                    store.QuarterlySales = 0;
                    store.GasCurrentQuarterlySales += RandomDollars(200, 4900);
                }
            }
        }

        #region Sales
        static bool doubleInput(ref double amount)
        {
            string input;
            bool returnVal = false;
            input = Console.ReadLine();
            if ( Double.TryParse(input, out amount)) {
                returnVal = true;
            }
            return returnVal;
        }

        static void RetailSale(EmployeeRepository employees)
        {
            string input;
            double amount = 0;
            int employeeID;
            Employee employeeItem;

            Console.WriteLine("Select the employee by ID:");
            employees.GetEmployees().ForEach(employee => Console.WriteLine("Employee ID: {0}, {1}", employee.EmployeeID, employee.Name));
            input = Console.ReadLine();
            if (int.TryParse(input, out employeeID))
            {
                Console.WriteLine("You selected employee {0}", employeeID);
                employeeItem = employees.GetEmployees().FirstOrDefault(item => item.EmployeeID == employeeID);
                var type = employeeItem.GetType().Name;

                if (type == "StoreAssociate")
                {
                    var tempEmployee = (StoreAssociate)employeeItem;
                    Console.Write("Enter Retail Sale amount: ");
                    if (doubleInput(ref amount))
                    {
                        tempEmployee.RetailQuarterlySales += amount;
                    }
                    else Console.WriteLine("Invalid input.");

                }
                else if (type == "AssistantManager")
                {
                    var tempEmployee = (AssistantManager)employeeItem;
                    Console.Write("Enter Retail Sale amount: ");
                    if (doubleInput(ref amount))
                    {
                        tempEmployee.RetailQuarterlySales += amount;
                    }
                    else Console.WriteLine("Invalid input.");
                }
            }
        }
        static void GasSale(StoreRepository stores)
        {
            Console.WriteLine("Gas Sale: Enter Store Id:\n");
            stores.GetStores().ForEach(store => Console.WriteLine("Store # {0}", store.StoreID));
            int storeID;
            string input;
            Store storeItem;
            double amount = 0;
            input = Console.ReadLine();
            if (int.TryParse(input, out storeID))
            {
                storeItem = stores.GetStores().FirstOrDefault(item => item.StoreID == storeID);
                if (storeItem != null)
                {
                    Console.Write("Enter Gas Sale amount: ");
                    if (doubleInput(ref amount))
                    {
                        storeItem.GasCurrentQuarterlySales += amount;
                    }
                    else Console.WriteLine("Invalid input.");
                }
                else Console.WriteLine("Invalid Store selection.");
            }
        }

        static void AddSales(StoreRepository stores, EmployeeRepository employees, ref bool pause)
        {
            bool quit = false;
            ConsoleKeyInfo inputKey;
            while (!quit)
            {
                Console.WriteLine("Please choose an option below:");
                Console.WriteLine("1. Enter Employee Sale");
                Console.WriteLine("2. Enter Gas Sale");
                Console.WriteLine("3. Exit");
                Console.WriteLine();
                Console.Write("\r\nEnter an option: ");
                inputKey = Console.ReadKey(true);
                switch (inputKey.KeyChar)
                {
                    case '1':
                        pause = true;
                        RetailSale(employees);
                        pause = false;
                        break;
                    case '2':
                        pause = true;
                        GasSale(stores);
                        pause = false;
                        break;
                    case '3':
                        quit = true;
                        break;
                }
            } // while (!quit)
        }
        #endregion

        static void StoreReport(StoreRepository stores)
        {
            string input;
            ConsoleKeyInfo exitKey;
            Console.WriteLine("Select Store:");
            stores.GetStores().ForEach(store => Console.WriteLine("Store #: {0}", store.StoreID));
            input = Console.ReadLine();
            int storeID;
            Store storeItem;
            if (int.TryParse(input, out storeID))
            {
                // get store by selecting by store ID
                storeItem = stores.GetStores().FirstOrDefault(item => item.StoreID == storeID);
                if (storeItem != null)
                {
                    Console.WriteLine("Sales report for Store #: {0}", storeItem.StoreID);
                    Console.WriteLine("Gas sales for this quarter: {0:C}", storeItem.GasCurrentQuarterlySales);
                    Console.WriteLine("Gas sales for current year: {0:C}", storeItem.GasCurrentYearlySales);
                    Console.WriteLine("Retail sales for this quarter: {0:C}", storeItem.QuarterlySales);
                    Console.WriteLine("Retail sales for current year: {0:C}\n", storeItem.YearlySales);
                }
                else Console.WriteLine("Invalid Store selection.\n");
            }
            Console.WriteLine("Hit any key to exit Store Report");
            exitKey = Console.ReadKey(true);
        }

        static void DistrictReport()
        {
            Console.WriteLine("Calling District Report\n");
        }
        static void AddEmployee()
        {
            Console.WriteLine("Calling Add Employee\n");
        }

        static void AddStore()
        {
            Console.WriteLine("Calling Add Store\n");
        }

        static void SampleData(DistrictRepository districts, StoreRepository stores, EmployeeRepository employees)
        {
            District districtObj;
            Store storeObj;
            StoreAssociate associateObj;

            // Populate list of districts
            string[] districtNames = new string[] { "Middle Tennessee", "East Tennessee", "West Tennesee", "Central Kentucky" };
            for (int i = 0; i < districtNames.Length; i++)
            {
                districtObj = new District("Middle Tennessee", i);
                districts.SaveNewDistrict(districtObj);
            }

            // Populate initial list of stores
            int j = 0;
            for ( int i = 0; i < 4; i++)
            {
                storeObj = new Store(i,j++);
                stores.SaveNewStore(storeObj);
                j++;
                if (j == districts.GetDistricts().Count) j = 0;
            }

            // Populate initial list of Associates
            string[] employeeNames = new string[] { "John Maple", "Honey-Rae Swan", "Sean Rossettie", "Jonathon Joyner" };
            j = 0;
            for ( int i = 0; i < 4; i++)
            {
                associateObj = new StoreAssociate(employeeNames[i], i, j);
                employees.SaveNewEmployee(associateObj);
                j++;
                if (j == stores.GetStores().Count) j = 0;
            }
        }

        // Add Employee
        static void AddNewEmployee(DistrictRepository districts, StoreRepository stores, EmployeeRepository employee)
        {
            Console.Clear();
            Console.WriteLine("Please Choose option Below");
            Console.WriteLine("1. Add a Assistant Manager");
            Console.WriteLine("2. Add a District Manager");
            var assistOrDistrict = Console.ReadLine();

            if (assistOrDistrict == "1")
            {
                Console.WriteLine("Add a new Assistant Manager");

                Console.WriteLine("Enter Assistant Manager Name");
                string Name;
                Name = Console.ReadLine();
                var randomNumber = new Random();
                var EmployeeID = randomNumber.Next(1, 80);

                Console.WriteLine("Enter StoreID");
                stores.GetStores().ForEach(store => Console.WriteLine($"StoreID: {store.StoreID}"));
                var StoreName = Console.ReadLine();
                var StoreID = int.Parse(StoreName);

                Console.WriteLine("Enter District ID");
                districts.GetDistricts().ForEach(District => Console.WriteLine($"District: {District.Name} ID: {District.DistrictID}"));
                var DistrictName = Console.ReadLine();
                var DistrictID = int.Parse(DistrictName);


                var newAssistantEmployee = new AssistantManager(Name, StoreID, DistrictID, EmployeeID);
                
                    employee.SaveNewEmployee(newAssistantEmployee);
                
                Console.WriteLine($"Welcome {Name} you have an Employee ID of {EmployeeID} StoreID of {StoreID} and DistrictID of {DistrictID}");

            } 
            else if (assistOrDistrict == "2")
             {
                Console.WriteLine("Create new District Manager");
               // DistrictID = Console.ReadLine();



            }


        }


        static void MainMenu(ref int count, ref bool quit, ref bool pause, DistrictRepository districts, StoreRepository stores, EmployeeRepository employees)
        {
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
                    Console.Write("\r\nEnter sales from one of the following districts: ");
                    Console.ReadLine();
                    break;
                case "2":
                    Console.WriteLine("2");
                    break;
                case "3":
                    AddNewEmployee(districts, stores, employees);
                    Console.WriteLine("3");
                    break;
                case "4":
                    Console.WriteLine("4");
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine();
                    break;
            }

        }
    }
}
