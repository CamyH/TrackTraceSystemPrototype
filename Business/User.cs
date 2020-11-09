using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTrace.Business
{
    class User
    {
        private List<User> users = new List<User>();
        private string userId;
        private string phoneNumber;

        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        public List<User> Users
        {
            get { return users; }
        }
        public void NewUser(string number)
        {
            User user = new User();
            // Generate new UNIQUE user id
            Random rand = new Random();
            int id = rand.Next(9999);
            user.UserId = Convert.ToString(id);
            user.PhoneNumber = number;
            users.Add(user);
        }
    }
}
