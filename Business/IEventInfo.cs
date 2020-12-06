using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTrace.Business
{
    /// <summary>
    /// Author: Cameron Hunt
    /// Date last modified: 06/12/2020
    /// This interface contains the date, time and location properties used by VisitSingleton and ContactSingleton.
    /// </summary>
    public interface IEventInfo 
    {
        string Date
        {
            get;
            set;
        }
        string Time
        {
            get;
            set;
        }
        string Location
        {
            get;
            set;
        }
    }
}
