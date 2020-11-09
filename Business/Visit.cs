using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTrace.Business
{
    class Visit : Event
    {
        private List<Visit> vists = new List<Visit>();
        public List<Visit> Visits
        {
            get { return vists; }
        }
        public void RecordVisit(string user, string date, string time, string location)
        {
            Visit visit = new Visit();
            visit.UserId = user;
            visit.Date = date;
            visit.Time = time;
            visit.LocationName = location;
            Visits.Add(visit);
        }
    }
}
