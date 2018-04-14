using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatCommuter
{
    class BusLine
    {
        public string name { get; set; }
        private ISet<BusStop> busStops = new HashSet<BusStop>();

        public ISet<BusStop> getBusStops()
        {
            return busStops;
        }

        
    }
}
