using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTrace.Business
{
    /// <summary>
    /// Author: Cameron Hunt
    /// Date last modified: 10/12/2020
    /// This class is for recording and storing contact between two users. 
    /// It has one method for recording a new contact and one for finding all phone numbers of contacts of a specific user.
    /// It uses the Singleton design pattern.
    /// </summary>
    [Serializable]
    class ContactSingleton : UserSingleton, IEventInfo
    {
        private List<ContactSingleton> contacts = new List<ContactSingleton>();
        private List<String> numbers = new List<String>();
        private static ContactSingleton instance;
        private ContactSingleton currentContact;
        private string individual1;
        private string individual2;
        protected ContactSingleton() { }

        public static ContactSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ContactSingleton();
                }
                return instance;
            }
        }
        public ContactSingleton CurrentContact
        {
            get { return currentContact; }
            set { currentContact = value; }
        }
        public List<ContactSingleton> Contacts
        {
            get { return contacts; }
        }
        public string Individual1
        {
            get { return individual1; }
            set { individual1 = value; }
        }
        public string Individual2
        {
            get { return individual2; }
            set { individual2 = value; }
        }
        // Implementing Interface properties
        public string Date { get; set; }
        public string Time { get; set; }
        public string Location { get; set; }
        public void RecordContact(string person1Id, string person2Id, string person2PhoneNumber, string date, string time)
        {
            // Method for creating a new Contact object and adding it to the list.
            ContactSingleton newContact = new ContactSingleton();
            newContact.Individual1 = person1Id;
            newContact.Individual2 = person2Id;
            newContact.PhoneNumber = person2PhoneNumber;
            newContact.Date = date;
            newContact.Time = time;
            CurrentContact = newContact;
            contacts.Add(newContact);
        }
        public List<String> GetPhoneNumbers(string date, string time)
        {
            // Method for finding all phone numbers of contacts on a specified date and time.
            foreach(ContactSingleton aContact in contacts)
            {
                if (DateTime.Parse(aContact.Date) >= DateTime.Parse(date) && DateTime.Parse(aContact.Time) >= DateTime.Parse(time) && !numbers.Contains(aContact.PhoneNumber))
                {
                    numbers.Add(aContact.PhoneNumber);
                }
            }
            return numbers;
        }
    }
}
