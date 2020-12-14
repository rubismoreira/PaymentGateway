using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CO.AcessControl.Models
{
    public class User
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public List<string> Roles { get; set; }
    }
}
