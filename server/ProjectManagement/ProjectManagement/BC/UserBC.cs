﻿using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.BC
{
    public class UserBC
    {
        DAC.ProjectManagementEntities dbContext = null;

        public UserBC()
        {
            dbContext = new DAC.ProjectManagementEntities();
        }

        public UserBC(DAC.ProjectManagementEntities context)
        {
            dbContext = context;
        }
        public List<User> GetUser()
        {
            using (dbContext)
            {
                return dbContext.Users.Select(x => new User()
                {
                    FirstName = x.First_Name,
                    LastName = x.Last_Name,
                    EmployeeId = x.Employee_ID,
                    UserId = x.User_ID
                }).ToList();
            }

        }

        public int InsertUserDetails(User user)
        {
            using (dbContext)
            {
                dbContext.Users.Add(new DAC.User()
                {
                    Last_Name = user.LastName,
                    First_Name = user.FirstName,
                    Employee_ID = user.EmployeeId
                });
                return dbContext.SaveChanges();
            }
        }

        public int UpdateUserDetails(User user)
        {
            using (dbContext)
            {
                var editDetails = (from editUser in dbContext.Users
                                   where editUser.User_ID == user.UserId
                                   select editUser).First();
                // Modify existing records
                if (editDetails != null)
                {
                    editDetails.First_Name = user.FirstName;
                    editDetails.Last_Name = user.LastName;
                    editDetails.Employee_ID = user.EmployeeId;

                }
                return dbContext.SaveChanges();
            }

        }

        public int DeleteUserDetails(User user)
        {
            using (dbContext)
            {
                var editDetails = (from editUser in dbContext.Users
                                   where editUser.User_ID == user.UserId
                                   select editUser).First();
                // Delete existing record
                if (editDetails != null)
                {
                    dbContext.Users.Remove(editDetails);
                }
                return dbContext.SaveChanges();
            }

        }
    }
}