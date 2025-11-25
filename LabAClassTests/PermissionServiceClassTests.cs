using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab.Interface;
using Lab.Class; 

namespace LabAClassTests
{
    [TestClass]
    public sealed class PermissionServiceClassTests
    {
        [TestMethod]
        public void CEOPermissionsTest() 
        { 
            //Arrange
            IUserInfo ceo = new CEO();
            Company company = new Company(ceo);
            bool expectedAccessToCompany = true;
            bool expectedAccessToProjectBoards = true;
            bool expectedAccessToTasks = true;

            //Act
            bool actualAccessToCompany = PermissionService.CanInteractWithCompany(ceo);
            bool actualAccessToProjectBoards = PermissionService.CanInteractWithProjectBoard(ceo);
            bool actualAccessToTasks = PermissionService.CanInteractWithTask(ceo);

            //Assert
            Assert.AreEqual(expectedAccessToCompany, actualAccessToCompany);
            Assert.AreEqual(expectedAccessToProjectBoards, actualAccessToProjectBoards);
            Assert.AreEqual(expectedAccessToTasks, actualAccessToTasks);
        }

        [TestMethod]
        public void EmployeePermissionsTest()
        {
            //Arrange
            IUserInfo ceo = new CEO();
            IUserInfo employee = new Employee();
            Company company = new Company(new CEO());
            bool expectedAccessToCompany = false;
            bool expectedAccessToProjectBoards = false;
            bool expectedAccessToTasks = true;

            //Act
            bool actualAccessToCompany = PermissionService.CanInteractWithCompany(employee);
            bool actualAccessToProjectBoards = PermissionService.CanInteractWithProjectBoard(employee);
            bool actualAccessToTasks = PermissionService.CanInteractWithTask(employee);

            //Assert
            Assert.AreEqual(expectedAccessToCompany, actualAccessToCompany);
            Assert.AreEqual(expectedAccessToProjectBoards, actualAccessToProjectBoards);
            Assert.AreEqual(expectedAccessToTasks, actualAccessToTasks);
        }

        [TestMethod]
        public void ProjectManagerPermissionsTest()
        {
            //Arrange
            IUserInfo ceo = new CEO();
            IUserInfo projectManager = new ProjectManager();
            Company company = new Company(new CEO());
            bool expectedAccessToCompany = false;
            bool expectedAccessToProjectBoards = true;
            bool expectedAccessToTasks = true;

            //Act
            bool actualAccessToCompany = PermissionService.CanInteractWithCompany(projectManager);
            bool actualAccessToProjectBoards = PermissionService.CanInteractWithProjectBoard(projectManager);
            bool actualAccessToTasks = PermissionService.CanInteractWithTask(projectManager);

            //Assert
            Assert.AreEqual(expectedAccessToCompany, actualAccessToCompany);
            Assert.AreEqual(expectedAccessToProjectBoards, actualAccessToProjectBoards);
            Assert.AreEqual(expectedAccessToTasks, actualAccessToTasks);
        }
    }
}
