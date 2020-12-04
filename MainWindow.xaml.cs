using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrackTrace.Business;
using TrackTrace.Data;

namespace TrackTrace
{
    /// <summary>
    /// Author: Cameron Hunt
    /// Date of last modification: 04/12/2020;
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void newIndividualBtn_Click(object sender, RoutedEventArgs e)
        {
            User user = User.Instance;
            Serialize serialize = new Serialize();
            // Input Validation. Standard UK phone number length is 11
            if (phoneNumberTxt.Text.Length == 11)
                user.NewUser(phoneNumberTxt.Text);
            else
                // Display error message
                MessageBox.Show("Invalid UK Phone Number. Try again.");
            // Adding User IDs to all user combo boxes, unless they already exist
            foreach(User aUser in user.Users)
            {
                if (!userList.Items.Contains(aUser.UserId))
                {
                    userList.Items.Add(aUser.UserId);
                    individualList1.Items.Add(aUser.UserId);
                    individualList2.Items.Add(aUser.UserId);
                }
            }
        }

        private void newLocationBtn_Click(object sender, RoutedEventArgs e)
        {
            LocationSingleton newLocation = LocationSingleton.Instance;
            Serialize serialize = new Serialize();
            // Invoking NewLocation method
            newLocation.NewLocation(locationTxt.Text);
            // Adding locations to location combo boxes, unless they already exist
            foreach(LocationSingleton aLocation in newLocation.Locations)
            {
                if (!locationList.Items.Contains(aLocation.LocationName))
                    locationList.Items.Add(aLocation.LocationName);
            }
        }

        private void checkInBtn_Click(object sender, RoutedEventArgs e)
        {
            VisitSingleton visit = VisitSingleton.Instance;
            string date = Convert.ToString(System.DateTime.Now.ToString("dd/MM/yyyy"));
            string time = System.DateTime.Now.ToShortTimeString();
            // Adding location, date, time to combo boxes, unless already exist to avoid duplication
            if (!chooseVisitLocationList.Items.Contains(locationList.Text))
                chooseVisitLocationList.Items.Add(locationList.Text);
            if (!chooseVisitDateList.Items.Contains(date))
            {
                chooseVisitDateList.Items.Add(date);
                chooseVisitDate2List.Items.Add(date);
            }
            if (!chooseVisitTimeList.Items.Contains(time))
            {
                chooseVisitTimeList.Items.Add(time);
                chooseVisitTime2List.Items.Add(time);
            }
            // Invoking RecordVisit method
            visit.RecordVisit(userList.Text, date, time, locationList.Text);
            foreach(VisitSingleton aVisit in visit.Visits)
            {
                contactList.Items.Add(aVisit.UserId);
                contactList.Items.Add(aVisit.PhoneNumber);
                contactList.Items.Add(aVisit.Location);
                contactList.Items.Add(aVisit.Date);
                contactList.Items.Add(aVisit.Time);
            }
        }

        private void recordContactBtn_Click(object sender, RoutedEventArgs e)
        {
            ContactSingleton newContact = ContactSingleton.Instance;
            User user = User.Instance;
            string individual1UserId = "";
            string individual1PhoneNumber = "";
            string individual2UserId = "";
            string individual2PhoneNumber = "";
            chooseIndividualList.Items.Add(individualList1.Text);
            // Separate date and time used for generating file
            string date = Convert.ToString(System.DateTime.Now.ToString("dd/MM/yyyy"));
            // Adding date of contact to date combo box, unless the date already exists to avoid duplicates
            if(!chooseDateList.Items.Contains(date))
                chooseDateList.Items.Add(date);
            string time = System.DateTime.Now.ToShortTimeString();
            // Adding time of contact to time combo box, unless the time already exists to avoid duplicates
            if (!chooseDateList.Items.Contains(time))
                chooseTimeList.Items.Add(time);
            foreach(User aUser in user.Users)
            {
                if(aUser.UserId.Equals(individualList1.Text))
                {
                    contactList.Items.Add(aUser.UserId);
                    individual1UserId = aUser.UserId;
                } else if(aUser.UserId.Equals(individualList2.Text))
                {
                    individual2UserId = aUser.UserId;
                    individual2PhoneNumber = aUser.PhoneNumber;
                }
                    
            }
            newContact.RecordContact(individual1UserId, individual2UserId, individual2PhoneNumber, date, time);
        }

        private void generateListBtn_Click(object sender, RoutedEventArgs e)
        {
            ContactSingleton contact = ContactSingleton.Instance;
            User user = User.Instance;
            List<String> numbers = new List<String>();
            string date = chooseDateList.Text;
            string time = chooseTimeList.Text;            
            numbers = contact.GetPhoneNumbers(date, time);
            for (int i = 0; i < numbers.Count; i++)
            {
                contactList.Items.Add(numbers[i]);
            }
        }

        private void ExportBtn_Click(object sender, RoutedEventArgs e)
        {
            GenerateCSVSingleton dataCSV = GenerateCSVSingleton.Instance;
            User user = User.Instance;
            LocationSingleton location = LocationSingleton.Instance;
            VisitSingleton visit = VisitSingleton.Instance;
            ContactSingleton contact = ContactSingleton.Instance;
            List<User> users = new List<User>();
            List<LocationSingleton> locations = new List<LocationSingleton>();
            List<VisitSingleton> visits = new List<VisitSingleton>();
            List<ContactSingleton> contacts = new List<ContactSingleton>();
            users = user.Users;
            locations = location.Locations;
            visits = visit.Visits;
            contacts = contact.Contacts;
            // Exporting Data to CSV file
            dataCSV.GenerateCSV(users, locations, visits, contacts);
            Serialize data = new Serialize();
            // Exporting Data using serialization
            data.ExportToFile(user.Users);
            //AllData allData = new AllData();
            //data.ExportToFile(allData);
            /*List<User> importedData = new List<User>();
            importedData = data.ImportFromFile();
            foreach(User aUser in importedData)
            {
                contactList.Items.Add(aUser.UserId);
                contactList.Items.Add(aUser.PhoneNumber);
            }*/
            //data.Serialization(users, eventsList, locations);
        }

        private void findVisitorsBtn_Click(object sender, RoutedEventArgs e)
        {
            VisitSingleton visit = VisitSingleton.Instance;
            List<String> allVisits = new List<String>();
            string date = chooseVisitDateList.Text;
            string date2 = chooseVisitDate2List.Text;
            string time = chooseVisitTimeList.Text;
            string time2 = chooseVisitTime2List.Text;
            string location = chooseVisitLocationList.Text;
            // Reset display box
            //visitedList.Items.Clear();
            // Make sure allVisits list is empty
            allVisits.Clear();
            allVisits = visit.findAllVisitors(location, date, date2, time, time2);
            for(int i = 0; i < allVisits.Count; i++)
            {
                visitedList.Items.Add(allVisits[i]);
            }
        }

        private void importBtn_Click(object sender, RoutedEventArgs e)
        {
            List<User> importedData = new List<User>();
            Serialize data = new Serialize();
            // Importing data
            importedData = data.ImportFromFile();
            foreach(User aUser in importedData)
            {
                userList.Items.Add(aUser.UserId);
                individualList1.Items.Add(aUser.UserId);
                individualList2.Items.Add(aUser.UserId);
                chooseIndividualList.Items.Add(aUser.UserId);
                dataBox.Items.Add(aUser.UserId);
                dataBox.Items.Add(aUser.PhoneNumber);
            }
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            visitedList.Items.Clear();
        }
    }
}
