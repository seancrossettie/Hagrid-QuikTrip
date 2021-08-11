using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hagrid_QuikTrip.Employee
{
    abstract class Employee
    {
        public string Name { get; set; }

        public int EmployeeID { get; set; }

        public double RetailQuarterlySales { get; set; } = 0;
        public double RetailYearlySales { get; set; } = 0;

        public Employee()
        {
        }
    }
}
