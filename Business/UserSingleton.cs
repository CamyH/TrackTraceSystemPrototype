using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTrace.Business
{
    /// <summary>
    /// Author: Cameron Hunt
    /// Date last modified: 03/12/2020
    /// This class is for creating and storing new users. It has one method for creating a new user.
    /// It uses the Singleton design pattern.
    /// </summary>
    [Serializable]
    public class User
    {
        private List<User> users = new List<User>();
        private List<int> idList = new List<int>();
        private static User instance;
        private string userId;
        private string phoneNumber;
        protected User() { }

        public static User Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new User();
                }
                return instance;
            }
        }
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
            // Method for creating a new User object and adding it to the list. Each user is assigned a unique random number for their userId.
            User user = new User();
            // Generate new UNIQUE user id
            Random rand = new Random();
            int id = rand.Next(9999);
            // While the generated id already exists, keep generating new ids to make sure it is unqiue.
            while (idList.Contains(id))
                id = rand.Next(9999);
            user.UserId = Convert.ToString(id);
            user.PhoneNumber = number;
            users.Add(user);
        }
    }
}
