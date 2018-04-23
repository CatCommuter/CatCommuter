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
        public DateTime startTime { get; } //date and time of current startTime for this week
        public TimeSpan timeSpan { get; }
        public ISet<BusStop> busStops { get; set; }

        public BusLine(string name, TimeSpan timeSpan, DateTime startTime)
        {
            this.name = name;
            this.timeSpan = timeSpan;
            this.busStops = new HashSet<BusStop>();
            this.startTime = startTime;
        }
    }
}
