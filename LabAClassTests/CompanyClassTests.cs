using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab.Class;
using Lab.Interface;
using System;
using System.Linq;

namespace LabAClassTests
{
    [TestClass]
    public sealed class CompanyClassTests
    {
        [TestMethod]
        public void CompanyCreationNoNameTest()
        {
            //Arrange
            IUserInfo ceo = new CEO();
            string expectedName = "Company";

            //Act
            Company company = new Company(ceo);

            //Assert
            Assert.AreEqual(ceo, company.Employees[0]);
            Assert.AreEqual(company.Name, expectedName);

        }

        [TestMethod]
        public void CompanyCreationWithNameTest()
        {
            //Arrange
            IUserInfo ceo = new CEO();
            string expectedName = "Mishdef Corp.";

            //Act
            Company company = new Company(ceo, expectedName);

            //Assert
            Assert.AreEqual(ceo, company.Employees[0]);
            Assert.AreEqual(company.Name, expectedName);
        }

        [TestMethod]
        public void ProjectBoardCreationGrantedAccessTest()
        {
            //Arrange
            IUserInfo ceo = new CEO();
            Company company = new Company(ceo);

            //Act
            company.CreateProjectBoard(ceo, "NewBoard");

            //Assert
            Assert.AreEqual(1, company.ProjectBoards.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ProjectBoardCreationDeniedAccessFromEmployeeTest()
        {
            //Arrange
            IUserInfo ceo = new CEO();
            IUserInfo employee = new Employee();
            Company company = new Company(ceo);

            //Act
            company.CreateProjectBoard(employee, "NewBoard");

            //Assert
            Assert.AreEqual(0, company.ProjectBoards.Count);
        }

        [TestMethod]
        public void AddEmployeeGrantedAccessTest()
        {
            // Arrange
            IUserInfo ceo = new CEO();
            Company company = new Company(ceo);
            IUserInfo newEmployee = new Employee();
            int initialCount = company.Employees.Count;

            // Act
            company.AddEmployee(ceo, newEmployee);

            // Assert
            Assert.AreEqual(initialCount + 1, company.Employees.Count);
            Assert.IsTrue(company.Employees.Contains(newEmployee));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddEmployeeDeniedAccessFromEmployeeTest()
        {
            // Arrange
            IUserInfo ceo = new CEO();
            Company company = new Company(ceo);
            IUserInfo existingEmployee = new Employee();
            IUserInfo employeeToAdd = new Employee();

            company.AddEmployee(ceo, existingEmployee);
            int initialCount = company.Employees.Count;

            // Act
            company.AddEmployee(existingEmployee, employeeToAdd);

            // Assert
            Assert.AreEqual(initialCount, company.Employees.Count);
        }

        [TestMethod]
        public void ChangeNameGrantedAccessTest()
        {
            // Arrange
            IUserInfo ceo = new CEO();
            Company company = new Company(ceo);
            string newName = "Future Corp.";

            // Act
            company.ChangeName(ceo, newName);

            // Assert
            Assert.AreEqual(newName, company.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ChangeNameDeniedAccessFromEmployeeTest()
        {
            // Arrange
            IUserInfo ceo = new CEO();
            IUserInfo employee = new Employee();
            Company company = new Company(ceo);
            string originalName = company.Name;
            string newName = "Evil Corp.";

            // Act
            company.ChangeName(employee, newName);

            // Assert
            Assert.AreEqual(originalName, company.Name);
        }

        [TestMethod]
        public void RemoveEmployeeGrantedAccessTest()
        {
            // Arrange
            IUserInfo ceo = new CEO();
            Company company = new Company(ceo);
            IUserInfo employeeToRemove = new Employee();
            company.AddEmployee(ceo, employeeToRemove); 
            int initialCount = company.Employees.Count;

            // Act
            company.RemoveEmployee(ceo, employeeToRemove);

            // Assert
            Assert.AreEqual(initialCount - 1, company.Employees.Count);
            Assert.IsFalse(company.Employees.Contains(employeeToRemove));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RemoveEmployeeDeniedAccessSelfRemovalTest()
        {
            // Arrange
            IUserInfo ceo = new CEO();
            Company company = new Company(ceo);
            int initialCount = company.Employees.Count;

            // Act
            company.RemoveEmployee(ceo, ceo);

            // Assert
            Assert.AreEqual(initialCount, company.Employees.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RemoveEmployeeDeniedAccessFromEmployeeTest()
        {
            // Arrange
            IUserInfo ceo = new CEO();
            IUserInfo actingEmployee = new Employee();
            IUserInfo targetEmployee = new Employee();
            Company company = new Company(ceo);
            company.AddEmployee(ceo, actingEmployee);
            company.AddEmployee(ceo, targetEmployee);
            int initialCount = company.Employees.Count;

            // Act
            company.RemoveEmployee(actingEmployee, targetEmployee);

            // Assert
            Assert.AreEqual(initialCount, company.Employees.Count);
        }

        [TestMethod]
        public void RemoveProjectBoardGrantedAccessTest()
        {
            // Arrange
            IUserInfo ceo = new CEO();
            Company company = new Company(ceo);
            company.CreateProjectBoard(ceo, "Board_A");
            var boardToRemove = company.ProjectBoards.First();
            int initialCount = company.ProjectBoards.Count; 

            // Act
            company.RemoveProjectBoard(ceo, boardToRemove);

            // Assert
            Assert.AreEqual(initialCount - 1, company.ProjectBoards.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RemoveProjectBoardDeniedAccessTest()
        {
            // Arrange
            IUserInfo ceo = new CEO();
            IUserInfo employee = new Employee();
            Company company = new Company(ceo);
            company.CreateProjectBoard(ceo, "Board_A");

            var boardToRemove = company.ProjectBoards.First();
            int initialCount = company.ProjectBoards.Count; 

            // Act
            company.RemoveProjectBoard(employee, boardToRemove);

            // Assert
            Assert.AreEqual(initialCount, company.ProjectBoards.Count);
        }
    }
}