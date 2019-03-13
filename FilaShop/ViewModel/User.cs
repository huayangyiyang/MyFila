using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilaShop.User
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string VCode { get; set; }
        public string Nickname { get; set; }

        public string returnurl { get; set; }
    }
}