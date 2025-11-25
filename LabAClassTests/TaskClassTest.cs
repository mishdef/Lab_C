using Lab.Class;
using Lab.Interface;
using Lab.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;

namespace LabAClassTests
{
    [TestClass]
    public sealed class TaskClassTest
    {
        IUserInfo ceo;
        Company company;
        IUserInfo projectManager;
        IUserInfo employee;


        [TestInitialize]
        public void TestInitialize()
        {
            ceo = new CEO();
            company = new Company(ceo);
            projectManager = new ProjectManager();
            employee = new Employee();
            company.CreateProjectBoard(ceo, "Board_A");
            company.AddEmployee(ceo, employee);
            company.AddEmployee(ceo, projectManager);
            company.ProjectBoards[0].AddTask(projectManager, "Task_A");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            company = null;
        }

        [TestMethod]
        public void AssignTaskToEmployeeGrantedAccess() 
        {
            //Arrange

            //Act
            company.ProjectBoards[0].Tasks[0].AssignEmployee(projectManager, employee);

            //Assert
            Assert.AreEqual(employee, company.ProjectBoards[0].Tasks[0].Assignee);
        }

        [TestMethod]
        public void AssignTaskToEmployeeDeniedAccess() 
        {
            //Arrange

            //Act
            company.ProjectBoards[0].Tasks[0].AssignEmployee(employee, employee);


            //Assert
            Assert.AreEqual(null, company.ProjectBoards[0].Tasks[0].Assignee);
        }

        [TestMethod]
        public void UnassignTaskFromEmployeeGrantedAccess()
        {
            //Arrange

            //Act
            company.ProjectBoards[0].Tasks[0].AssignEmployee(employee, projectManager);
            company.ProjectBoards[0].Tasks[0].UnassignEmployee(employee, projectManager);

            //Assert
            Assert.AreEqual(null, company.ProjectBoards[0].Tasks[0].Assignee);
        }

        [TestMethod]
        public void UnassignTaskFromEmployeeDeniedAccess()
        {
            //Arrange

            //Act
            company.ProjectBoards[0].Tasks[0].AssignEmployee(employee, projectManager);
            company.ProjectBoards[0].Tasks[0].UnassignEmployee(employee, employee);

            //Assert
            Assert.AreEqual(null, company.ProjectBoards[0].Tasks[0].Assignee);
        }

        [TestMethod]
        public void MoveTaskToDONEGrantedAccess()
        {
            //Arrange

            //Act
            company.ProjectBoards[0].Tasks[0].AssignEmployee(employee, projectManager);
            company.ProjectBoards[0].Tasks[0].MoveTask(projectManager, TaskStat.Done);

            //Assert
            Assert.AreEqual(TaskStat.Done, company.ProjectBoards[0].Tasks[0].CurrentStatus);
        }

        [TestMethod]
        public void GetInfoTest()
        {
            //Arrange
            string expectedInfo = "[Task] - Task_A | Status: ToDo | Assignee: Unassigned";

            //Act
            string info = company.ProjectBoards[0].Tasks[0].GetInfo();

            //Assert
            Assert.AreEqual(expectedInfo, info);
        }
    }
}
