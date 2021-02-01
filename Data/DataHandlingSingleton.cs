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
    /// Date last modified: 10/12/2020
    /// This class provides a method for exporting the data to a csv file
    /// It uses the Singleton design pattern.
    /// </summary>
    class DataHandlingSingleton
    {
        // Instance of class
        private static DataHandlingSingleton instance;

        private StreamWriter writer = new StreamWriter(File.OpenWrite(@"D:\C# Coursework\TrackTrace\Data2.csv"));
        private string path = @"D:\C# Coursework\TrackTrace\Data.csv";

        // Private constructor - cannot use new keyword
        private DataHandlingSingleton() { }

        public static DataHandlingSingleton Instance
        {
            get
            {
                // Check if instance has been created already
                if (instance == null)
                    // If not then create new instance 
                    instance = new DataHandlingSingleton();
                return instance;
            }
        }
        public void ImportData()
        {
            // Method for Importing all the data from the Data.csv file if it exists.
            if (File.Exists(path))
            {
                string currentLine = "";
                UserSingleton user = UserSingleton.Instance;
                LocationSingleton location = LocationSingleton.Instance;
                VisitSingleton visit = VisitSingleton.Instance;
                ContactSingleton contact = ContactSingleton.Instance;
                using (StreamReader reader = new StreamReader(path))
                {
                    while ((currentLine = reader.ReadLine()) != null)
                    {
                        var values = currentLine.Split(',');
                        if (values[0] == "User:")
                        {
                            string userIdTemp = values[1];
                            string phoneNumberTemp = values[2];
                            user.ImportUser(userIdTemp, phoneNumberTemp);
                        }
                        else if (values[0] == "Location:")
                        {
                            string locationNameTemp = values[1];
                            location.NewLocation(locationNameTemp);
                        }
                        else if (values[0] == "Visit:")
                        {
                            string userIdTemp = values[1];
                            string dateTemp = values[2];
                            string timeTemp = values[3];
                            string locationTemp = values[4];
                            visit.RecordVisit(userIdTemp, dateTemp, timeTemp, locationTemp);
                        }
                        else if (values[0] == "Contact:")
                        {
                            string individual1Temp = values[1];
                            string individual2Temp = values[2];
                            string individual2Number = values[3];
                            string dateTemp = values[4];
                            string timeTemp = values[5];
                            contact.RecordContact(individual1Temp, individual2Temp, individual2Number, dateTemp, timeTemp);
                        }
                    }
                }
            }
        }
        public void GenerateCSVUser(UserSingleton newUser)
        {
            // Method for creating the Data.csv file if it does not exist already and adding any new user data to the file.
            string prefix = "User:";
            if(!File.Exists(path))
            {
                using (StreamWriter writer = File.CreateText(path))
                {
                    var newLine = string.Format("{0},{1},{2}", prefix, newUser.UserId, newUser.PhoneNumber);
                    writer.Write(newLine);
                    writer.Write(Environment.NewLine);
                }
            } else
            {
                using (StreamWriter writer = File.AppendText(path))
                {
                    var newLine = string.Format("{0},{1},{2}", prefix, newUser.UserId, newUser.PhoneNumber);
                    writer.Write(newLine);
                    writer.Write(Environment.NewLine);
                }
            }
            writer.Close();
        }
        public void GenerateCSVLocation(LocationSingleton newLocation)
        {
            // Method for creating the Data.csv file if it does not exist already and adding any new location data to the file.
            string prefix = "Location:";
            if (!File.Exists(path))
            {
                using (StreamWriter writer = File.CreateText(path))
                {
                    var newLine = string.Format("{0},{1}", prefix, newLocation.LocationName);
                    writer.Write(newLine);
                    writer.Write(Environment.NewLine);
                }
            }
            else
            {
                using (StreamWriter writer = File.AppendText(path))
                {
                    var newLine = string.Format("{0},{1}", prefix, newLocation.LocationName);
                    writer.Write(newLine);
                    writer.Write(Environment.NewLine);
                }
            }
            writer.Close();
        }

        public void GenerateCSVVisit(VisitSingleton newVisit)
        {
            // Method for creating the Data.csv file if it does not exist already and adding any new visit data to the file.
            string prefix = "Visit:";
            if (!File.Exists(path))
            {
                using (StreamWriter writer = File.CreateText(path))
                {
                    var newLine = string.Format("{0},{1},{2},{3},{4}", prefix, newVisit.UserId, newVisit.Date, newVisit.Time, newVisit.Location);
                    writer.Write(newLine);
                    writer.Write(Environment.NewLine);
                }
            }
            else
            {
                using (StreamWriter writer = File.AppendText(path))
                {
                    var newLine = string.Format("{0},{1},{2},{3},{4}", prefix, newVisit.UserId, newVisit.Date, newVisit.Time, newVisit.Location);
                    writer.Write(newLine);
                    writer.Write(Environment.NewLine);
                }
            }
            writer.Close();
        }

        public void GenerateCSVContact(ContactSingleton newContact)
        {
            // Method for creating the Data.csv file if it does not exist already and adding any new contact data to the file.
            string prefix = "Contact:";
            if (!File.Exists(path))
            {
                using (StreamWriter writer = File.CreateText(path))
                {
                    var newLine = string.Format("{0},{1},{2},{3},{4},{5}", prefix, newContact.Individual1, newContact.Individual2, newContact.PhoneNumber, newContact.Date, newContact.Time);
                    writer.Write(newLine);
                    writer.Write(Environment.NewLine);
                }
            }
            else
            {
                using (StreamWriter writer = File.AppendText(path))
                {
                    var newLine = string.Format("{0},{1},{2},{3},{4},{5}", prefix, newContact.Individual1, newContact.Individual2, newContact.PhoneNumber, newContact.Date, newContact.Time);
                    writer.Write(newLine);
                    writer.Write(Environment.NewLine);
                }
            }
            writer.Close();
        }
        public void GenerateCSVContactList(List<String> numbersList)
        {
            // Method for creating the Data.csv file if it does not exist already and adding any phone numbers of people who have been in contact with a specific individual.
            string prefix = "Contact List for User:";
            if (!File.Exists(path))
            {
                using (StreamWriter writer = File.CreateText(path))
                {
                    for (int i = 0; i < numbersList.Count; i++)
                    {
                        var newLine = string.Format("{0},{1}", prefix, numbersList[i]);
                        writer.Write(newLine);
                        writer.Write(Environment.NewLine);
                    }
                }
            }
            else
            {
                using (StreamWriter writer = File.AppendText(path))
                {
                    for (int i = 0; i < numbersList.Count; i++)
                    {
                        var newLine = string.Format("{0},{1}", prefix, numbersList[i]);
                        writer.Write(newLine);
                        writer.Write(Environment.NewLine);
                    }
                }
            }
            writer.Close();
        }
    }
}
