using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTrace.Business
{
    class Event : Location
    {
        private string date;
        private string time;

        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        public string Time
        {
            get { return time; }
            set { time = value; }
        }
    }
}
