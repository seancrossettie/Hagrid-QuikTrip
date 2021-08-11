using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hagrid_QuikTrip.District
{
    class District
    {
        public string Name { get; set; }
        public int DistrictID { get; set; }
        public double QuarterlyDistrictSales { get; set; }
        public double YearlyDistrictSales { get; set; }
        public District(string name, int districtID)
        {
            Name = name;
            DistrictID = districtID;
        }
    }
}
