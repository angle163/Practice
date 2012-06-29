using System;

namespace Practice.Web.Model
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Username { get; set; }

        public User()
        {
            this.Id = Guid.NewGuid();
        }

        public User(string username)
            : this(Guid.NewGuid(), username)
        {
        }

        public User(Guid id, string username)
        {
            this.Id = id;
            this.Username = username;
        }
    }
}