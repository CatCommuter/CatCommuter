using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatCommuter
{
    class Route
    {
        BusStop from;
        BusStop to;
        BusLine busLine;

        public Route(BusStop from, BusStop to, BusLine busLine)
        {
            this.from = from;
            this.to = to;
            this.busLine = busLine;
        }
        BusStop getFrom()
        {
            return from;
        }

        BusStop getTo()
        {
            return to;
        }

        BusLine getBusLine()
        {
            return busLine;
        }
    }
}
