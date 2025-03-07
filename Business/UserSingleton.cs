﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTrace.Business
{
    /// <summary>
    /// Author: Cameron Hunt
    /// Date last modified: 09/12/2020
    /// This class is for creating and storing new users. It has one method for creating a new user.
    /// It uses the Singleton design pattern.
    /// </summary>
    [Serializable]
    public class UserSingleton
    {
        private List<UserSingleton> users = new List<UserSingleton>();
        private List<int> idList = new List<int>();
        private static UserSingleton instance;
        private UserSingleton currentUser;
        private string userId;
        private string phoneNumber;
        protected UserSingleton() { }

        public static UserSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserSingleton();
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
        public List<UserSingleton> Users
        {
            get { return users; }
        }
        public UserSingleton CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }
        public void NewUser(string number)
        {
            // Method for creating a new User object and adding it to the list. Each user is assigned a unique random number for their userId.
            UserSingleton user = new UserSingleton();
            // Generate new UNIQUE user id
            Random rand = new Random();
            int id = rand.Next(9999);
            idList.Add(id);
            // If the generated id already exists, keep generating new ids until it no longer exists to make sure it is unqiue.
            while (idList.Contains(id))
                id = rand.Next(9999);
            user.UserId = Convert.ToString(id);
            user.PhoneNumber = number;
            // Sets the Current User which has just been created for use in the DataHandlingSingleton Class
            CurrentUser = user;
            users.Add(user);
        }
        public void ImportUser(string userId, string number)
        {
            // Method for creating a new User object using the imported UserId and Phone Number.
            UserSingleton user = new UserSingleton();
            user.UserId = userId;
            user.PhoneNumber = number;
            users.Add(user);
        }
    }
}
