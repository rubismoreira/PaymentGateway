using System;
using System.Linq;
using CO.AcessControl.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace CO.AcessControl.AcessClient
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizeCOAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _policy;

        public AuthorizeCOAttribute(string policy)
        {
            this._policy = policy;
        }

        public void OnAuthorization(AuthorizationFilterContext authorizationFilterContext)
        {
            var services = authorizationFilterContext.HttpContext.RequestServices;
            var userService = services.GetService<IUserService>();
            var userId = Int32.Parse(authorizationFilterContext.HttpContext.User.Claims.First(x => x.Type == "id").Value);

            var authorized = userService.AuthorizeUser(userId, this._policy);
            if(!authorized)
                authorizationFilterContext.Result =new ForbidResult();
        }
    }
}