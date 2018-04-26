using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace CatCommuter
{
    class BusStop
    {
        public string name { get; }
        public BasicGeoposition location { get; } //TODO
        IDictionary<BusLine, ISet<DateTime>> busLines { get; }


        public BusStop(string name, IDictionary<BusLine, ISet<DateTime>> busLines, BasicGeoposition location)
        {
            this.name = name;
            this.busLines = busLines;
            this.location = location;
        }

        public ISet<DateTime> getTimes(BusLine busLine)
        {
            ISet<DateTime> returnSet = null;
            busLines.TryGetValue(busLine, out returnSet);
            return returnSet;
        }
    }
}
