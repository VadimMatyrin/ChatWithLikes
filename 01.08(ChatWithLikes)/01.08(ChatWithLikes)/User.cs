using System;
using System.Collections.Generic;
using System.Text;

namespace ChatWithLikes
{
    public class User
    {
        public int User_id { get; private set; }
        public string Username { get; private set; }

        public User(int user_id, string username)
        {
            User_id = user_id;
            Username = username;
        }
    }
}
