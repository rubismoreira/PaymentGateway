using System;
using CO.AcessControl.Core.Entities;

namespace CO.AcessControl.Core.Service
{
    public interface IUserService
    {
        User Login(string username, string password);

        bool AuthorizeUser(int userId, string policy);
    }
}