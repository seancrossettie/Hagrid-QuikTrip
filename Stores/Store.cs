using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hagrid_QuikTrip.Stores
{
    class Store
    {
        public int StoreID { get; set; }
        public int DistrictID { get; set; }
        public double GasCurrentQuarterlySales { get; set; } = 0;
        public double GasCurrentYearlySales { get; set; } = 0;
        public double QuarterlySales { get; set; } = 0;
        public double YearlySales { get; set; } = 0;

        public Store(int storeID)
        {
            StoreID = storeID;
        }

        public Store(int storeID, int districtID)
        {
            StoreID = storeID;
            DistrictID = districtID;
        }
    }
}
