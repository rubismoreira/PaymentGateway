using System;
using CO.AcessControl.Core.Entities;

namespace CO.AcessControl.Service
{
    public interface IJwtHandlerService
    {
        string GenerateJWTToken(User userInfo);
    }
}