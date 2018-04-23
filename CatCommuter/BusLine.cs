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
        public TimeSpan timeSpan { get; }
        private ISet<BusStop> busStops { get; }

        public BusLine(string name, TimeSpan timeSpan, ISet<BusStop> busStops)
        {
            this.name = name;
            this.timeSpan = timeSpan;
            this.busStops = busStops;
        }
    }
}
