using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab.Interface;
using Lab.Enum;

namespace Lab.Class
{
    public class Employee : User
    {
        public Employee()
        {
            Name = "Employee";
        }

        public Employee(string name) : base(name)  {   }

        public override string GetInfo()
        {
            return $"[Employee] - {Name}";
        }
    }
}
