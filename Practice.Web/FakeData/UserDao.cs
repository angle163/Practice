using System;
using System.Collections.Generic;
using Practice.Web.Model;

namespace Practice.Web.FakeData
{
    public class UserDao
    {
        public bool Add(User user)
        {
            IList<User> users = AppFakeData.Users;
            if (IsExisted(user.Username))
            {
                return false;
            }

            users.Add(user);
            return true;
        }

        public bool IsExisted(string username)
        {
            IList<User> users = AppFakeData.Users;
            foreach (User u in users)
            {
                if (u.Username.Equals(username))
                {
                    return true;
                }
            }
            return false;
        }

        public IList<User> Get()
        {
            return AppFakeData.Users;
        }

        public User Get(Guid id)
        {
            foreach (User user in AppFakeData.Users)
            {
                if (user.Id == id)
                {
                    return user;
                }
            }
            return null;
        }

        public User Get(string username)
        {
            foreach (User user in AppFakeData.Users)
            {
                if (user.Username.Equals(username))
                {
                    return user;
                }
            }
            return null;
        }

        public bool Update(User user)
        {
            IList<User> users = AppFakeData.Users;
            foreach (User u in users)
            {
                if (u.Id == user.Id)
                {
                    u.Username = user.Username;
                    return true;
                }
            }
            return false;
        }

        public bool Delete(Guid id)
        {
            IList<User> users = AppFakeData.Users;
            foreach (User u in users)
            {
                if (u.Id == id)
                {
                    users.Remove(u);
                    return true;
                }
            }
            return false;
        }
    }
}