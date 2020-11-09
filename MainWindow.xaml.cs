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
            User user = new User();
            user.NewUser(phoneNumberTxt.Text);
            foreach(User aUser in user.Users)
            {
                if(!userList.Items.Contains(aUser.UserId))
                {
                    userList.Items.Add(aUser.UserId);
                    individualList1.Items.Add(aUser.UserId);
                    individualList2.Items.Add(aUser.UserId);
                }
            }
        }

        private void newLocationBtn_Click(object sender, RoutedEventArgs e)
        {
            Location newLocation = new Location();
            newLocation.NewLocation(locationTxt.Text);
            foreach(Location aLocation in newLocation.Locations)
            {
                if (!locationList.Items.Contains(aLocation.LocationName))
                    locationList.Items.Add(aLocation.LocationName);
            }
        }

        private void checkInBtn_Click(object sender, RoutedEventArgs e)
        {
            Visit visit = new Visit();
            string date = Convert.ToString(System.DateTime.Now.ToString("dd/MM/yyyy"));
            string time = System.DateTime.Now.ToShortTimeString();
            visit.RecordVisit(userList.Text, date, time, locationList.Text);
            foreach(Visit aVisit in visit.Visits)
            {
                testingBox.Items.Add(aVisit.UserId);
                testingBox.Items.Add(aVisit.PhoneNumber);
                testingBox.Items.Add(aVisit.LocationName);
                testingBox.Items.Add(aVisit.Date);
                testingBox.Items.Add(aVisit.Time);
            }
        }

        private void recordContactBtn_Click(object sender, RoutedEventArgs e)
        {
            Contact newContact = new Contact();
            User user = new User();
            string individual1UserId = "";
            string individual1PhoneNumber = "";
            string individual2UserId = "";
            string individual2PhoneNumber = "";
            chooseIndividualList.Items.Add(individualList1.Text);
            // Separate date and time used for generating file
            string date = Convert.ToString(System.DateTime.Now.ToString("dd/MM/yyyy"));
            chooseDateList.Items.Add(date);
            string time = System.DateTime.Now.ToShortTimeString();
            chooseTimeList.Items.Add(time);
            foreach(User aUser in user.Users)
            {
                if(aUser.Equals(individualList1.Text))
                {
                    individual1UserId = aUser.UserId;
                } else if(aUser.Equals(individualList2.Text))
                {
                    individual2UserId = aUser.UserId;
                    individual2PhoneNumber = aUser.PhoneNumber;
                }
                    
            }
            newContact.RecordContact(individual1UserId, individual2UserId, individual2PhoneNumber, date, time);
            // Have a list of some sorts storing contacts for each individual. Then use that for generating CSV
            foreach (Contact aContact in newContact.Contacts)
            {
                testingBox.Items.Add(aContact.Individual1);
                testingBox.Items.Add(aContact.Individual2);
                testingBox.Items.Add(aContact.Date);
                testingBox.Items.Add(aContact.Time);
            }
        }

        private void generateCSVBtn_Click(object sender, RoutedEventArgs e)
        {
            // Currently not working
            Contact contact = new Contact();
            List<String> numbers = new List<String>();
            string date = chooseDateList.Text;
            string time = chooseTimeList.Text;            
            numbers = contact.GetPhoneNumbers(date, time);
            for (int i = 0; i < numbers.Count; i++)
            {
                testingBox.Items.Add(numbers[i]);
                testingBox.Items.Add("HELLO");
            }
        }
    }
}
