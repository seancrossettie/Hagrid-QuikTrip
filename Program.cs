using System;

namespace Hagrid_QuikTrip
{
    class Program
    {
        static void Main(string[] args)
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
