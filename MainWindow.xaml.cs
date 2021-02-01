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
    /// Date of last modification: 10/12/2020;
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Importing all data contained within Data.csv file
            UserSingleton user = UserSingleton.Instance;
            LocationSingleton location = LocationSingleton.Instance;
            VisitSingleton visit = VisitSingleton.Instance;
            ContactSingleton contact = ContactSingleton.Instance;
            DataHandlingSingleton data = DataHandlingSingleton.Instance;
            // Invoke method to import all data from file
            data.ImportData();
            foreach(UserSingleton aUser in user.Users)
            {
                // Iterate through all user objects and populate user lists
                // Input validation to prevent duplicates
                if (!userList.Items.Contains(aUser.UserId))
                    userList.Items.Add(aUser.UserId);
                if (!individualList1.Items.Contains(aUser.UserId))
                    individualList1.Items.Add(aUser.UserId);
                if (!individualList2.Items.Contains(aUser.UserId))
                    individualList2.Items.Add(aUser.UserId);
                if (!chooseIndividualList.Items.Contains(aUser.UserId))
                    chooseIndividualList.Items.Add(aUser.UserId);
            }
            foreach(LocationSingleton aLocation in location.Locations)
            {
                // Iterate through all location objects and populate location lists
                // Input validation to prevent duplicates
                if (!locationList.Items.Contains(aLocation.LocationName))
                    locationList.Items.Add(aLocation.LocationName);
                if (!chooseVisitLocationList.Items.Contains(aLocation.LocationName))
                    chooseVisitLocationList.Items.Add(aLocation.LocationName);
            }
            foreach(VisitSingleton aVisit in visit.Visits)
            {
                // Iterate through all visit objects and populate visit lists
                // Input validation to prevent duplicates
                if (!chooseVisitDateList.Items.Contains(aVisit.Date))
                    chooseVisitDateList.Items.Add(aVisit.Date);
                if (!chooseVisitDate2List.Items.Contains(aVisit.Date))
                    chooseVisitDate2List.Items.Add(aVisit.Date);
                if (!chooseVisitTimeList.Items.Contains(aVisit.Time))
                    chooseVisitTimeList.Items.Add(aVisit.Time);
                if (!chooseVisitTime2List.Items.Contains(aVisit.Time))
                    chooseVisitTime2List.Items.Add(aVisit.Time);
            }
            foreach(ContactSingleton aContact in contact.Contacts)
            {
                // Iterate through all contact objects and populate contact lists
                // Input validation to prevent duplicates
                if (!chooseIndividualList.Items.Contains(aContact.Individual1))
                    chooseIndividualList.Items.Add(aContact.Individual1);
                if (!chooseDateList.Items.Contains(aContact.Date))
                    chooseDateList.Items.Add(aContact.Date);
                if (!chooseTimeList.Items.Contains(aContact.Time))
                    chooseTimeList.Items.Add(aContact.Time);
            }
        }

        private void newIndividualBtn_Click(object sender, RoutedEventArgs e)
        {
            UserSingleton user = UserSingleton.Instance;
            DataHandlingSingleton export = DataHandlingSingleton.Instance;
            int result = 0;
            // Input Validation. Standard UK phone number length is 11 digits
            bool validation = int.TryParse(phoneNumberTxt.Text, out result); 
            if (phoneNumberTxt.Text.Length == 11 && validation == false)
            {
                user.NewUser(phoneNumberTxt.Text);
                // Output data to CSV
                export.GenerateCSVUser(user.CurrentUser);
            }else
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
            // Reset text field
            phoneNumberTxt.Text = "";
        }

        private void newLocationBtn_Click(object sender, RoutedEventArgs e)
        {
            LocationSingleton newLocation = LocationSingleton.Instance;
            DataHandlingSingleton export = DataHandlingSingleton.Instance;
            // Invoking NewLocation method
            newLocation.NewLocation(locationTxt.Text);
            // Adding locations to location combo boxes, unless they already exist
            foreach(LocationSingleton aLocation in newLocation.Locations)
            {
                if (!locationList.Items.Contains(aLocation.LocationName))
                    locationList.Items.Add(aLocation.LocationName);
            }
            // Reset Text Field
            locationTxt.Text = "";
            // Output data to CSV
            export.GenerateCSVLocation(newLocation.CurrentLocation);
        }

        private void checkInBtn_Click(object sender, RoutedEventArgs e)
        {
            VisitSingleton visit = VisitSingleton.Instance;
            DataHandlingSingleton export = DataHandlingSingleton.Instance;
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
            export.GenerateCSVVisit(visit.CurrentVisit);
        }

        private void recordContactBtn_Click(object sender, RoutedEventArgs e)
        {
            ContactSingleton newContact = ContactSingleton.Instance;
            UserSingleton user = UserSingleton.Instance;
            DataHandlingSingleton export = DataHandlingSingleton.Instance;
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
                    individual1UserId = aUser.UserId;
                } else if(aUser.UserId.Equals(individualList2.Text))
                {
                    individual2UserId = aUser.UserId;
                    individual2PhoneNumber = aUser.PhoneNumber;
                }
                    
            }
            newContact.RecordContact(individual1UserId, individual2UserId, individual2PhoneNumber, date, time);
            // Reset displayed text
            individualList1.Text = "";
            individualList2.Text = "";
            // Output data to CSV
            export.GenerateCSVContact(newContact.CurrentContact);
        }

        private void generateListBtn_Click(object sender, RoutedEventArgs e)
        {
            string date = chooseDateList.Text;
            string time = chooseTimeList.Text;
            ContactSingleton contact = ContactSingleton.Instance;
            DataHandlingSingleton export = DataHandlingSingleton.Instance;
            List<String> numbers = contact.GetPhoneNumbers(date, time);          
            for (int i = 0; i < numbers.Count; i++)
            {
                contactList.Items.Add(numbers[i]);
            }
            // Output to CSV
            export.GenerateCSVContactList(numbers);
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
            allVisits = visit.FindAllVisitors(location, date, date2, time, time2);
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
