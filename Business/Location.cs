using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTrace.Business
{
    class Location : User
    {
        private List<Location> locations = new List<Location>();
        private string locationName;

        public string LocationName
        {
            get { return locationName; }
            set { locationName = value; }
        }
        public List<Location> Locations
        {
            get { return locations; }
        }
        public void NewLocation(string location)
        {
            Location newLocation = new Location();
            newLocation.LocationName = location;
            locations.Add(newLocation);
        }
    }
}
