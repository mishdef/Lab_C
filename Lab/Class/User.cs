using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab.Interface;

namespace Lab.Class
{
    public abstract class User : IUserInfo, IPrintable
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public User() 
        { 
            Name = "User";
        }

        public User(string name)
        {
            Name = name;
        }

        public abstract string GetInfo();
    }
}
