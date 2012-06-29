using System;
using System.Collections.Generic;
using Practice.Web.Model;

namespace Practice.Web.FakeData
{
    public static class AppFakeData
    {
        public static IList<User> Users { get; set; }

        static AppFakeData()
        {
            Users = new List<User>();
            Users.Add(new User(Guid.NewGuid(), "Sue"));
            Users.Add(new User(Guid.NewGuid(), "Tom"));
            Users.Add(new User(Guid.NewGuid(), "Jerry"));
            Users.Add(new User(Guid.NewGuid(), "Lucy"));
            Users.Add(new User(Guid.NewGuid(), "Jack"));
            Users.Add(new User(Guid.NewGuid(), "Thomas"));
        }
    }
}