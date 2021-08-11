using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hagrid_QuikTrip.Employee
{
    class AssistantManager : Employee
    {
        public int StoreID { get; set; }
        public AssistantManager(string name, int employeeID, int storeID)
        {
            Name = name;
            EmployeeID = employeeID;
            StoreID = storeID;
        }
    }
}
