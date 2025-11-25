using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab.Interface;
using Lab.Enum;
using System.Collections;
using MyFunctions;

namespace Lab.Class
{
    public class ProjectBoard : IPrintable, IEnumerable<Task>
    {
        public delegate void WarningToMuchTasksInToDo(string message);

        static public event WarningToMuchTasksInToDo? WarningToMuchTasksInToDoEvent;
        static public event WarningToMuchTasksInToDo? WarningToMuchToMuchTasksInToDoEvent;

        public string Name { get; set; }
        public List<Task> Tasks { get; } = new List<Task>();

        public ProjectBoard()
        {
            Name = "ProjectBoard";
        }
        public ProjectBoard(string name)
        {
            Name = name;
        }

        public Task AddTask(User sessionUser, string taskName)
        {
            if (PermissionService.CanInteractWithProjectBoard(sessionUser))
            {
                Tasks.Add(new Task(taskName));

                if (Tasks.Count > 10)
                {
                    WarningToMuchToMuchTasksInToDoEvent?.Invoke("STOP!!!! STOP!!!! STOP!!!! :(");
                }
                else if (Tasks.Count > 5)
                {
                    WarningToMuchTasksInToDoEvent?.Invoke("You have to much tasks in to do, its recomended to STOP!!!! :(");
                }


                return Tasks.Last();
            }
            else
            {
                throw new WarningException("Only CEO and ProjectManager can add task");
            }
        }

        public List<Task> SearchTasks(Predicate<Task> predicate)
        {
            return Tasks.FindAll(predicate);
        }

        public void RemoveTask(User assignee, Task task)
        {
            if (PermissionService.CanInteractWithProjectBoard(assignee))
            {
                Tasks.Remove(task);
            }
            else
            {
                throw new WarningException("Only CEO and ProjectManager can remove task");
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
                throw new WarningException("Only CEO and ProjectManager can change name");
            }
        }

        public string GetInfo()
        {
            return $"[Project Board] - {Name}. Tasks: {Tasks.Count}";
        }

        public IEnumerator<Task> GetEnumerator()
        {
            return Tasks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
