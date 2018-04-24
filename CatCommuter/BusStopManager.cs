using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatCommuter
{
    class BusStopManager
    {
        private static BusStopManager Instance;

        ISet<BusStop> busStops = new HashSet<BusStop>();

        private BusStopManager()
        {
            //Load from storage

            //Create BusLine objects

            BusLine sampleLine = new BusLine("C2", new TimeSpan(20), new DateTime());

            //Create BusStop objects
                //Store BusLine objects in stops as needed.

            IDictionary<BusLine, ISet<DateTime>> busLines = new Dictionary<BusLine, ISet<DateTime>>();
            ISet<DateTime> times = new HashSet<DateTime>();
            times.Add(new DateTime());
            busLines.Add(sampleLine, times);
            busStops.Add(new BusStop("Muir Pass", busLines, "the location"));
        }

        public static BusStopManager GetBusStopManager
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new BusStopManager();

                }
                return Instance;
            }
        }

        public BusStop getBusStop(string location)
        {
            BusStop closestStop = busStops.GetEnumerator().Current;
            double distance = Double.MaxValue;

            foreach (BusStop busStop in busStops)
            {
                //Compare distance to closestStop
            }

            //if (distance == Double.MaxValue)
            //    return null;

            return closestStop;
        }

        /**
         * Returns a list of bus stops near the location, ordered by nearest distance first.
         */
        public IList<BusStop> getBusStops(string location, int maxDistanceInMiles)
        {
            return null;
        }
    }
}
