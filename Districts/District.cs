using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hagrid_QuikTrip.Districts
{
    class District
    {
        public string Name { get; set; }
        public double QuarterlyDistrictSales { get; set; }
        public double YearlyDistrictSales { get; set; }
        public District(string name)
        {
            Name = name;
        }
    }
}
