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
    /// Date of last modification: 08/12/2020;
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
            UserSingleton user = UserSingleton.Instance;
            GenerateCSVSingleton export = GenerateCSVSingleton.Instance;
            // Input Validation. Standard UK phone number length is 11 digits
            if (phoneNumberTxt.Text.Length == 11)
                user.NewUser(phoneNumberTxt.Text);
            else
                // Display error message
                MessageBox.Show("Invalid UK Phone Number. Try again.");
            // Adding User IDs to all user combo boxes, unless they already exist
            foreach(UserSingleton aUser in user.Users)
            {
                if (!userList.Items.Contains(aUser.UserId))
                {
                    userList.Items.Add(aUser.UserId);
                    individualList1.Items.Add(aUser.UserId);
                    individualList2.Items.Add(aUser.UserId);
                }
            }
            // Output data to CSV
            export.GenerateCSVUser(user.Users);
        }

        private void newLocationBtn_Click(object sender, RoutedEventArgs e)
        {
            LocationSingleton newLocation = LocationSingleton.Instance;
            GenerateCSVSingleton export = GenerateCSVSingleton.Instance;
            // Invoking NewLocation method
            newLocation.NewLocation(locationTxt.Text);
            // Adding locations to location combo boxes, unless they already exist
            foreach(LocationSingleton aLocation in newLocation.Locations)
            {
                if (!locationList.Items.Contains(aLocation.LocationName))
                    locationList.Items.Add(aLocation.LocationName);
            }
            // Output data to CSV
            export.GenerateCSVLocation(newLocation.Locations);
        }

        private void checkInBtn_Click(object sender, RoutedEventArgs e)
        {
            VisitSingleton visit = VisitSingleton.Instance;
            GenerateCSVSingleton export = GenerateCSVSingleton.Instance;
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
            // Output data to CSV
            export.GenerateCSVVisit(visit.Visits);
        }

        private void recordContactBtn_Click(object sender, RoutedEventArgs e)
        {
            ContactSingleton newContact = ContactSingleton.Instance;
            UserSingleton user = UserSingleton.Instance;
            GenerateCSVSingleton export = GenerateCSVSingleton.Instance;
            string individual1UserId = "";
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
            foreach(UserSingleton aUser in user.Users)
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
            // Output data to CSV
            export.GenerateCSVContact(newContact.Contacts);
        }

        private void generateListBtn_Click(object sender, RoutedEventArgs e)
        {
            string date = chooseDateList.Text;
            string time = chooseTimeList.Text;
            ContactSingleton contact = ContactSingleton.Instance;
            List<String> numbers = contact.GetPhoneNumbers(date, time);          
            for (int i = 0; i < numbers.Count; i++)
            {
                contactList.Items.Add(numbers[i]);
            }
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
            // Make sure allVisits list is empty
            allVisits.Clear();
            allVisits = visit.findAllVisitors(location, date, date2, time, time2);
            for(int i = 0; i < allVisits.Count; i++)
            {
                visitedList.Items.Add(allVisits[i]);
            }
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            visitedList.Items.Clear();
        }
    }
}
