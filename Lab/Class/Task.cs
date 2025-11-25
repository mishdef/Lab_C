using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab.Interface;
using Lab.Enum;
using MyFunctions;

namespace Lab.Class
{
    public class Task : IPrintable, ICloneable
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set 
            { 
                if (value.Length < 2 && value.Length >= 20) throw new WarningException("Task name should be between 2 and 20 characters");
                else _name = value;
            }
        }

        public TaskStat CurrentStatus { get; set; } = TaskStat.ToDo;
        public User? Assignee { get; set; } = null;

        public Task()
        {
            Name = "Task";
        }
        public Task(string name)
        {
            Name = name;
        }
        public Task(string name, User assignee) : this(name)
        {
            Assignee = assignee;
        }

        public void AssignEmployee(User sessionUser, User assignee)
        {
            if (PermissionService.CanInteractWithProjectBoard(sessionUser))
            {
                Assignee = assignee;
            }
            else 
            {
                throw new WarningException("Only CEO and ProjectManager can assign employee to task");
            }
        }

        public void UnassignEmployee(User sessionUser)
        {
            if (PermissionService.CanInteractWithProjectBoard(sessionUser))
            {
                Assignee = null;
            }
            else
            {
                throw new WarningException("Only CEO and ProjectManager can unassign employee from task");
            }
        }
        public void ChangeName(User sessionUser, string name)
        {
            if (PermissionService.CanInteractWithProjectBoard(sessionUser))
            {
                Name = name;
            }
            else
            {
                throw new WarningException("Only CEO and ProjectManager can change task name");
            }
        }
        public void MoveTask(User assaignee, TaskStat NewStatus)
        {
            if (PermissionService.CanInteractWithTask(assaignee))
            {
                CurrentStatus = NewStatus;
            }
        }

        public string GetInfo()
        {
            string assigneeName = (Assignee != null) ? Assignee.Name : "Unassigned";
            return $"[Task] - {Name} | Status: {CurrentStatus} | Assignee: {assigneeName}";
        }

        public object Clone()
        {
            return new Task(Name, Assignee);
        }
    }
}