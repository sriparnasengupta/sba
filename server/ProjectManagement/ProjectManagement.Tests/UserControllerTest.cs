using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagement.Controllers;
using System.Collections.Generic;
using System.Web;
using ProjectManagement.Models;
using System.Data.Entity;

namespace ProjectManagement.Test
{
    class MockProjectManagementEntities : DAC.ProjectManagementEntities
    {
        private DbSet<DAC.User> _users = null;
        private DbSet<DAC.Project> _projects = null;
        private DbSet<DAC.Task> _tasks = null;
        public override DbSet<DAC.User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
            }
        }

        public override DbSet<DAC.Project> Projects
        {
            get
            {
                return _projects;
            }
            set
            {
                _projects = value;
            }
        }

        public override DbSet<DAC.Task> Tasks
        {
            get
            {
                return _tasks;
            }
            set
            {
                _tasks = value;
            }
        }
    }

    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void TestGetUser_Success()
        {
            var context = new MockProjectManagementEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Payel",
                Last_Name = "Basu",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "523365",
                First_Name = "Priyanka",
                Last_Name = "Sen",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 523365
            });
            context.Users = users;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.GetUser() as JSendResponse;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Data, typeof(List<User>));
            Assert.AreEqual((result.Data as List<User>).Count, 2);
        }

        [TestMethod]
        public void TestInsertUser_Success()
        {
            var context = new MockProjectManagementEntities();
            var user = new Models.User();
            user.FirstName = "sriparna";
            user.LastName = "sengupta";
            user.EmployeeId = "123456";
            user.UserId = 123;
            var controller = new UserController(new BC.UserBC(context));
            var result = controller.InsertUserDetails(user) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Data, 1);
        }

        [TestMethod]
        public void TestUpdateUser_Success()
        {
            var context = new MockProjectManagementEntities();
            var users = new TestDbSet<DAC.User>();

            users.Add(new DAC.User()
            {
                Employee_ID = "523365",
                First_Name = "Priyanka",
                Last_Name = "Sen",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 523365
            });
            context.Users = users;

            var user = new Models.User();
            user.FirstName = "Priyanka";
            user.LastName = "Sen";
            user.EmployeeId = "123";
            user.UserId = 523365;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual((context.Users.Local[0]).First_Name.ToUpper(), "PRIYANKA");
        }

        [TestMethod]
        public void TestDeleteUser_Success()
        {
            var context = new MockProjectManagementEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Payel",
                Last_Name = "Basu",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "523365",
                First_Name = "Priyanka",
                Last_Name = "Sen",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 523365
            });
            context.Users = users;

            var user = new Models.User();
            user.FirstName = "Priyanka";
            user.LastName = "Sen";
            user.EmployeeId = "523365";
            user.UserId = 523365;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.DeleteUserDetails(user) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual(context.Users.Local.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeleteUser_UserNullException()
        {
            var context = new MockProjectManagementEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Payel",
                Last_Name = "Basu",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "523365",
                First_Name = "Priyanka",
                Last_Name = "Sen",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 523365
            });
            context.Users = users;

            var user = new Models.User();
            user = null;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.DeleteUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestDeleteUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagementEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Payel",
                Last_Name = "Basu",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "523365",
                First_Name = "Priyanka",
                Last_Name = "Sen",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 523365
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "TEST";

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.DeleteUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagementEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Payel",
                Last_Name = "Basu",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "523365",
                First_Name = "Priyanka",
                Last_Name = "Sen",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 523365
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "-233";

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.DeleteUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagementEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Payel",
                Last_Name = "Basu",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "523365",
                First_Name = "Priyanka",
                Last_Name = "Sen",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 523365
            });
            context.Users = users;

            var user = new Models.User();
            user.ProjectId = -1;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.DeleteUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteUser_NegativeUserIdFormat()
        {
            var context = new MockProjectManagementEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Payel",
                Last_Name = "Basu",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "523365",
                First_Name = "Priyanka",
                Last_Name = "Sen",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 523365
            });
            context.Users = users;

            var user = new Models.User();
            user.UserId = -1;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.DeleteUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateUser_UserNullException()
        {
            var context = new MockProjectManagementEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Payel",
                Last_Name = "Basu",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "523365",
                First_Name = "Priyanka",
                Last_Name = "Sen",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 523365
            });
            context.Users = users;

            var user = new Models.User();
            user = null;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestUpdateUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagementEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Payel",
                Last_Name = "Basu",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "523365",
                First_Name = "Priyanka",
                Last_Name = "Sen",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 523365
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "TEST";

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagementEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Payel",
                Last_Name = "Basu",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "523365",
                First_Name = "Priyanka",
                Last_Name = "Sen",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 523365
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "-233";

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagementEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Payel",
                Last_Name = "Basu",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "523365",
                First_Name = "Priyanka",
                Last_Name = "Sen",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 523365
            });
            context.Users = users;

            var user = new Models.User();
            user.ProjectId = -1;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateUser_NegativeUserIdFormat()
        {
            var context = new MockProjectManagementEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Payel",
                Last_Name = "Basu",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "523365",
                First_Name = "Priyanka",
                Last_Name = "Sen",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 523365
            });
            context.Users = users;

            var user = new Models.User();
            user.UserId = -1;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertUser_UserNullException()
        {
            var context = new MockProjectManagementEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Payel",
                Last_Name = "Basu",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "523365",
                First_Name = "Priyanka",
                Last_Name = "Sen",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 523365
            });
            context.Users = users;

            var user = new Models.User();
            user = null;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.InsertUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestInsertUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagementEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Payel",
                Last_Name = "Basu",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "523365",
                First_Name = "Priyanka",
                Last_Name = "Sen",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 523365
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "TEST";

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.InsertUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestInsertUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagementEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Payel",
                Last_Name = "Basu",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "523365",
                First_Name = "Priyanka",
                Last_Name = "Sen",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 523365
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "-233";

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.InsertUserDetails(user) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestInsertUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagementEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Payel",
                Last_Name = "Basu",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "523365",
                First_Name = "Priyanka",
                Last_Name = "Sen",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 523365
            });
            context.Users = users;

            var user = new Models.User();
            user.ProjectId = -1;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.InsertUserDetails(user) as JSendResponse;
        }

    }
}

