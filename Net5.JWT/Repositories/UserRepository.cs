﻿using Net5.JWT.Models;
using System.Collections.Generic;
using System.Linq;

namespace Net5.JWT.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = "goku", Password = "goku", Role = "manager" });
            users.Add(new User { Id = 2, Username = "vejeta", Password = "vejeta", Role = "employee" });
            users.Add(new User { Id = 3, Username = "kuririn", Password = "kuririn", Role = "tester" });
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
        }
    }
}
