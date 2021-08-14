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
                            Store employeeStore = stores.GetStores().First(store => store.StoreID == tempEmployee.StoreID);
                            employee.RetailQuarterlySales += randomSale;
                            employeeStore.QuarterlySales += randomSale;
                            if (employeeStore.YearlySales <= employeeStore.QuarterlySales)
                            {
                                employeeStore.YearlySales = employeeStore.QuarterlySales;
                            }
                        }
                        else if (type == "StoreManager")
                        {
                            var tempEmployee = (StoreManager)employee;
                            Store employeeStore = stores.GetStores().First(store => store.StoreID == tempEmployee.StoreID);
                            employee.RetailQuarterlySales += randomSale;
                            employeeStore.QuarterlySales += randomSale;
                            if (employeeStore.YearlySales <= employeeStore.QuarterlySales)
                            {
                                employeeStore.YearlySales = employeeStore.QuarterlySales;
                            }
                        }
                        else if (type == "AssistantManager")
                        {
                            var tempEmployee = (AssistantManager)employee;
                            Store employeeStore = stores.GetStores().First(store => store.StoreID == tempEmployee.StoreID);
                            employee.RetailQuarterlySales += randomSale;
                            employeeStore.QuarterlySales += randomSale;
                            if (employeeStore.YearlySales <= employeeStore.QuarterlySales)
                            {
                                employeeStore.YearlySales = employeeStore.QuarterlySales;
                            }
                        }
                    }
                }
                Thread.Sleep(2000);

                foreach (var store in stores.GetStores())
                {
                    // Add gas sales.
                    store.GasCurrentQuarterlySales += RandomDollars(200, 4900);
                    if (store.GasCurrentYearlySales < store.GasCurrentQuarterlySales)
                    {
                        store.GasCurrentYearlySales = store.GasCurrentQuarterlySales;
                    }
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
                else if (type == "StoreManager")
                {
                    var tempEmployee = (StoreManager)employeeItem;
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
        static void QuarterlyReport(StoreRepository stores)
        {
            Console.WriteLine("Submit Quarterly Sales: Enter Store Id:\n");
            stores.GetStores().ForEach(store => Console.WriteLine("Store # {0}", store.StoreID));
            int storeID;
            string input;
            ConsoleKeyInfo inputKey;
            Store storeItem;
            input = Console.ReadLine();
            if (int.TryParse(input, out storeID))
            {
                storeItem = stores.GetStores().FirstOrDefault(item => item.StoreID == storeID);
                if (storeItem != null)
                {
                    Console.WriteLine("Sales data for store # {0}:", storeItem.StoreID);
                    Console.WriteLine("Gas sales for this quarter: {0:C}", storeItem.GasCurrentQuarterlySales);
                    Console.WriteLine("Gas sales for current year: {0:C}", storeItem.GasCurrentYearlySales);
                    Console.WriteLine("Retail sales for this quarter: {0:C}", storeItem.QuarterlySales);
                    Console.WriteLine("Retail sales for current year: {0:C}\n", storeItem.YearlySales);
                    Console.WriteLine("Submit quarterly data?  Enter 'y' to submit");

                    inputKey = Console.ReadKey(true);
                    Console.Write('\n');
                    if (inputKey.KeyChar == 'y' || inputKey.KeyChar == 'Y')
                    {
                        // Add quarterly sales data to yearly totals, reset quarterly sales to zero.
                        Console.Write("Submitting quarterly sales data...\n");
                            storeItem.GasCurrentYearlySales += storeItem.GasCurrentQuarterlySales;
                            storeItem.GasCurrentQuarterlySales = 0;
                            storeItem.YearlySales += storeItem.QuarterlySales;
                            storeItem.QuarterlySales = 0;

                        Console.WriteLine("Updated data for store # {0}", storeItem.StoreID);
                        Console.WriteLine("Gas sales for this quarter: {0:C}", storeItem.GasCurrentQuarterlySales);
                        Console.WriteLine("Gas sales for current year: {0:C}", storeItem.GasCurrentYearlySales);
                        Console.WriteLine("Retail sales for this quarter: {0:C}", storeItem.QuarterlySales);
                        Console.WriteLine("Retail sales for current year: {0:C}\n", storeItem.YearlySales);
                    }
                    else Console.WriteLine("Quarterly sales not submitted");
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
                Console.WriteLine("3. Submit Quarterly Data ");
                Console.WriteLine("4. Exit");
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
                        pause = true;
                        QuarterlyReport(stores);
                        pause = false;
                        break;
                    case '4':
                        quit = true;
                        break;
                }
            } // while (!quit)
        }
        #endregion
        
        static void StoreReport(StoreRepository stores, EmployeeRepository employees)
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
                    var managerList = employees.GetEmployees().Where(employee => employee.GetType().Name == "StoreManager").Cast<StoreManager>();
                    var manager = managerList.FirstOrDefault(employee => employee.StoreID == storeItem.StoreID);
                    if (manager != null)
                    {
                        Console.WriteLine("Store Manager: {0}", manager.Name);
                        Console.WriteLine("Retail Sales: {0:C}", manager.RetailQuarterlySales);
                    }
                    else if (manager == null) Console.WriteLine("No manager for this store.");

                    var assistantManagerList = employees.GetEmployees().Where(employee => employee.GetType().Name == "AssistantManager").Cast<AssistantManager>();
                    var assistantManager = assistantManagerList.FirstOrDefault(employee => employee.StoreID == storeItem.StoreID);
                    if(assistantManager != null)
                    {
                        Console.WriteLine("Assistant Manager: {0}", assistantManager.Name);
                        Console.WriteLine("Retail Sales: {0:C}", assistantManager.RetailQuarterlySales);
                    }
                    else if (assistantManager == null) Console.WriteLine("No assistant manager for this store.");

                    var AssociatesList =  employees.GetEmployees().Where(employee => employee.GetType().Name == "StoreAssociate").Cast<StoreAssociate>();
                    foreach (var employee in AssociatesList)
                    {
                        if(employee.StoreID == storeItem.StoreID)
                        {
                            Console.WriteLine("Associate: {0}", employee.Name);
                            Console.WriteLine("Retail Sales: {0:C}", employee.RetailQuarterlySales);
                        }
                    }


                    Console.WriteLine("Sales report for Store #{0}", storeItem.StoreID);
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

        static void DistrictReport(DistrictRepository districts, StoreRepository stores)
        {
            string input;
            ConsoleKeyInfo exit;

            Console.WriteLine();
            
            districts.GetDistricts().ForEach(district => {
                Console.WriteLine($"{district.Name} ID #: {district.DistrictID}");
            });
            Console.WriteLine();
            Console.WriteLine("\r\nEnter a District ID: ");
            input = Console.ReadLine();

            int districtID;
            District district;
            IEnumerable<Store> storeTotals;


            if (int.TryParse(input, out districtID))
            {
                district = districts.GetDistricts().FirstOrDefault(dist => dist.DistrictID == districtID);
                storeTotals = stores.GetStores().Where(store => store.DistrictID == districtID);

                if (district !=null)
                {
                    foreach (var store in storeTotals)
                    {
                        district.QuarterlyDistrictSales += store.GasCurrentQuarterlySales + store.QuarterlySales;
                        district.YearlyDistrictSales += store.GasCurrentYearlySales + store.YearlySales;
                    }

                    Console.WriteLine();
                    Console.WriteLine($"{district.Name}");
                    Console.WriteLine("--------------------");
                    Console.WriteLine($"Quarterly Sales for this district: ${district.QuarterlyDistrictSales}");
                    Console.WriteLine($"Yearly Sales for this district: ${district.YearlyDistrictSales}");
                }
                Console.WriteLine();
                Console.WriteLine("Hit any key to exit district report");
                exit = Console.ReadKey(true);
            }
        }

        static void AddEmployee()
        {
            Console.WriteLine("Calling Add Employee\n");
        }

        static void AddStore()
        {
            Console.WriteLine("Calling Add Store\n");
        }

        static void AddDistrict()
        {
            Console.WriteLine("Calling Add Distrtict\n");
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
                districtObj = new District(districtNames[i] , i);
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
            StoreManager manager1 = new StoreManager("Mary Ann", 5, 0);
            employees.SaveNewEmployee(manager1);
            AssistantManager assistantManager1 = new AssistantManager("Gilligan", 6, 0);
            employees.SaveNewEmployee(assistantManager1);
            
        }

        static void MainMenu(ref int count, ref bool quit, ref bool pause, DistrictRepository districts, StoreRepository stores, EmployeeRepository employees)
        {
            ConsoleKeyInfo inputKey;

            Console.WriteLine("Welcome to the QuikTrip Sales Management System");
            Console.WriteLine();
            while (!quit)
            {
                Console.WriteLine("Please choose an option below:");
                Console.WriteLine("1. Enter Sales Data");
                Console.WriteLine("2. Generate Store Report");
                Console.WriteLine("3. Generate District Report");
                Console.WriteLine("4. Add New Employee");
                Console.WriteLine("5. Add a Store/District");
                Console.WriteLine("6. Exit");
                Console.WriteLine();
                Console.WriteLine("\r\nEnter an option: ");
                inputKey = Console.ReadKey(true);
                switch (inputKey.KeyChar)
                {
                    case '1':
                        AddSales(stores, employees, ref pause);
                        break;
                    case '2':
                        StoreReport(stores, employees);
                        break;
                    case '3':
                        DistrictReport(districts, stores);
                        break;
                    case '4':
                        AddEmployee();
                        break;
                    case '5':
                        Console.WriteLine("Please choose an option below:");
                        Console.WriteLine("1. Add a Store");
                        Console.WriteLine("2. Add a District");
                        var storeOrDistrict = Console.ReadLine();
                        if(storeOrDistrict == "1")
                        {
                            Console.WriteLine("Add a new store.");
                            Console.WriteLine("Enter the district ID in which the store belongs:");
                            districts.GetDistricts().ForEach(district => Console.WriteLine($"District: {district.Name} \n ID: {district.DistrictID}"));
                            var storeName = Console.ReadLine();
                            var randomNumber = new Random();
                            var storeID = randomNumber.Next(5000, 10000);
                            var districtID = int.Parse(storeName);
                            var newStore = new Store(storeID, districtID);
                            if (storeID > 5000)
                            {
                                stores.SaveNewStore(newStore);
                                stores.GetStores().ForEach(store => Console.WriteLine($"The store ID is {store.StoreID} and its district ID is {store.DistrictID}"));
                            }
                        }
                        else if(storeOrDistrict == "2")
                        {
                            Console.WriteLine("Add a new district.");

                            Console.WriteLine("District Name:");
                            var districtName = Console.ReadLine();
                            if (districtName != "")
                            {
                                var randomNumber = new Random();
                                var districtID = randomNumber.Next(5000, 10000);
                                var newDistrict = new District(districtName, districtID);
                                districts.SaveNewDistrict(newDistrict);
                                districts.GetDistricts().ForEach(district => Console.WriteLine($"The district name is {district.Name} and its ID is {district.DistrictID}"));
                            }

                        } else
                        {
                            Console.WriteLine("Invalid choice");
                        }
                        break;
                    case '6':
                        quit = true;
                        //Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine();
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            bool quit = false;
            bool pause = false;
            int count = 0;
            var districts = new DistrictRepository();
            var stores = new StoreRepository();
            var employees = new EmployeeRepository();
            SampleData(districts, stores, employees);
            Parallel.Invoke(() =>
                {
                    SalesGenerator(employees, stores, ref quit, ref pause); ;
                },   // close first Action

                () =>
                {
                    MainMenu(ref count, ref quit, ref pause, districts, stores, employees);
                }   //close second Action

            ); //close parallel.invoke
        }
    }
}
