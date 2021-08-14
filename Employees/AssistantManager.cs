using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hagrid_QuikTrip.Employees
{
    class AssistantManager : Employee
    {
        public int StoreID { get; set; }
		public string Name { get; set; }
		public int DistrictID { get; set; }
        public int EmployeeID { get; set; }

		public AssistantManager(string name, int employeeID, int storeID, int districtID)
        {
            Name = name;
            EmployeeID = employeeID;
			StoreID = storeID;
            DistrictID = districtID;
        }
	}
}
