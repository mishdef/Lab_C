using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab.Interface;
using Lab.Enum;

namespace Lab.Class
{
    public class ProjectManager : User
    {
        public ProjectManager()
        {
            Name = "ProjectManager";
        }

        public ProjectManager(string name) : base(name) {  }

        public override string GetInfo()
        {
            return $"[Project Manager] - {Name}";
        }
    }
}
