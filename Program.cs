using System;
using System.Threading;
using System.Threading.Tasks;
using Hagrid_QuikTrip.Districts;
using Hagrid_QuikTrip.Stores;
using Hagrid_QuikTrip.Employees;
using System.Collections.Generic;


namespace Hagrid_QuikTrip
{
    class ParallelInvoke
    {
        static int SalesGenerator(EmployeeRepository employees, ref bool quit, ref bool pause)
        {

            var employeeList = employees.GetEmployees();
            int count = 0;

            while (!quit)
            {
                foreach (var employee in employeeList)
                {
                    if (!pause)
                    {
                        count += 1;
                        employee.RetailQuarterlySales += 5.0;
                    }

                }
                Thread.Sleep(500);
            }
            return count;
        }

        static void AddDistrictSales()
        {
            Console.WriteLine("Calling Add Sales\n");

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

        static void MainMenu(ref int count, ref bool quit, ref bool pause)
        {
            ConsoleKeyInfo inputKey;

            Console.WriteLine("Welcome to the QuikTrip Sales Management System");
            Console.WriteLine();
            while (!quit)
            {
                Console.WriteLine("Please choose an option below:");
                Console.WriteLine("1. Enter District Sales");
                Console.WriteLine("2. Generate District Report");
                Console.WriteLine("3. Add New Employee");
                Console.WriteLine("4. Add a Store/District");
                Console.WriteLine("5. Exit");
                Console.WriteLine();
                Console.Write("\r\nEnter an option: ");
                inputKey = Console.ReadKey(true);
                switch (inputKey.KeyChar)
                {
                    case '1':
                        pause = true;
                        AddDistrictSales();
                        pause = false;
                        break;
                    case '2':
                        pause = true;
                        DistrictReport();
                        pause = false;
                        break;
                    case '3':
                        AddEmployee();
                        break;
                    case '4':
                        AddStore();
                        break;
                    case '5':
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
            var store1 = new Store(1);
            stores.SaveNewStore(store1);
            StoreAssociate john = new StoreAssociate("John Maple", 1, 1);
            employees.SaveNewEmployee(john);
            Parallel.Invoke(() =>
            {
                count = SalesGenerator(employees, ref quit, ref pause); ;
            },   // close first Action

                () =>
                {
                    MainMenu(ref count, ref quit, ref pause);
                }   //close second Action

            ); //close parallel.invoke
        }
    }
}
