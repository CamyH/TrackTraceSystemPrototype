using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackTrace.Business;

namespace TrackTrace.Data
{
    /// <summary>
    /// Author: Cameron Hunt
    /// Date last modified: 03/12/2020
    /// This class provides a method for exporting the data to a csv file
    /// It uses the Singleton design pattern.
    /// </summary>
    class GenerateCSVSingleton
    {
        // Instance of class
        private static GenerateCSVSingleton instance;

        // Private constructor - cannot use new keyword
        private GenerateCSVSingleton() { }

        public static GenerateCSVSingleton Instance
        {
            get
            {
                // Check if instance has been created already
                if (instance == null)
                    // If not then create new instance 
                    instance = new GenerateCSVSingleton();
                return instance;
            }
        }

        public void GenerateCSV(List<User> userData, List<LocationSingleton> locationData, List<VisitSingleton> visitData, List<ContactSingleton> contactData)
        {
            StreamWriter writer = new StreamWriter(File.OpenWrite(@"D:\C# Coursework\TrackTrace\Test.csv"));
            foreach (User user in userData)
            {
                writer.WriteLine("User Data:");
                writer.WriteLine(user.UserId);
                writer.WriteLine(user.PhoneNumber);
            }
            foreach (LocationSingleton location in locationData)
            {
                writer.WriteLine("");
                writer.WriteLine("Location:");
                writer.WriteLine(location.LocationName);
            }
            foreach (VisitSingleton visit in visitData)
            {
                writer.WriteLine("");
                writer.WriteLine("Visit Data:");
                writer.WriteLine(visit.UserId);
                writer.WriteLine(visit.Date);
                writer.WriteLine(visit.Time);
                writer.WriteLine(visit.Location);
            }
            foreach (ContactSingleton contact in contactData)
            {
                writer.WriteLine("");
                writer.WriteLine("Contact Data:");
                writer.WriteLine(contact.Individual1);
                writer.WriteLine(contact.Individual2);
                writer.WriteLine(contact.PhoneNumber);
                writer.WriteLine(contact.Date);
                writer.WriteLine(contact.Time);
            }
            writer.Close();
        }
    }
}
