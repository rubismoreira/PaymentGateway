using System;
using System.Collections.Generic;
using System.Linq;
using CO.AcessControl.Core.Entities;

namespace CO.AcessControl.Core.Service
{
    public class UserService : IUserService
    {
        private List<User> appUsers = new List<User>
        {
            new User { Id = 1, Username = "UserRead", Password = "1234", Roles = new List<string>{ "ReadProcessPayment"} },
            new User { Id = 2, Username = "UserWrite", Password = "1234", Roles = new List<string> { "WriteProcessPayment" } },
            new User { Id = 3, Username = "UserFull", Password = "1234", Roles = new List<string> { "ReadProcessPayment", "WriteProcessPayment" } },
            new User { Id = 4, Username = "UserNone", Password = "1234", Roles = new List<string>() }
        };

        public User Login(string username, string password)
        {
            User user = appUsers.SingleOrDefault(x =>
                x.Username == username && x.Password == password);

            return user;
        }
        
        public bool AuthorizeUser(int userId, string policy)
        {
            var user = appUsers.Where(x => x.Id == userId).FirstOrDefault();
            if (user == null)
                return false;
            else
            {
                return user.Roles.Contains(policy);
            }
        }
    }
}