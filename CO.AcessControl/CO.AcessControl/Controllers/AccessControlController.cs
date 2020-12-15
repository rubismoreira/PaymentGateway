using CO.AcessControl.Core.Entities;
using CO.AcessControl.Core.Service;
using CO.AcessControl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using CO.AcessControl.Service;

namespace CO.AcessControl.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccessControlController : ControllerBase
    {
        private readonly IConfiguration _config;

        private readonly IJwtHandlerService _jwtHandlerService;

        private readonly IUserService _userService;

        public AccessControlController(IConfiguration config, IJwtHandlerService jwtHandlerService, IUserService userService)
        {
            this._config = config;
            this._jwtHandlerService = jwtHandlerService;
            this._userService = userService;
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserLoginRequest login)
        {
            IActionResult response = Unauthorized();
            User user = this._userService.Login(login.Username, login.Password);
            if (user != null)
            {
                var tokenString = this._jwtHandlerService.GenerateJWTToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            return response;
        }
    }
}

