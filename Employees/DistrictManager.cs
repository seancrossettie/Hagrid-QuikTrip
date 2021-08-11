using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hagrid_QuikTrip.Employees
{
    class DistrictManager : Employee
    {
        public int DistrictID { get; set; }
        public DistrictManager(string name, int employeeID, int districtID)
        {
            Name = name;
            EmployeeID = employeeID;
            DistrictID = districtID;
        }
    }
}
