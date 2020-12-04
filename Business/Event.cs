using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTrace.Business
{
    /// <summary>
    /// Author: Cameron Hunt
    /// Date last modified: 03/12/2020
    /// This class contains inheritable properties for the classes VisitSingleton and ContactSingleton to use.
    /// </summary>
    [Serializable]
    public class Event : User
    {
        private string date;
        private string time;
        private string location;

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
        public string Location
        {
            get { return location; }
            set { location = value; }
        }
    }
}
