using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTrace.Business
{
    /// <summary>
    /// Author: Cameron Hunt
    /// Date last modified: 04/12/2020
    /// This class is for recording and storing new visits to locations. One method to record a new visit and one to find all visits.
    /// It uses the Singleton design pattern.
    /// </summary>
    [Serializable]
    class VisitSingleton : Event
    {
        private List<VisitSingleton> visits = new List<VisitSingleton>();
        private List<String> allVisits = new List<String>();
        private static VisitSingleton instance;
        protected VisitSingleton() { }
        public static VisitSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VisitSingleton();
                }
                return instance;
            }
        }
        public List<VisitSingleton> Visits
        {
            get { return visits; }
        }
        public void RecordVisit(string user, string date, string time, string location)
        {
            // Method for creating a new Visit object and adding it to the list.
            VisitSingleton visit = new VisitSingleton();
            visit.UserId = user;
            visit.Date = date;
            visit.Time = time;
            visit.Location = location;
            visits.Add(visit);
        }
        public List<String> findAllVisitors(string location, string firstDate, string secondDate, string firstTime, string secondTime)
        {
            // Method for finding all visitors to a specified location between two dates and times.
            // CHECK CODE - MAY BE BUGGY
            User user = User.Instance;
            foreach(User aUser in user.Users)
            {
                foreach(VisitSingleton aVisit in visits)
                {
                    if (aUser.UserId == aVisit.UserId && aVisit.Location == location)
                    {
                        // Setting the phone number for the visiter to their correct number
                        aVisit.PhoneNumber = aUser.PhoneNumber;
                        if(!allVisits.Contains(aVisit.PhoneNumber) && DateTime.Parse(aVisit.Date) >= DateTime.Parse(firstDate) && DateTime.Parse(aVisit.Date) <= DateTime.Parse(secondDate) && 
                            DateTime.Parse(aVisit.Time) >= DateTime.Parse(firstTime) && DateTime.Parse(aVisit.Time) <= DateTime.Parse(secondTime)) 
                            allVisits.Add(aVisit.PhoneNumber);
                    }                       
                }
            }
            return allVisits;
        }
    }
}
