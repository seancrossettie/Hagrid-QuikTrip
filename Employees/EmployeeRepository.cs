using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hagrid_QuikTrip.Employees
{
    class EmployeeRepository
    {
        // we're using a static list here instead of a proper database
        // but the general idea still holds up once we cover databases
        // it's just another piece of code to replace and refactor nbd
        static List<Employee> _employee = new List<Employee>();

		

		public List<Employee> GetEmployees()
        {
            return _employee;
        }

        public void SaveNewEmployee(Employee employee)
        {
            _employee.Add(employee);
        }
    }
}
