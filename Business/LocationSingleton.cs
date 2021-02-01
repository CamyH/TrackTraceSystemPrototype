using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTrace.Business
{
    /// <summary>
    /// Author: Cameron Hunt
    /// Date last modified: 09/12/2020
    /// This class is for creating and storing new locations. It contains one method for creating and storing a new location.
    /// It uses the Singleton design pattern.
    /// </summary>
    [Serializable]
    public class LocationSingleton
    {
        private List<LocationSingleton> locations = new List<LocationSingleton>();
        private string locationName;
        private static LocationSingleton instance;
        private LocationSingleton currentLocation;

        protected LocationSingleton() { }

        public static LocationSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LocationSingleton();
                }
                return instance;
            }
        }
        public string LocationName
        {
            get { return locationName; }
            set { locationName = value; }
        }
        public List<LocationSingleton> Locations
        {
            get { return locations; }
        }
        public LocationSingleton CurrentLocation
        {
            get { return currentLocation; }
            set { currentLocation = value; }
        }
        public void NewLocation(string location)
        {
            LocationSingleton newLocation = new LocationSingleton();
            newLocation.LocationName = location;
            // Sets the Current Location which has just been created for use in the DataHandlingSingleton Class
            CurrentLocation = newLocation;
            locations.Add(newLocation);
        }
    }
}
