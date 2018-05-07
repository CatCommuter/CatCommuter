using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace CatCommuter
{
    public class BusStop
    {
        public string name { get; }
        public BasicGeoposition location { get; } //TODO
        IDictionary<BusLine, ISet<DateTime>> busLines { get; }
        public ISet<DateTime> times
        { get
            {
                ISet<DateTime> returnSet = null;
                busLines.TryGetValue(Preferences.toEditBusLine, out returnSet);
                return returnSet;
            }
        }

        public BusStop(string name, IDictionary<BusLine, ISet<DateTime>> busLines, BasicGeoposition location)
        {
            this.name = name;
            if (busLines != null)
                this.busLines = busLines;
            else
                this.busLines = new Dictionary<BusLine, ISet<DateTime>>();
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
