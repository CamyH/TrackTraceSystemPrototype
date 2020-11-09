using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTrace.Business
{
    class Contact : Event
    {
        private List<Contact> contacts = new List<Contact>();
        private List<String> numbers = new List<String>();
        private string individual1;
        private string individual2;

        public List<Contact> Contacts
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

        public void RecordContact(string person1Id, string person2Id, string person2PhoneNumber, string date, string time)
        {
            Contact newContact = new Contact();
            newContact.Individual1 = person1Id;
            newContact.Individual2 = person2Id;
            newContact.PhoneNumber = person2PhoneNumber;
            newContact.Date = date;
            newContact.Time = time;
            contacts.Add(newContact);
        }
        public List<String> GetPhoneNumbers(string date, string time)
        {
            foreach(Contact aContact in contacts)
            {
                if(DateTime.Parse(aContact.Date) >= DateTime.Parse(date) && DateTime.Parse(aContact.Time) >= DateTime.Parse(time))
                {
                    numbers.Add(aContact.PhoneNumber);
                }

            }
            return numbers;
        }
    }
}
