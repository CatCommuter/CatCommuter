using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatCommuter
{
    class BusStop
    {
        ISet<BusLine> busLines = new HashSet<BusLine>();

        public ISet<BusLine> GetBusLines()
        {
            return busLines;
        }

        public void getTime(BusStop stop)
        {
            //TODO
        }

        public void getLocation()
        {
            //TODO
        }
    }
}
