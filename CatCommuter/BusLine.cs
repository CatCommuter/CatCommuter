using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CatCommuter
{
    public class BusLine
    {
        public string name { get; set; }
        public DateTime startTime { get; } //date and time of current startTime for this week
        public TimeSpan timeSpan { get; }
        public IList<BusStop> busStops = new List<BusStop>();

        public BusLine(string name, TimeSpan timeSpan, DateTime startTime)
        {
            this.name = name;
            this.timeSpan = timeSpan;
            this.startTime = startTime;
        }

        public void addStop(BusStop busStop)
        {
            busStops.Add(busStop);
        }
    }
}
