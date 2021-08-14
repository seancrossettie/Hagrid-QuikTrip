using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hagrid_QuikTrip.Employees
{
    class StoreManager : Employee
    {
        public int StoreID { get; set; }
        public StoreManager(string name, int employeeID, int storeID)
        {
            Name = name;
            EmployeeID = employeeID;
            StoreID = storeID;
        }
    }
}
