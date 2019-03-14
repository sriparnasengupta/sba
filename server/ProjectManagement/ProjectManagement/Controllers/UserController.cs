﻿using ProjectManagement.ActionFilters;
using ProjectManagement.BC;
using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectManagement.Controllers
{
    public class UserController : ApiController
    {
        UserBC _userObjBC = null;

        public UserController()
        {
            _userObjBC = new UserBC();
        }

        public UserController(UserBC userObjBC)
        {
            _userObjBC = userObjBC;
        }

        [HttpGet]
        [ProjManagementLogFilter]
        [ProjManagementExceptionFilter]
        [Route("api/user")]
        public JSendResponse GetUser()
        {
            List<User> Users = _userObjBC.GetUser();

            return new JSendResponse()
            {
                Data = Users
            };
        }

        [HttpPost]
        [ProjManagementLogFilter]
        [ProjManagementExceptionFilter]
        [Route("api/user/add")]
        public JSendResponse InsertUserDetails(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User id is null");
            }
            try
            {
                int employeeId = Convert.ToInt32(user.EmployeeId);
            }
            catch (FormatException ex)
            {
                throw new FormatException("Invalid format of employee Id", ex);
            }
            if (Convert.ToInt32(user.EmployeeId) < 0)
            {
                throw new ArithmeticException("Employee id cannot be negative");
            }
            if (Convert.ToInt32(user.ProjectId) < 0)
            {
                throw new ArithmeticException("Project id cannot be negative");
            }
            return new JSendResponse()
            {
                Data = _userObjBC.InsertUserDetails(user)
            };

        }

        [HttpPost]
        [Route("api/user/update")]
        [ProjManagementLogFilter]
        [ProjManagementExceptionFilter]
        public JSendResponse UpdateUserDetails(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User id is null");
            }
            try
            {
                int employeeId = Convert.ToInt32(user.EmployeeId);
            }
            catch (FormatException ex)
            {
                throw new FormatException("Invalid format of employee Id", ex);
            }
            if (Convert.ToInt32(user.EmployeeId) < 0)
            {
                throw new ArithmeticException("Employee id cannot be negative");
            }
            if (Convert.ToInt32(user.ProjectId) < 0)
            {
                throw new ArithmeticException("Project id cannot be negative");
            }
            if (user.UserId <= 0)
            {
                throw new ArithmeticException("User id cannot be negative or 0");
            }
            return new JSendResponse()
            {
                Data = _userObjBC.UpdateUserDetails(user)
            };
        }

        [HttpPost]
        [Route("api/user/delete")]
        public JSendResponse DeleteUserDetails(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User id is null");
            }
            try
            {
                int employeeId = Convert.ToInt32(user.EmployeeId);
            }
            catch (FormatException ex)
            {
                throw new FormatException("Invalid format of employee Id", ex);
            }
            if (Convert.ToInt32(user.EmployeeId) < 0)
            {
                throw new ArithmeticException("Employee id cannot be negative");
            }
            if (Convert.ToInt32(user.ProjectId) < 0)
            {
                throw new ArithmeticException("Project id cannot be negative");
            }
            if (user.UserId <= 0)
            {
                throw new ArithmeticException("User id cannot be negative or 0");
            }
            return new JSendResponse()
            {
                Data = _userObjBC.DeleteUserDetails(user)
            };
        }
    }
}
