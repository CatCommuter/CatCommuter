using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatCommuter
{
    class BusStopManager
    {
        ISet<BusStop> busStops = new HashSet<BusStop>();

        public BusStopManager()
        {
            //Load from storage

            //Create BusLine objects

            //Create BusStop objects
                //Store BusLine objects in stops as needed.
        }

        public BusStop getBusStop(string location)
        {
            //TODO: return location
            return null;
        }

        /**
         * Returns a list of bus stops near the location, ordered by nearest distance first.
         */
        public IList<BusStop> getBusStops(string location, int maxDistance)
        {
            return null;
        }
    }
}
