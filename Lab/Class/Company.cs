using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab.Interface;
using MyFunctions;

namespace Lab.Class
{
    public class Company
    {
        public string Name { get; set; }
        public List<User> Employees { get; set; } = new List<User>();
        public List<ProjectBoard> ProjectBoards { get; } = new List<ProjectBoard>();

        public Company(User CEO)
        {
            Name = "Company";
            AddEmployee(CEO, CEO);
        }

        public Company(User CEO, string name) : this(CEO)
        {
            Name = name;
        }

        public void CreateProjectBoard(User sessionUser, string name)
        {
            if (PermissionService.CanInteractWithCompany(sessionUser))
            {
                ProjectBoards.Add(new ProjectBoard(name));
            }
            else
            {
                throw new WarningException("Only CEO can create project board");
            }
        }

        public void AddEmployee(User sessionUser, User newEmployee)
        {
            if (PermissionService.CanInteractWithCompany(sessionUser))
            {
                Employees.Add(newEmployee);
            }
            else
            {
                throw new WarningException("Only CEO can add employee");
            }
        }
        public void ChangeName(User sessionUser, string name)
        {
            if (PermissionService.CanInteractWithCompany(sessionUser))
            {
                Name = name;
            }
            else
            {
                throw new WarningException("Only CEO can change name");
            }
        }
        public void RemoveEmployee(User sessionUser, User employee)
        {
            if (sessionUser == employee)
            {
                throw new WarningException("You can't remove yourself");
            }
            if (PermissionService.CanInteractWithCompany(sessionUser))
            {
                Employees.Remove(employee);
            }
            else
            {
                throw new WarningException("Only CEO can remove employee");
            }
        }
        public void RemoveProjectBoard(User sessionUser, ProjectBoard projectBoard)
        {
            if (PermissionService.CanInteractWithCompany(sessionUser))
            {
                ProjectBoards.Remove(projectBoard);
            }
            else
            {
                throw new WarningException("Only CEO can remove project board");
            }
        }
    }
}
