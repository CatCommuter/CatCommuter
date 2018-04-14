using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatCommuter
{
    class BusLine
    {
        ISet<BusStop> busStops = new HashSet<BusStop>();

        ISet<BusStop> getBusStops()
        {
            return busStops;
        }
    }
}
