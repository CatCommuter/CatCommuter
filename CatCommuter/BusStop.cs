﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatCommuter
{
    class BusStop
    {
        public string name { get; }
        public string location { get; } //TODO
        IDictionary<BusLine, ISet<DateTime>> busLines { get; }


        public BusStop(string name, IDictionary<BusLine, ISet<DateTime>> busLines, string location)
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
