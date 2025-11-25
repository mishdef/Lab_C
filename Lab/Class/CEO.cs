using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab.Interface;

namespace Lab.Class
{
    public class CEO : User
    {
        public CEO()
        {
            Name = "CEO";
        }

        public CEO(string name) : base(name) { }

        public override string GetInfo()
        {
            return $"[CEO] - {Name}";
        }
    }
}
