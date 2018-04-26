using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace CatCommuter
{
    class BusStopManager
    {
        private static BusStopManager instance;

        ISet<BusStop> busStops = new HashSet<BusStop>();
        ISet<BusLine> busLinesSet = new HashSet<BusLine>();
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
            busLinesSet.Add(sampleLine);
            BasicGeoposition position = new BasicGeoposition
            {
                Latitude = 1D,
                Longitude = 1D
            };
            busStops.Add(new BusStop("Muir Pass", busLines, position));
        }

        public static BusStopManager getInstance()
        {
            if (instance == null)
            {
                instance = new BusStopManager();
            }
            return instance;
        }

        public BusStop getBusStop(BasicGeoposition location)
        {
            BusStop closestStop = busStops.GetEnumerator().Current;
            double closestDistance = Double.MaxValue;

            foreach (BusStop busStop in busStops)
            {
                double distance = this.distance(location, closestStop.location);
                //Compare distance to closestStop
                if (distance < closestDistance)
                {
                    closestStop = busStop;
                    closestDistance = distance;
                }
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

        public ISet<BusLine> getBusLines()
        {
            return busLinesSet;
        }

        //https://stackoverflow.com/a/13429321
        private double distance(BasicGeoposition start, BasicGeoposition end)
        {
            if (double.IsNaN(start.Latitude) || double.IsNaN(start.Longitude) || double.IsNaN(end.Latitude) || double.IsNaN(end.Longitude))
            {
                throw new ArgumentException("LatitudeOrLongitudeIsNotANumber");
            }
            else
            {
                double latitude = start.Latitude * 0.0174532925199433;
                double longitude = start.Longitude * 0.0174532925199433;
                double num = end.Latitude * 0.0174532925199433;
                double longitude1 = end.Longitude * 0.0174532925199433;
                double num1 = longitude1 - longitude;
                double num2 = num - latitude;
                double num3 = Math.Pow(Math.Sin(num2 / 2), 2) + Math.Cos(latitude) * Math.Cos(num) * Math.Pow(Math.Sin(num1 / 2), 2);
                double num4 = 2 * Math.Atan2(Math.Sqrt(num3), Math.Sqrt(1 - num3));
                double num5 = 6376500 * num4;
                return num5;
            }
        }
    }
}
