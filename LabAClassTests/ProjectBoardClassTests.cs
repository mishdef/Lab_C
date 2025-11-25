using Lab.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab.Class;

namespace LabAClassTests
{
    [TestClass]
    public sealed class ProjectBoardClassTests
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
        }

        [TestCleanup]
        public void TestCleanup() {
           company = null;
        }

        [TestMethod]
        public void AddTaskGrantedAccess() 
        { 
            //Arrange

            //Act
            Lab.Class.Task task = company.ProjectBoards[0].AddTask(projectManager, "Task_A");

            //Assert
            Assert.AreEqual(1, company.ProjectBoards[0].Tasks.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddTaskDeniedAccess() 
        { 
            //Arrange

            //Act
            Lab.Class.Task task = company.ProjectBoards[0].AddTask(employee, "Task_A");

            //Assert
            Assert.AreEqual(0, company.ProjectBoards[0].Tasks.Count);
        }

        [TestMethod]
        public void RemoveTaskGrantedAccess() 
        { 
            //Arrange
            company.ProjectBoards[0].AddTask(projectManager, "Task_A");

            //Act
            company.ProjectBoards[0].RemoveTask(projectManager, company.ProjectBoards[0].Tasks[0]);

            //Assert
            Assert.AreEqual(0, company.ProjectBoards[0].Tasks.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RemoveTaskDeniedAccess() 
        {
            //Arrange
            company.ProjectBoards[0].AddTask(projectManager, "Task_A");

            //Act
            company.ProjectBoards[0].RemoveTask(employee, company.ProjectBoards[0].Tasks[0]);

            //Assert
            Assert.AreEqual(1, company.ProjectBoards[0].Tasks.Count);
        }

        [TestMethod]
        public void ChangeNameOfTheBoardGrantedAccess()
        {
            //Arrange
            
            //Act
            company.ProjectBoards[0].ChangeName(projectManager, "NewName");

            //Assert
            Assert.AreEqual("NewName", company.ProjectBoards[0].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ChangeNameOfTheBoardDeniedAccess()
        {
            //Arrange

            //Act
            company.ProjectBoards[0].ChangeName(employee, "NewName");

            //Assert
            Assert.AreEqual("Board_A", company.ProjectBoards[0].Name);
        }

        [TestMethod]
        public void GetInfoTest()
        {
            //Arrange
            string expectedInfo = "[Project Board] - Board_A. Tasks: 0";

            //Act
            string info = company.ProjectBoards[0].GetInfo();

            //Assert
            Assert.AreEqual(expectedInfo, info);
        }
    }
}
