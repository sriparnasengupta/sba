using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagement.Controllers;
using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Test
{

    [TestClass]
    public class TaskControllerTest
    {
        [TestMethod]
        public void TestRetrieveTasks_Success()
        {
            var context = new MockProjectManagementEntities();
            var tasks = new TestDbSet<DAC.Task>();
            var users = new TestDbSet<DAC.User>();
            var parentTasks = new TestDbSet<DAC.ParentTask>();

            parentTasks.Add(new DAC.ParentTask()
            {
                Parent_ID = 123456,
                Parent_Task_Name = "PNB"

            });
            context.ParentTasks = parentTasks;
            users.Add(new DAC.User()
            {
                Employee_ID = "414942",
                First_Name = "ankita",
                Last_Name = "ghosh",
                User_ID = 123,
                Task_ID = 1
            });
            context.Users = users;
            int projectid = 1234;
            tasks.Add(new DAC.Task()
            {
                Task_ID = 1,
                Task_Name = "ASDQW",
                Parent_ID = 123456,
                Project_ID = 1234,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0

            });
            context.Tasks = tasks;
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.RetrieveTaskByProjectId(projectid) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Data, typeof(List<ProjectManagement.Models.Task>));
        }

        [TestMethod]
        public void TestRetrieveParentTasks_Success()
        {
            var context = new MockProjectManagementEntities();
            var parentTasks = new TestDbSet<DAC.ParentTask>();
            parentTasks.Add(new DAC.ParentTask()
            {
                Parent_ID = 12345,
                Parent_Task_Name = "ANB"

            });
            parentTasks.Add(new DAC.ParentTask()
            {
                Parent_ID = 123456,
                Parent_Task_Name = "PNB"

            });
            context.ParentTasks = parentTasks;

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.RetrieveParentTasks() as JSendResponse;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Data, typeof(List<ProjectManagement.Models.ParentTask>));
            Assert.AreEqual((result.Data as List<ParentTask>).Count, 2);
        }

        [TestMethod]
        public void TestInsertTasks_Success()
        {
            var context = new MockProjectManagementEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "414942",
                First_Name = "ankita",
                Last_Name = "ghosh",
                User_ID = 123,
                Task_ID = 123
            });
            context.Users = users;
            var task = new ProjectManagement.Models.Task()
            {

                Task_Name = "ASDQW",
                Parent_ID = 123674,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0,
                User = new User()
                {
                    FirstName = "ankita",
                    LastName = "ghosh",
                    EmployeeId = "123456",
                    UserId = 123
                }
            };

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.InsertTaskDetails(task) as JSendResponse;


            Assert.IsNotNull(result);

            Assert.IsNotNull((context.Users.Local[0]).Task_ID);
        }

        [TestMethod]
        public void TestUpdateProjects_Success()
        {
            var context = new MockProjectManagementEntities();
            var tasks = new TestDbSet<DAC.Task>();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = 418220.ToString(),
                First_Name = "TEST",
                Last_Name = "TEST2",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 123
            });
            tasks.Add(new DAC.Task()
            {
                Task_ID = 1,
                Task_Name = "ASDQW",
                Parent_ID = 123674,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0
            });
            context.Tasks = tasks;
            context.Users = users;
            var testTask = new Models.Task()
            {
                TaskId = 1,
                Task_Name = "task1",
                Parent_ID = 123674,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 30,
                Status = 0,
                User = new User()
                {
                    FirstName = "ankita",
                    LastName = "ghosh",
                    EmployeeId = "123456",
                    UserId = 123
                }
            };

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.UpdateTaskDetails(testTask) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual((context.Tasks.Local[0]).Priority, 30);
        }

        [TestMethod]
        public void TestDeleteProjects_Success()
        {
            var context = new MockProjectManagementEntities();
            var tasks = new TestDbSet<DAC.Task>();

            tasks.Add(new DAC.Task()
            {
                Task_ID = 1,
                Task_Name = "task1",
                Parent_ID = 123674,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0
            });
            context.Tasks = tasks;
            var testTask = new Models.Task()
            {
                TaskId = 1,
                Task_Name = "task1",
                Parent_ID = 123674,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0
            };

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.DeleteTaskDetails(testTask) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual((context.Tasks.Local[0]).Status, 1);
        }

        [TestMethod]
        public void TestRetrieveTaskByProjectId_Success()
        {
            var context = new MockProjectManagementEntities();
            var tasks = new TestDbSet<DAC.Task>();
            var users = new TestDbSet<DAC.User>();
            var parentTasks = new TestDbSet<DAC.ParentTask>();
            parentTasks.Add(new DAC.ParentTask()
            {
                Parent_ID = 12345,
                Parent_Task_Name = "ANB"

            });
            context.ParentTasks = parentTasks;
            users.Add(new DAC.User()
            {
                Employee_ID = "414942",
                First_Name = "ankita",
                Last_Name = "ghosh",
                User_ID = 123,
                Task_ID = 12345,
                Project_ID = 1234
            });
            context.Users = users;
            tasks.Add(new DAC.Task()
            {
                Project_ID = 12345,
                Parent_ID = 12345,
                Task_ID = 12345,
                Task_Name = "TEST",
                Priority = 1,
                Status = 1,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5)
            });
            tasks.Add(new DAC.Task()
            {
                Project_ID = 123,
                Parent_ID = 123,
                Task_ID = 123,
                Task_Name = "TEST",
                Priority = 1,
                Status = 1,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5)
            });
            context.Tasks = tasks;

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.RetrieveTaskByProjectId(12345) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Data, typeof(List<ProjectManagement.Models.Task>));
            Assert.AreEqual((result.Data as List<ProjectManagement.Models.Task>).Count, 1);
            Assert.AreEqual((result.Data as List<ProjectManagement.Models.Task>)[0].Task_Name, "TEST");
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestRetrieveTaskByProjectId_NegativeTaskId()
        {
            var context = new MockProjectManagementEntities();

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.RetrieveTaskByProjectId(-12345) as JSendResponse;
        }





        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertTask_NullTaskObject()
        {
            var context = new MockProjectManagementEntities();

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.InsertTaskDetails(null) as JSendResponse;
        }


        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestInsertTask_NegativeTaskParentId()
        {
            var context = new MockProjectManagementEntities();
            ProjectManagement.Models.Task task = new Models.Task();
            task.Parent_ID = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.InsertTaskDetails(task) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestInsertTask_NegativeProjectId()
        {
            var context = new MockProjectManagementEntities();
            ProjectManagement.Models.Task task = new Models.Task();
            task.Project_ID = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.InsertTaskDetails(task) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestInsertTask_NegativeTaskId()
        {
            var context = new MockProjectManagementEntities();
            ProjectManagement.Models.Task task = new Models.Task();
            task.TaskId = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.InsertTaskDetails(task) as JSendResponse;
        }




        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateTask_NullTaskObject()
        {
            var context = new MockProjectManagementEntities();

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.UpdateTaskDetails(null) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateTask_NegativeTaskParentId()
        {
            var context = new MockProjectManagementEntities();
            ProjectManagement.Models.Task task = new Models.Task();
            task.Parent_ID = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.UpdateTaskDetails(task) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateTask_NegativeProjectId()
        {
            var context = new MockProjectManagementEntities();
            ProjectManagement.Models.Task task = new Models.Task();
            task.Project_ID = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.UpdateTaskDetails(task) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateTask_NegativeTaskId()
        {
            var context = new MockProjectManagementEntities();
            ProjectManagement.Models.Task task = new Models.Task();
            task.TaskId = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.UpdateTaskDetails(task) as JSendResponse;
        }




        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeleteTask_NullTaskObject()
        {
            var context = new MockProjectManagementEntities();

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.DeleteTaskDetails(null) as JSendResponse;
        }



        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteTask_NegativeTaskParentId()
        {
            var context = new MockProjectManagementEntities();
            ProjectManagement.Models.Task task = new Models.Task();
            task.Parent_ID = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.DeleteTaskDetails(task) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteTask_NegativeProjectId()
        {
            var context = new MockProjectManagementEntities();
            ProjectManagement.Models.Task task = new Models.Task();
            task.Project_ID = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.DeleteTaskDetails(task) as JSendResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteTask_NegativeTaskId()
        {
            var context = new MockProjectManagementEntities();
            ProjectManagement.Models.Task task = new Models.Task();
            task.TaskId = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.DeleteTaskDetails(task) as JSendResponse;
        }
    }
}
